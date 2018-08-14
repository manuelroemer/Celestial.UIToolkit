using System;
using System.Diagnostics;

namespace Celestial.UIToolkit.Common
{

    /// <summary>
    /// An abstract base class for a singleton.
    /// When deriving from this class, specify your class as this class' type parameter
    /// and ensure that the deriving class has no publicly visible constructors.
    /// </summary>
    /// <typeparam name="T">
    /// The type of class which is deriving from this class.
    /// </typeparam>
    [DebuggerStepThrough]
    public abstract class Singleton<T>
    {

        private static T _instance;
        
        /// <summary>
        /// Gets the single instance of the singleton.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)Activator.CreateInstance(typeof(T), true);
                }
                return _instance;
            }
        }
        
    }

}
