namespace Notifications.Wpf
{
    using System;

    /// <summary>
    /// Defines the <see cref="IToastNotificationService" />
    /// </summary>
    public interface IToastNotificationService
    {
        /// <summary>
        /// The Show
        /// </summary>
        /// <param name="content">The content<see cref="ToastContent"/></param>
        /// <param name="onClick">The onClick<see cref="Action"/></param>
        /// <param name="onClose">The onClose<see cref="Action"/></param>
        void Show(IToastContent content, Action onClick = null, Action onClose = null);
    }
}
