namespace Notifications.Wpf
{
    using System;

    /// <summary>
    /// Defines the <see cref="ToastContent" />
    /// </summary>
    public class ToastContent
    {
        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the ContainerName
        /// </summary>
        public string ContainerName { get; set; }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public ToastNotificationType Type { get; set; }

        /// <summary>
        /// Gets or sets the ExpirationTime
        /// </summary>
        public TimeSpan ExpirationTime { get; set; }
    }
}
