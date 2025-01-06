namespace Notifications.Wpf
{
    using System;

    /// <summary>
    /// Defines the <see cref="IToastContent" />
    /// </summary>
    public interface IToastContent
    {
        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Gets or sets the ContainerName
        /// </summary>
        string ContainerName { get; set; }

        /// <summary>
        /// Gets or sets the DataTemplateKey
        /// </summary>
        string DataTemplateKey { get; set; }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        ToastNotificationType Type { get; set; }

        /// <summary>
        /// Gets or sets the ExpirationTime
        /// </summary>
        TimeSpan ExpirationTime { get; set; }
    }
}
