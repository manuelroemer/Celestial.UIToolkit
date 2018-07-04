using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Aurora.UIToolkit
{

    /// <summary>
    /// Provides attached dependency properties which allow setting the
    /// Margin properties of both <see cref="FrameworkElement"/> objects, aswell as
    /// an object's children.
    /// </summary>
    public static class MarginHelper
    {

        #region Attached Property Definitions

        /// <summary>
        /// Identifies the MarginGroups attached dependency property.
        /// </summary>
        public static readonly DependencyProperty MarginGroupsProperty = DependencyProperty.RegisterAttached(
            "MarginGroups", typeof(MarginGroupCollection), typeof(MarginHelper), new PropertyMetadata(new MarginGroupCollection()));

        /// <summary>
        /// Identifies the MarginGroup attached dependency property.
        /// </summary>
        public static readonly DependencyProperty MarginGroupProperty = DependencyProperty.RegisterAttached(
            "MarginGroup", typeof(string), typeof(MarginHelper), new PropertyMetadata(null, MarginGroup_Changed));

        /// <summary>
        /// Identifies the ChildGroup attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ChildrenGroupProperty = DependencyProperty.RegisterAttached(
            "ChildrenGroup", typeof(string), typeof(MarginHelper), new PropertyMetadata(null, ChildrenGroup_Changed));

        /// <summary>
        /// Identifies the ChildrenMargin attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ChildrenMarginProperty = DependencyProperty.RegisterAttached(
            "ChildrenMargin", typeof(Thickness), typeof(MarginHelper), new PropertyMetadata(new Thickness(), ChildrenMargin_Changed));

        /// <summary>
        /// Gets the value of the <see cref="MarginGroupsProperty"/>.
        /// </summary>
        /// <param name="obj">The dependency object for which the property should be retrieved.</param>
        /// <returns>A <see cref="MarginGroupCollection"/>.</returns>
        public static MarginGroupCollection GetMarginGroups(DependencyObject obj)
        {
            return (MarginGroupCollection)obj.GetValue(MarginGroupsProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="MarginGroupsProperty"/> for the specified <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The dependency object for which the property should be set.</param>
        /// <param name="value">A <see cref="MarginGroupCollection"/>.</param>
        public static void SetMarginGroups(DependencyObject obj, MarginGroupCollection value)
        {
            obj.SetValue(MarginGroupsProperty, value);
        }

        /// <summary>
        /// Gets the value of the <see cref="MarginGroupProperty"/>.
        /// </summary>
        /// <param name="obj">The dependency object for which the property should be retrieved.</param>
        /// <returns>A <see cref="string"/> which identifies the object's margin group.</returns>
        public static string GetMarginGroup(DependencyObject obj)
        {
            return (string)obj.GetValue(MarginGroupProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="MarginGroupProperty"/> for the specified <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The dependency object for which the property should be set.</param>
        /// <param name="value">A string which represents the object's margin group.</param>
        public static void SetMarginGroup(DependencyObject obj, string value)
        {
            obj.SetValue(MarginGroupProperty, value);
        }

        /// <summary>
        /// Gets the value of the <see cref="ChildrenGroupProperty"/>.
        /// </summary>
        /// <param name="obj">The dependency object for which the property should be retrieved.</param>
        /// <returns>A <see cref="string"/>.</returns>
        public static string GetChildrenGroup(DependencyObject obj)
        {
            return (string)obj.GetValue(ChildrenGroupProperty);
        }
        
        /// <summary>
        /// Sets the value of the <see cref="ChildrenGroupProperty"/> for the specified <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The dependency object for which the property should be set.</param>
        /// <param name="value">A <see cref="string"/>.</param>
        public static void SetChildrenGroup(DependencyObject obj, string value)
        {
            obj.SetValue(ChildrenGroupProperty, value);
        }

        /// <summary>
        /// Gets the value of the <see cref="ChildrenMarginProperty"/>.
        /// </summary>
        /// <param name="obj">The dependency object for which the property should be retrieved.</param>
        /// <returns>A <see cref="string"/>.</returns>
        public static Thickness GetChildrenMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(ChildrenMarginProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="ChildrenMarginProperty"/> for the specified <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The dependency object for which the property should be set.</param>
        /// <param name="value">A <see cref="Thickness"/>.</param>
        public static void SetChildrenMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(ChildrenMarginProperty, value);
        }

        #endregion
        
        /// <summary>
        /// Called when the <see cref="MarginGroupProperty"/> of a <see cref="DependencyObject"/>
        /// is changed.
        /// When this happens, the object's margin property gets updated (if found).
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> whose <see cref="MarginGroupProperty"/> got changed.<param>
        /// <param name="e">Event args about the changed property.</param>
        private static void MarginGroup_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is FrameworkElement frameworkElement)) return;
            
            MarginGroup group = FindGroup(frameworkElement, (string)e.NewValue);
            if (group != null)
            {
                // If the margin has been explicitly set, don't overwrite it.
                if (frameworkElement.ReadLocalValue(FrameworkElement.MarginProperty) == DependencyProperty.UnsetValue)
                {
                    frameworkElement.Margin = group.Margin;
                }
            }
        }

        /// <summary>
        /// Called when the <see cref="ChildrenGroupProperty"/> of a <see cref="DependencyObject"/> is changed.
        /// When this happens, this handler iterates through each of the object's children (if any can be found)
        /// and sets their margin to the value of this group.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> whose <see cref="ChildrenGroupProperty"/> got changed.</param>
        /// <param name="e">Event args about the changed property.</param>
        private static void ChildrenGroup_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is FrameworkElement frameworkElement &&
                !frameworkElement.IsInitialized)
            {
                // This here is hacky, but necessary:
                // When the attached property is set, a parent is usually not initialized yet.
                // This means that we cannot retrieve its children, which is required for this method.
                // -> We need to wait until the element is initialized, before updating the children's margin values.
                //    Do this by listening to the Initialized event.
                WeakEventManager<FrameworkElement, EventArgs>.AddHandler(
                        frameworkElement,
                        nameof(FrameworkElement.Initialized),
                        Element_Initialized);
            }
            else
            {
                SetChildMargin();
            }

            // Event Handler which calls UpdateChildMargin() once the element is initialized.
            void Element_Initialized(object sender, EventArgs args)
            {
                WeakEventManager<FrameworkElement, EventArgs>.RemoveHandler(
                    frameworkElement,
                    nameof(FrameworkElement.Initialized),
                    Element_Initialized);
                SetChildMargin();
            }

            // Updates the children's margin values.
            void SetChildMargin()
            {
                MarginGroup group = FindGroup(obj as FrameworkElement, (string)e.NewValue);
                if (group != null)
                {
                    UpdateChildMargins(obj, group.Margin);
                }
            }
        }

        /// <summary>
        /// Called when the <see cref="ChildrenMarginProperty"/> of a <see cref="DependencyObject"/> is changed.
        /// When this happens, this handler iterates through each of the object's children (if any can be found)
        /// and sets their margin to the value of the <see cref="ChildrenMarginProperty"/>.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> whose <see cref="ChildrenMarginProperty"/> got changed.</param>
        /// <param name="e">Event args about the changed property.</param>
        private static void ChildrenMargin_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is FrameworkElement frameworkElement &&
                !frameworkElement.IsInitialized)
            {
                // Check ChildrenGroup_Changed for an explanation of the following.
                WeakEventManager<FrameworkElement, EventArgs>.AddHandler(
                        frameworkElement,
                        nameof(FrameworkElement.Initialized),
                        Element_Initialized);
            }
            else
            {
                UpdateChildMargins(obj, (Thickness)e.NewValue);
            }

            void Element_Initialized(object sender, EventArgs args)
            {
                WeakEventManager<FrameworkElement, EventArgs>.RemoveHandler(
                    frameworkElement,
                    nameof(FrameworkElement.Initialized),
                    Element_Initialized);
                UpdateChildMargins(obj, (Thickness)e.NewValue);
            }
        }

        /// <summary>
        /// Tries to find a <see cref="MarginGroup"/> which matches the specified <paramref name="name"/>
        /// in the <paramref name="frameworkElement"/>'s parent tree.
        /// </summary>
        /// <param name="frameworkElement">The element for which a <see cref="MarginGroup"/> is needed.</param>
        /// <param name="name">The name of the desired group.</param>
        /// <returns>
        /// The found <see cref="MarginGroup"/> or
        /// <c>null</c>, if no group with the specified name was found.
        /// </returns>
        private static MarginGroup FindGroup(FrameworkElement frameworkElement, string name)
        {
            if (name == null) return null;
            if (frameworkElement == null)
            {
                Trace.WriteLine(
                    $"Couldn't find margin group {name} in the ancestor tree of {frameworkElement}.",
                    nameof(MarginHelper));
                return null;
            }

            MarginGroupCollection groups = GetMarginGroups(frameworkElement);
            if (groups != null && groups.Count > 0)
            {
                foreach (MarginGroup group in groups)
                {
                    if (group.Name == name) return group;
                }
            }

            return FindGroup(frameworkElement.Parent as FrameworkElement, name);
        }
        
        /// <summary>
        /// Updates the margin property for each of the parent's children.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="margin">The margin which each of the children is supposed to have.</param>
        private static void UpdateChildMargins(DependencyObject parent, Thickness margin)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (child != null)
                {
                    // If a child's margin is explicitly set, don't overwrite it.
                    if (child.ReadLocalValue(FrameworkElement.MarginProperty) == DependencyProperty.UnsetValue)
                    {
                        child.Margin = margin;
                    }
                }
            }
        }

    }

    /// <summary>
    /// Represents a group which defines a shared margin value, identified via a name.
    /// If used together with the <see cref="MarginHelper.MarginGroupProperty"/> and
    /// <see cref="MarginHelper.ChildrenGroupProperty"/>, this allows to set the margin
    /// of multiple controls to the value set in an instance of this class.
    /// </summary>
    public class MarginGroup
    {

        /// <summary>
        /// Gets or sets the group's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the actual margin.
        /// Any object whose <see cref="MarginHelper.MarginGroupProperty"/> is set to this
        /// group will have this margin value.
        /// </summary>
        public Thickness Margin { get; set; }

        /// <summary>
        /// Initializes a new and empty instance of the <see cref="MarginGroup"/> class.
        /// </summary>
        public MarginGroup()
            : this("", new Thickness()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarginGroup"/> class with the specified
        /// values.
        /// </summary>
        /// <param name="name">The group's name.</param>
        /// <param name="margin">The actual margin.</param>
        public MarginGroup(string name, Thickness margin)
        {
            this.Name = name;
            this.Margin = margin;
        }
        
        /// <summary>
        /// Returns a string representation of this margin group.
        /// </summary>
        /// <returns>A <see cref="string"/> representing this margin group.</returns>
        public override string ToString()
        {
            return $"{Name}: {this.Margin}";
        }

    }

    /// <summary>
    /// Provides access to an ordered, strongly-typed collection of <see cref="MarginGroup"/> objects.
    /// </summary>
    public class MarginGroupCollection : Collection<MarginGroup>
    {

        /// <summary>
        /// Inserts the specified <paramref name="item"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item which is supposed to be inserted.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="item"/> is null.</exception>
        protected override void InsertItem(int index, MarginGroup item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            base.InsertItem(index, item);
        }

        /// <summary>
        /// Sets the specified <paramref name="item"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item which is supposed to be set.</param>
        protected override void SetItem(int index, MarginGroup item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            base.SetItem(index, item);
        }

    }

}
