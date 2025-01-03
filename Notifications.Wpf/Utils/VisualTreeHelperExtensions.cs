namespace Notifications.Wpf.Utils
{
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Defines the <see cref="VisualTreeHelperExtensions" />
    /// </summary>
    internal class VisualTreeHelperExtensions
    {
        /// <summary>
        /// The GetParent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="child">The child<see cref="DependencyObject"/></param>
        /// <returns>The <see cref="T"/></returns>
        public static T GetParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);

            if (parent == null) return null;

            var tParent = parent as T;
            if (tParent != null)
            {
                return tParent;
            }

            return GetParent<T>(parent);
        }
    }
}
