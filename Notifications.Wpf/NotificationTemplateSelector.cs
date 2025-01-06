namespace Notifications.Wpf
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="NotificationTemplateSelector" />
    /// </summary>
    public class NotificationTemplateSelector : DataTemplateSelector
    {
        
        private DataTemplate _customDataTemplate;
        /// <summary>
        /// Defines the _defaultNotificationTemplate
        /// </summary>
        private DataTemplate _defaultNotificationTemplate;

        /// <summary>
        /// The GetTemplatesFromResources
        /// </summary>
        /// <param name="container">The container<see cref="FrameworkElement"/></param>
        private void GetTemplatesFromResources(FrameworkElement container)
        {            
            _defaultNotificationTemplate =
                    container?.FindResource("DefaultNotificationTemplate") as DataTemplate;
        }

        /// <summary>
        /// The SelectTemplate
        /// </summary>
        /// <param name="item">The item<see cref="object"/></param>
        /// <param name="container">The container<see cref="DependencyObject"/></param>
        /// <returns>The <see cref="DataTemplate"/></returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (_defaultNotificationTemplate == null)
            {
                GetTemplatesFromResources((FrameworkElement)container);
            }
            if (item is IToastContent toastContent)
            {
                if (toastContent.DataTemplate == null)
                {
                    return _defaultNotificationTemplate;
                }
                return toastContent.DataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
