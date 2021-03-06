﻿using System;
using System.Windows;
using System.Windows.Markup;

namespace Celestial.UIToolkit.Xaml
{

    // Thanks to https://stackoverflow.com/a/1866600/10018492
    // for providing the required ideas for this class.

    /// <summary>
    /// A custom markup extension which merges multiple styles
    /// into one single style.
    /// </summary>
    /// <remarks>
    /// Be aware that this markup extension is very resource intensive.
    /// When merging two styles, every single property will have to be copied,
    /// each and every time when this extension is being used.
    /// 
    /// Take care of the following points:
    /// -  Use the extension on small styles with few setters/triggers.
    /// -  Avoid using it on styles which are based on other styles,
    ///    as these base styles will have to be merged aswell.
    /// </remarks>
    [ContentProperty(nameof(StyleKeys))]
    public class MergeStylesExtension : MarkupExtension
    {

        private string _styleKeys;
        private string[] _styleKeyParts;

        /// <summary>
        /// Gets or sets a string which defines the keys of the style resources
        /// which are supposed to be merged.
        /// The string must contain the name of at least one style resource.
        /// Multiple keys must be separated via one of the following chars:
        /// <list type="bullet">
        ///     <item>
        ///         <description>(Whitespace)</description>
        ///     </item>
        ///     <item>
        ///         <description>,</description>
        ///     </item>
        ///     <item>
        ///         <description>;</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <example>
        /// The following is a valid string which references two style resources:
        /// "StyleKey1 StyleKey2"
        /// </example>
        [ConstructorArgument("styleKeys")]
        public string StyleKeys
        {
            get { return _styleKeys; }
            set
            {
                _styleKeys = value ?? "";
                ParseStyleKeyParts();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MergeStylesExtension"/>
        /// with the <see cref="StyleKeys"/> property being an empty string.
        /// </summary>
        public MergeStylesExtension()
            : this("") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MergeStylesExtension"/> class
        /// with the specified <paramref name="styleKeys"/> string.
        /// </summary>
        /// <param name="styleKeys">
        /// A string which defines the keys of the style resources which are supposed
        /// to be merged.
        /// </param>
        public MergeStylesExtension(string styleKeys)
        {
            StyleKeys = styleKeys;
        }

        private void ParseStyleKeyParts()
        {
            const char separator = ',';
            string splittable = StyleKeys.Replace(' ', separator)
                                         .Replace(';', separator);
            _styleKeyParts = splittable.Split(
                new char[] { separator },
                StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Locates and merges the styles which are declared via the 
        /// <see cref="StyleKeys"/> property and returns the resulting style.
        /// </summary>
        /// <param name="serviceProvider">
        /// A service provider to be used by the markup extension.
        /// Can be null.
        /// </param>
        /// <returns>
        /// A <see cref="Style"/> which is the result of the merge operation.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_styleKeyParts.Length == 1)
            {
                // No need to create new styles when there is only one. Act like a StaticResource.
                return RetrieveStyleFromResources(_styleKeyParts[0], serviceProvider);
            }
            else
            {
                return CreateMergedStyle(serviceProvider);
            }
        }
        
        private Style CreateMergedStyle(IServiceProvider serviceProvider)
        {
            var finalStyle = new Style();
            foreach (var styleKey in _styleKeyParts)
            {
                var styleToBeMerged = RetrieveStyleFromResources(styleKey, serviceProvider);
                finalStyle.MergeWith(styleToBeMerged);
            }
            return finalStyle;
        }

        private Style RetrieveStyleFromResources(object resourceKey, IServiceProvider serviceProvider)
        {
            Style style = null;

            try
            {
                style = Application.Current.TryFindResource(resourceKey) as Style;
            }
            catch { }

            if (style == null)
            {
                // If the style couldn't be retrieved by the Application, it may still exist,
                // for instance in another Style's resources. Use a StaticResource to retrieve it.
                try
                {
                    var staticResource = new StaticResourceExtension(resourceKey);
                    style = staticResource.ProvideValue(serviceProvider) as Style;
                }
                catch { }
            }

            if (style == null)
            {
                throw new ResourceReferenceKeyNotFoundException(
                    $"Couldn't find a resource with the key \"{resourceKey}\".",
                    resourceKey
                );
            }

            return style;
        }

    }

    /// <summary>
    /// Provides extension methods for merging <see cref="Style"/> classes.
    /// </summary>
    internal static class StyleMergingExtensions
    {
        
        /// <summary>
        /// Merges the <paramref name="src"/> style's setters, triggers, ...
        /// into this style.
        /// </summary>
        /// <param name="target">
        /// The target style. This is the one which will receive all of the other's properties.
        /// </param>
        /// <param name="src">
        /// The source style. This is the style from which all values will be copied.
        /// In case of conflicts, this is the winning style.
        /// </param>
        public static void MergeWith(this Style target, Style src)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (src == null) throw new ArgumentNullException(nameof(src));
            
            SetMergedTargetType(target, src);
            MergeBaseStyles(target, src);
            MergeSetters(target, src);
            MergeTriggers(target, src);
            MergeResources(target, src);
        }

        private static void SetMergedTargetType(Style target, Style src)
        {
            if (target.TargetType == null)
            {
                target.TargetType = src.TargetType;
            }
            else if (target.TargetType != null && src.TargetType != null)
            {
                ThrowIfTargetTypesAreConflicting(target.TargetType, src.TargetType);
                if (target.TargetType.IsAssignableFrom(src.TargetType))
                    target.TargetType = src.TargetType;
            }
        }

        private static void MergeBaseStyles(Style target, Style src)
        {
            if (src.BasedOn != null)
            {
                target.MergeWith(src.BasedOn);
            }
        }

        private static void MergeSetters(Style target, Style src)
        {
            foreach (var setter in src.Setters)
            {
                target.Setters.Add(setter);
            }
        }

        private static void MergeTriggers(Style target, Style src)
        {
            foreach (var trigger in src.Triggers)
            {
                target.Triggers.Add(trigger);
            }
        }

        private static void MergeResources(Style target, Style src)
        {
            foreach (var resourceKey in src.Resources.Keys)
            {
                target.Resources[resourceKey] = src.Resources[resourceKey];
            }
        }
        
        private static void ThrowIfTargetTypesAreConflicting(Type type1, Type type2)
        {
            if (!type1.IsAssignableFrom(type2) &&
                !type2.IsAssignableFrom(type1))
            {
                throw new InvalidOperationException(
                        "Merging two styles failed. Their target types are mutually " +
                        "excluding each other. " +
                        "Merging styles will only work if the styles target the same type " +
                        "of control, or a control which derives from the other.");
            }
        }

    }

}
