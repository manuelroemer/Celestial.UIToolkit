using System;
using System.Windows;

namespace Celestial.UIToolkit.Xaml
{
    
    /// <summary>
    /// An extension of the <see cref="VisualState"/> class which represents the visual appearance
    /// of the control when it is in a specific state.
    /// This class extends the default <see cref="VisualState"/> with support for setters.
    /// </summary>
    public class ExtendedVisualState : VisualState
    {

        private SetterBaseCollection _setters;
        private ConditionCollection _conditions;

        /// <summary>
        /// Gets a collection of <see cref="SetterBase"/> instances which can be used to change
        /// the appearance of <see cref="UIElement"/> objects when this <see cref="VisualState"/>
        /// is active.
        /// </summary>
        public SetterBaseCollection Setters
        {
            get
            {
                if (_setters == null)
                    _setters = new SetterBaseCollection();
                return _setters;
            }
        }

        /// <summary>
        /// Gets a collection of conditions which determine whether this visual state will
        /// be applied.
        /// </summary>
        public ConditionCollection Conditions
        {
            get
            {
                if (_conditions == null)
                    _conditions = new ConditionCollection();
                return _conditions;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedVisualState"/> class.
        /// </summary>
        public ExtendedVisualState()
        {
        }

        /// <summary>
        /// Returns a value indicating whether all of the <see cref="Conditions"/> are applying
        /// to a specific control.
        /// </summary>
        /// <param name="control">
        /// The control/element for which the conditions are checked.
        /// </param>
        /// <returns>
        /// true if all conditions in the <see cref="Conditions"/> collection apply; false if not.
        /// </returns>
        internal bool AreConditionsApplyingToControl(DependencyObject control)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));

            foreach (var condition in Conditions)
            {
                if (condition != null)
                {
                    object actualValue = control.GetValue(condition.Property);

                    if (!actualValue.Equals(condition.Value))
                        return false;
                }
            }
            return true;
        }

    }

}
