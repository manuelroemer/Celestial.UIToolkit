using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit
{
    public class VisualStateManagerFW : DependencyObject
    {
        /// <summary>Identifiziert die <see cref="P:System.Windows.VisualStateManagerFW.CustomVisualStateManagerFW" />-Abhängigkeitseigenschaft.</summary>
        /// <returns>Der Bezeichner für die <see cref="P:System.Windows.VisualStateManagerFW.CustomVisualStateManagerFW" />-Abhängigkeitseigenschaft.</returns>
        public static readonly DependencyProperty CustomVisualStateManagerFWProperty = DependencyProperty.RegisterAttached("CustomVisualStateManagerFW", typeof(VisualStateManagerFW), typeof(VisualStateManagerFW), (PropertyMetadata)null);
        private static readonly DependencyPropertyKey VisualStateGroupsPropertyKey = DependencyProperty.RegisterAttachedReadOnly("VisualStateGroups", typeof(IList), typeof(VisualStateManagerFW), (PropertyMetadata)new FrameworkPropertyMetadata(/*(object)new ObservableCollectionDefaultValueFactory<VisualStateGroup>()*/));
        /// <summary>Identifiziert die <see cref="P:System.Windows.VisualStateManagerFW.VisualStateGroups" />-Abhängigkeitseigenschaft.</summary>
        /// <returns>Der Bezeichner für die <see cref="P:System.Windows.VisualStateManagerFW.VisualStateGroups" />-Abhängigkeitseigenschaft.</returns>
        public static readonly DependencyProperty VisualStateGroupsProperty = VisualStateManagerFW.VisualStateGroupsPropertyKey.DependencyProperty;
        private static readonly Duration DurationZero = new Duration(TimeSpan.Zero);

        private static bool GoToStateCommon(FrameworkElement control, FrameworkElement stateGroupsRoot, string stateName, bool useTransitions)
        {
            if (stateName == null)
                throw new ArgumentNullException(nameof(stateName));
            if (stateGroupsRoot == null)
                return false;
            IList<VisualStateGroup> stateGroupsInternal = (IList<VisualStateGroup>)VisualStateManagerFW.GetVisualStateGroupsInternal(stateGroupsRoot);
            if (stateGroupsInternal == null)
                return false;
            VisualStateGroup group;
            VisualState state;
            VisualStateManagerFW.TryGetState(stateGroupsInternal, stateName, out group, out state);
            VisualStateManagerFW visualStateManager = VisualStateManagerFW.GetCustomVisualStateManagerFW(stateGroupsRoot);
            if (visualStateManager != null)
                return visualStateManager.GoToStateCore(control, stateGroupsRoot, stateName, group, state, useTransitions);
            if (state != null)
                return VisualStateManagerFW.GoToStateInternal(control, stateGroupsRoot, group, state, useTransitions);
            return false;
        }

        /// <summary>Führt Übergang des Steuerelements von einem Zustand in einen anderen durch.Verwenden Sie diese Methode für Übergänge zwischen Zuständen in Steuerelementen mit <see cref="T:System.Windows.Controls.ControlTemplate" />.</summary>
        /// <returns>true, wenn der Zustand des Steuerelements erfolgreich gewechselt wurde, andernfalls false.</returns>
        /// <param name="control">Das Steuerelement, dessen Zustand gewechselt werden soll. </param>
        /// <param name="stateName">Der Zustand, in den übergegangen wird.</param>
        /// <param name="useTransitions">true, wenn ein <see cref="T:System.Windows.VisualTransition" />-Objekt für den Zustandsübergang verwendet werden soll, andernfalls false.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="control" /> ist null.– oder –<paramref name="stateName" /> ist null.</exception>
        public static bool GoToState(FrameworkElement control, string stateName, bool useTransitions)
        {
            //if (control == null)
                throw new ArgumentNullException(nameof(control));
            //FrameworkElement stateGroupsRoot = control.StateGroupsRoot;
            //return VisualStateManagerFW.GoToStateCommon(control, stateGroupsRoot, stateName, useTransitions);
        }

        /// <summary>Wechselt zwischen zwei Zuständen des Elements.Verwenden Sie diese Methode zum Wechseln von Zuständen, die von einer Anwendung und nicht von einem Steuerelement definiert werden.</summary>
        /// <returns>true, wenn der Zustand des Steuerelements erfolgreich gewechselt wurde, andernfalls false.</returns>
        /// <param name="stateGroupsRoot">Das Stammelement, das den <see cref="T:System.Windows.VisualStateManagerFW" /> enthält.</param>
        /// <param name="stateName">Der Zustand, in den übergegangen wird.</param>
        /// <param name="useTransitions">true, wenn ein <see cref="T:System.Windows.VisualTransition" />-Objekt für den Zustandsübergang verwendet werden soll, andernfalls false.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="stateGroupsRoot" /> ist null.– oder –<paramref name="stateName" /> ist null.</exception>
        public static bool GoToElementState(FrameworkElement stateGroupsRoot, string stateName, bool useTransitions)
        {
            if (stateGroupsRoot == null)
                throw new ArgumentNullException(nameof(stateGroupsRoot));
            return VisualStateManagerFW.GoToStateCommon((FrameworkElement)null, stateGroupsRoot, stateName, useTransitions);
        }

        /// <summary>Wechselt zwischen den Zuständen eines Steuerelements.</summary>
        /// <returns>true, wenn der Zustand des Steuerelements erfolgreich gewechselt wurde, andernfalls false.</returns>
        /// <param name="control">Das Steuerelement, dessen Zustand gewechselt werden soll. </param>
        /// <param name="stateGroupsRoot">Das Stammelement, das den <see cref="T:System.Windows.VisualStateManagerFW" /> enthält.</param>
        /// <param name="stateName">Der Name des Zustands, in den gewechselt werden soll.</param>
        /// <param name="group">Die <see cref="T:System.Windows.VisualStateGroup" />, zu der der Zustand gehört.</param>
        /// <param name="state">Die Darstellung des Zustands, in den gewechselt werden soll.</param>
        /// <param name="useTransitions">true, wenn ein <see cref="T:System.Windows.VisualTransition" />-Objekt für den Zustandsübergang verwendet werden soll, andernfalls false.</param>
        protected virtual bool GoToStateCore(FrameworkElement control, FrameworkElement stateGroupsRoot, string stateName, VisualStateGroup group, VisualState state, bool useTransitions)
        {
            return VisualStateManagerFW.GoToStateInternal(control, stateGroupsRoot, group, state, useTransitions);
        }

        /// <summary>Ruft die angefügte <see cref="P:System.Windows.VisualStateManagerFW.CustomVisualStateManagerFW" />-Eigenschaft ab.</summary>
        /// <returns>Der Manager für den visuellen Zustand, durch den Übergänge zwischen den Zuständen eines Steuerelements ausgeführt werden. </returns>
        /// <param name="obj">Das Element, von dem die angefügte <see cref="P:System.Windows.VisualStateManagerFW.CustomVisualStateManagerFW" />-Eigenschaft abgerufen werden soll.</param>
        public static VisualStateManagerFW GetCustomVisualStateManagerFW(FrameworkElement obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return obj.GetValue(VisualStateManagerFW.CustomVisualStateManagerFWProperty) as VisualStateManagerFW;
        }

        /// <summary>Legt die angefügte <see cref="P:System.Windows.VisualStateManagerFW.CustomVisualStateManagerFW" />-Eigenschaft fest.</summary>
        /// <param name="obj">Das Objekt, für das die Eigenschaft festgelegt werden soll.</param>
        /// <param name="value">Der Manager für den visuellen Zustand, durch den Übergänge zwischen den Zuständen eines Steuerelements ausgeführt werden.</param>
        public static void SetCustomVisualStateManagerFW(FrameworkElement obj, VisualStateManagerFW value)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            obj.SetValue(VisualStateManagerFW.CustomVisualStateManagerFWProperty, (object)value);
        }

        internal static Collection<VisualStateGroup> GetVisualStateGroupsInternal(FrameworkElement obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            bool hasModifiers;
            //if (obj.GetValueSource(VisualStateManagerFW.VisualStateGroupsProperty, (PropertyMetadata)null, out hasModifiers) != BaseValueSourceInternal.Default)
            //    return obj.GetValue(VisualStateManagerFW.VisualStateGroupsProperty) as Collection<VisualStateGroup>;
            return (Collection<VisualStateGroup>)null;
        }

        /// <summary>Ruft die angefügte <see cref="P:System.Windows.VisualStateManagerFW.VisualStateGroups" />-Eigenschaft ab.</summary>
        /// <returns>Die Auflistung von <see cref="T:System.Windows.VisualStateGroup" />-Objekten, die dem angegebenen Objekt zugeordnet ist.</returns>
        /// <param name="obj">Das Element, von dem die angefügte <see cref="P:System.Windows.VisualStateManagerFW.VisualStateGroups" />-Eigenschaft abgerufen werden soll.</param>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public static IList GetVisualStateGroups(FrameworkElement obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return obj.GetValue(VisualStateManagerFW.VisualStateGroupsProperty) as IList;
        }

        internal static bool TryGetState(IList<VisualStateGroup> groups, string stateName, out VisualStateGroup group, out VisualState state)
        {
            for (int index = 0; index < groups.Count; ++index)
            {
                VisualStateGroup group1 = groups[index];
                VisualState state1 = VSMExtensions.GetStateByName(group1, stateName);
                if (state1 != null)
                {
                    group = group1;
                    state = state1;
                    return true;
                }
            }
            group = (VisualStateGroup)null;
            state = (VisualState)null;
            return false;
        }

        private static bool GoToStateInternal(FrameworkElement control, FrameworkElement stateGroupsRoot, VisualStateGroup group, VisualState state, bool useTransitions)
        {
            if (stateGroupsRoot == null)
                throw new ArgumentNullException(nameof(stateGroupsRoot));
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            if (group == null)
                throw new InvalidOperationException();

            VisualState lastState = group.CurrentState;
            if (lastState == state)
                return true;

            VisualTransition transition = useTransitions ? GetTransition(
                stateGroupsRoot, group, lastState, state) : (VisualTransition)null;
            Storyboard transitionAnimations = GenerateDynamicTransitionAnimations(
                stateGroupsRoot, group, state, transition);

            if (transition == null || 
                transition.GeneratedDuration == VisualStateManagerFW.DurationZero && 
                (transition.Storyboard == null || transition.Storyboard.Duration == VisualStateManagerFW.DurationZero))
            {
                if (transition != null && transition.Storyboard != null)
                    group.StartNewThenStopOld(stateGroupsRoot, transition.Storyboard, state.Storyboard);
                else
                    group.StartNewThenStopOld(stateGroupsRoot, state.Storyboard);
                group.RaiseCurrentStateChanging(stateGroupsRoot, lastState, state, control);
                group.RaiseCurrentStateChanged(stateGroupsRoot, lastState, state, control);
            }
            else
            {
                transition.DynamicStoryboardCompleted = false;
                transitionAnimations.Completed += (EventHandler)((sender, e) =>
                {
                    if (transition.Storyboard == null || transition.ExplicitStoryboardCompleted)
                    {
                        if (VisualStateManagerFW.ShouldRunStateStoryboard(control, stateGroupsRoot, state, group))
                            group.StartNewThenStopOld(stateGroupsRoot, state.Storyboard);
                        group.RaiseCurrentStateChanged(stateGroupsRoot, lastState, state, control);
                    }
                    transition.DynamicStoryboardCompleted = true;
                });
                if (transition.Storyboard != null && transition.ExplicitStoryboardCompleted)
                {
                    EventHandler transitionCompleted = (EventHandler)null;
                    transitionCompleted = (EventHandler)((sender, e) =>
                    {
                        if (transition.DynamicStoryboardCompleted)
                        {
                            if (VisualStateManagerFW.ShouldRunStateStoryboard(control, stateGroupsRoot, state, group))
                                group.StartNewThenStopOld(stateGroupsRoot, state.Storyboard);
                            group.RaiseCurrentStateChanged(stateGroupsRoot, lastState, state, control);
                        }
                        transition.Storyboard.Completed -= transitionCompleted;
                        transition.ExplicitStoryboardCompleted = true;
                    });
                    transition.ExplicitStoryboardCompleted = false;
                    transition.Storyboard.Completed += transitionCompleted;
                }
                group.StartNewThenStopOld(stateGroupsRoot, transition.Storyboard, transitionAnimations);
                group.RaiseCurrentStateChanging(stateGroupsRoot, lastState, state, control);
            }
            group.CurrentState() = state;
            return true;
        }

        private static VisualTransition GetTransition(FrameworkElement stateGroupsRoot, VisualStateGroup group, VisualState lastState, VisualState state)
        {
            throw new NotImplementedException();
        }

        [SecurityCritical]
        [SecurityTreatAsSafe]
        private static bool ShouldRunStateStoryboard(FrameworkElement control, FrameworkElement stateGroupsRoot, VisualState state, VisualStateGroup group)
        {
            bool flag1 = true;
            bool flag2 = true;
            //if (control != null && !control.IsVisible)
            //    flag1 = PresentationSource.CriticalFromVisual((DependencyObject)control) != null;
            //if (stateGroupsRoot != null && !stateGroupsRoot.IsVisible)
            //    flag2 = PresentationSource.CriticalFromVisual((DependencyObject)stateGroupsRoot) != null;
            if (flag1 & flag2)
                return state == group.CurrentState;
            return false;
        }

        /// <summary>Löst das <see cref="E:System.Windows.VisualStateGroup.CurrentStateChanging" />-Ereignis für das angegebene <see cref="T:System.Windows.VisualStateGroup" />-Objekt aus.</summary>
        /// <param name="stateGroup">Das Objekt, für das das <see cref="E:System.Windows.VisualStateGroup.CurrentStateChanging" />-Ereignis aufgetreten ist.</param>
        /// <param name="oldState">Der Zustand, aus dem das Steuerelement wechselt.</param>
        /// <param name="newState">Der Zustand, in den das Steuerelement wechselt.</param>
        /// <param name="control">Das Steuerelement, dessen Zustände gewechselt werden.</param>
        /// <param name="stateGroupsRoot">Das Stammelement, das den <see cref="T:System.Windows.VisualStateManagerFW" /> enthält.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="stateGroupsRoot" /> ist null.– oder –<paramref name="newState" /> ist null.</exception>
        protected void RaiseCurrentStateChanging(VisualStateGroup stateGroup, VisualState oldState, VisualState newState, FrameworkElement control, FrameworkElement stateGroupsRoot)
        {
            if (stateGroup == null)
                throw new ArgumentNullException(nameof(stateGroup));
            if (newState == null)
                throw new ArgumentNullException(nameof(newState));
            if (stateGroupsRoot == null)
                return;
            //stateGroup.RaiseCurrentStateChanging(stateGroupsRoot, oldState, newState, control);
        }

        /// <summary>Löst das <see cref="E:System.Windows.VisualStateGroup.CurrentStateChanging" />-Ereignis für das angegebene <see cref="T:System.Windows.VisualStateGroup" />-Objekt aus.</summary>
        /// <param name="stateGroup">Das Objekt, für das das <see cref="E:System.Windows.VisualStateGroup.CurrentStateChanging" />-Ereignis aufgetreten ist.</param>
        /// <param name="oldState">Der Zustand, aus dem das Steuerelement wechselt.</param>
        /// <param name="newState">Der Zustand, in den das Steuerelement wechselt.</param>
        /// <param name="control">Das Steuerelement, dessen Zustände gewechselt werden.</param>
        /// <param name="stateGroupsRoot">Das Stammelement, das den <see cref="T:System.Windows.VisualStateManagerFW" /> enthält.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="stateGroupsRoot" /> ist null.– oder –<paramref name="newState" /> ist null.</exception>
        protected void RaiseCurrentStateChanged(VisualStateGroup stateGroup, VisualState oldState, VisualState newState, FrameworkElement control, FrameworkElement stateGroupsRoot)
        {
            if (stateGroup == null)
                throw new ArgumentNullException(nameof(stateGroup));
            if (newState == null)
                throw new ArgumentNullException(nameof(newState));
            if (stateGroupsRoot == null)
                return;
            //stateGroup.RaiseCurrentStateChanged(stateGroupsRoot, oldState, newState, control);
        }

        private static Storyboard GenerateDynamicTransitionAnimations(
            FrameworkElement root, VisualStateGroup group, VisualState newState, VisualTransition transition)
        {
            IEasingFunction easingFunction = transition?.GeneratedEasingFunction;
            Storyboard storyboard = new Storyboard();
            storyboard.Duration = transition?.GeneratedDuration ?? new Duration(TimeSpan.Zero);

            var dictionary1 = FlattenTimelines(new Collection<Storyboard>()); // = FlattenTimelines(group.CurrentStoryboards);
            var dictionary2 = FlattenTimelines(transition?.Storyboard);
            var dictionary3 = FlattenTimelines(newState.Storyboard);

            foreach (KeyValuePair<VisualStateManagerFW.TimelineDataToken, Timeline> keyValuePair in dictionary2)
            {
                dictionary1.Remove(keyValuePair.Key);
                dictionary3.Remove(keyValuePair.Key);
            }

            foreach (KeyValuePair<VisualStateManagerFW.TimelineDataToken, Timeline> keyValuePair in dictionary3)
            {
                Timeline toAnimation = VisualStateManagerFW.GenerateToAnimation(root, keyValuePair.Value, easingFunction, true);
                if (toAnimation != null)
                {
                    toAnimation.Duration = storyboard.Duration;
                    storyboard.Children.Add(toAnimation);
                }
                dictionary1.Remove(keyValuePair.Key);
            }

            foreach (KeyValuePair<VisualStateManagerFW.TimelineDataToken, Timeline> keyValuePair in dictionary1)
            {
                Timeline fromAnimation = VisualStateManagerFW.GenerateFromAnimation(root, keyValuePair.Value, easingFunction);
                if (fromAnimation != null)
                {
                    fromAnimation.Duration = storyboard.Duration;
                    storyboard.Children.Add(fromAnimation);
                }
            }
            return storyboard;
        }

        private static Timeline GenerateFromAnimation(FrameworkElement root, Timeline timeline, IEasingFunction easingFunction)
        {
            Timeline destination = (Timeline)null;
            if (timeline is ColorAnimation || timeline is ColorAnimationUsingKeyFrames)
                destination = (Timeline)new ColorAnimation()
                {
                    EasingFunction = easingFunction
                };
            else if (timeline is DoubleAnimation || timeline is DoubleAnimationUsingKeyFrames)
                destination = (Timeline)new DoubleAnimation()
                {
                    EasingFunction = easingFunction
                };
            else if (timeline is PointAnimation || timeline is PointAnimationUsingKeyFrames)
                destination = (Timeline)new PointAnimation()
                {
                    EasingFunction = easingFunction
                };
            if (destination != null)
                VisualStateManagerFW.CopyStoryboardTargetProperties(root, timeline, destination);
            return destination;
        }

        private static Timeline GenerateToAnimation(FrameworkElement root, Timeline timeline, IEasingFunction easingFunction, bool isEntering)
        {
            Timeline destination = (Timeline)null;
            Color? targetColor = VisualStateManagerFW.GetTargetColor(timeline, isEntering);
            if (targetColor.HasValue)
                destination = (Timeline)new ColorAnimation()
                {
                    To = targetColor,
                    EasingFunction = easingFunction
                };
            if (destination == null)
            {
                double? targetDouble = VisualStateManagerFW.GetTargetDouble(timeline, isEntering);
                if (targetDouble.HasValue)
                    destination = (Timeline)new DoubleAnimation()
                    {
                        To = targetDouble,
                        EasingFunction = easingFunction
                    };
            }
            if (destination == null)
            {
                Point? targetPoint = VisualStateManagerFW.GetTargetPoint(timeline, isEntering);
                if (targetPoint.HasValue)
                    destination = (Timeline)new PointAnimation()
                    {
                        To = targetPoint,
                        EasingFunction = easingFunction
                    };
            }
            if (destination != null)
                VisualStateManagerFW.CopyStoryboardTargetProperties(root, timeline, destination);
            return destination;
        }

        private static void CopyStoryboardTargetProperties(FrameworkElement root, Timeline source, Timeline destination)
        {
            if (source == null && destination == null)
                return;
            string targetName = Storyboard.GetTargetName((DependencyObject)source);
            DependencyObject dependencyObject = Storyboard.GetTarget((DependencyObject)source);
            PropertyPath targetProperty = Storyboard.GetTargetProperty((DependencyObject)source);
            if (dependencyObject == null && !string.IsNullOrEmpty(targetName))
                dependencyObject = root.FindName(targetName) as DependencyObject;
            if (targetName != null)
                Storyboard.SetTargetName((DependencyObject)destination, targetName);
            if (dependencyObject != null)
                Storyboard.SetTarget((DependencyObject)destination, dependencyObject);
            if (targetProperty == null)
                return;
            Storyboard.SetTargetProperty((DependencyObject)destination, targetProperty);
        }

        private static Color? GetTargetColor(Timeline timeline, bool isEntering)
        {
            ColorAnimation colorAnimation = timeline as ColorAnimation;
            if (colorAnimation != null)
            {
                if (!colorAnimation.From.HasValue)
                    return colorAnimation.To;
                return colorAnimation.From;
            }
            ColorAnimationUsingKeyFrames animationUsingKeyFrames = timeline as ColorAnimationUsingKeyFrames;
            if (animationUsingKeyFrames == null)
                return new Color?();
            if (animationUsingKeyFrames.KeyFrames.Count == 0)
                return new Color?();
            return new Color?(animationUsingKeyFrames.KeyFrames[isEntering ? 0 : animationUsingKeyFrames.KeyFrames.Count - 1].Value);
        }

        private static double? GetTargetDouble(Timeline timeline, bool isEntering)
        {
            DoubleAnimation doubleAnimation = timeline as DoubleAnimation;
            if (doubleAnimation != null)
            {
                if (!doubleAnimation.From.HasValue)
                    return doubleAnimation.To;
                return doubleAnimation.From;
            }
            DoubleAnimationUsingKeyFrames animationUsingKeyFrames = timeline as DoubleAnimationUsingKeyFrames;
            if (animationUsingKeyFrames == null)
                return new double?();
            if (animationUsingKeyFrames.KeyFrames.Count == 0)
                return new double?();
            return new double?(animationUsingKeyFrames.KeyFrames[isEntering ? 0 : animationUsingKeyFrames.KeyFrames.Count - 1].Value);
        }

        private static Point? GetTargetPoint(Timeline timeline, bool isEntering)
        {
            PointAnimation pointAnimation = timeline as PointAnimation;
            if (pointAnimation != null)
            {
                if (!pointAnimation.From.HasValue)
                    return pointAnimation.To;
                return pointAnimation.From;
            }
            PointAnimationUsingKeyFrames animationUsingKeyFrames = timeline as PointAnimationUsingKeyFrames;
            if (animationUsingKeyFrames == null)
                return new Point?();
            if (animationUsingKeyFrames.KeyFrames.Count == 0)
                return new Point?();
            return new Point?(animationUsingKeyFrames.KeyFrames[isEntering ? 0 : animationUsingKeyFrames.KeyFrames.Count - 1].Value);
        }

        private static Dictionary<TimelineDataToken, Timeline> FlattenTimelines(Collection<Storyboard> storyboards)
        {
            var result = new Dictionary<TimelineDataToken, Timeline>();
            foreach (var storyboard in storyboards)
                FlattenTimelines(storyboard, result);
            return result;
        }

        private static Dictionary<TimelineDataToken, Timeline> FlattenTimelines(Storyboard storyboard)
        {
            var result = new Dictionary<TimelineDataToken, Timeline>();
            FlattenTimelines(storyboard, result);
            return result;
        }

        private static void FlattenTimelines(Storyboard storyboard, Dictionary<TimelineDataToken, Timeline> result)
        {
            if (storyboard == null)
                return;
            for (int index = 0; index < storyboard.Children.Count; ++index)
            {
                Timeline child = storyboard.Children[index];
                Storyboard storyboard1 = child as Storyboard;
                if (storyboard1 != null)
                    FlattenTimelines(storyboard1, result);
                else
                    result[new TimelineDataToken(child)] = child;
            }
        }

        private struct TimelineDataToken : IEquatable<VisualStateManagerFW.TimelineDataToken>
        {
            private DependencyObject _target;
            private string _targetName;
            private PropertyPath _targetProperty;

            public TimelineDataToken(Timeline timeline)
            {
                this._target = Storyboard.GetTarget((DependencyObject)timeline);
                this._targetName = Storyboard.GetTargetName((DependencyObject)timeline);
                this._targetProperty = Storyboard.GetTargetProperty((DependencyObject)timeline);
            }

            public bool Equals(TimelineDataToken other)
            {
                if (!this.TargetAndTargetNameEqual(other) ||
                    !this.PropertyPathsEqual(other) ||
                    PropertyPathParamsUnequal(other))
                    return false;

                return other._targetProperty.PathParameters.SequenceEqual(_targetProperty.PathParameters);
            }

            private bool TargetAndTargetNameEqual(TimelineDataToken other)
            {
                if (_targetName == null)
                {
                    if (_target == null)
                    {
                        return other._target == null && other._targetName == null;
                    }
                    else
                    {
                        return other._target == _target;
                    }
                }
                else
                {
                    return other._targetName == _targetName;
                }
            }

            private bool PropertyPathsEqual(TimelineDataToken other)
            {
                return (other._targetProperty.Path == this._targetProperty.Path);
            }

            private bool PropertyPathParamsUnequal(TimelineDataToken other)
            {
                return other._targetProperty.PathParameters.Count != _targetProperty.PathParameters.Count;
            }

            public override int GetHashCode()
            {
                int num1 = this._target != null ? this._target.GetHashCode() : 0;
                int num2 = this._targetName != null ? this._targetName.GetHashCode() : 0;
                int num3 = this._targetProperty == null || this._targetProperty.Path == null ? 0 : this._targetProperty.Path.GetHashCode();
                return (this._targetName != null ? num2 : num1) ^ num3;
            }
        }
    }
}

internal static class VSMExtensions
{
    internal static VisualState GetStateByName(this VisualStateGroup g, string stateName)
    {
        for (int index = 0; index < g.States.Count; ++index)
        {
            VisualState state = (VisualState)g.States[index];
            if (state.Name == stateName)
                return state;
        }
        return (VisualState)null;
    }

    internal static bool IsDefault(this VisualTransition t)
    {
        if (t.From == null) return t.To == null;
        return false;
    }
    
}