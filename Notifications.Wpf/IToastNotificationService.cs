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
        /// <param name="content">The content<see cref="object"/></param>
        /// <param name="areaName">The areaName<see cref="string"/></param>
        /// <param name="expirationTime">The expirationTime<see cref="TimeSpan?"/></param>
        /// <param name="onClick">The onClick<see cref="Action"/></param>
        /// <param name="onClose">The onClose<see cref="Action"/></param>
        void Show(object content, string areaName = "", TimeSpan? expirationTime = null, Action onClick = null, Action onClose = null);
    }
}
