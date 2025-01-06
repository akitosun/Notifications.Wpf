namespace Notifications.Wpf
{
    using Notifications.Wpf.Controls;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// Defines the <see cref="ToastNotificationService" />
    /// </summary>
    public class ToastNotificationService : IToastNotificationService
    {
        /// <summary>
        /// Defines the _dispatcher
        /// </summary>
        private readonly Dispatcher _dispatcher;

        /// <summary>
        /// Defines the Areas
        /// </summary>
        private static readonly List<ToastContainerControl> Areas = new List<ToastContainerControl>();

        /// <summary>
        /// Defines the Window
        /// </summary>
        private static readonly NotificationsOverlayWindow Window = new NotificationsOverlayWindow();

        /// <summary>
        /// Initializes a new instance of the <see cref="ToastNotificationService"/> class.
        /// </summary>
        /// <param name="dispatcher">The dispatcher<see cref="Dispatcher"/></param>
        public ToastNotificationService(Dispatcher dispatcher = null)
        {
            if (dispatcher == null)
            {
                dispatcher = Application.Current?.Dispatcher ?? Dispatcher.CurrentDispatcher;
            }

            _dispatcher = dispatcher;
        }

        /// <summary>
        /// The Show
        /// </summary>
        /// <param name="content">The content<see cref="ToastContent"/></param>
        /// <param name="onClick">The onClick<see cref="Action"/></param>
        /// <param name="onClose">The onClose<see cref="Action"/></param>
        public void Show(IToastContent content, Action onClick = null,
            Action onClose = null)
        {
            if (!_dispatcher.CheckAccess())
            {
                _dispatcher.BeginInvoke(
                    new Action(() => Show(content, onClick, onClose)));
                return;
            }

            if (string.IsNullOrEmpty(content.ContainerName))
            {
                var workArea = SystemParameters.WorkArea;

                Window.Left = workArea.Left;
                Window.Top = workArea.Top;
                Window.Width = workArea.Width;
                Window.Height = workArea.Height;

                Window.Show();
            }

            foreach (var area in Areas.Where(a => a.Name == content.ContainerName))
            {
                area.Show(content, onClick, onClose).RunSynchronously();
            }
        }

        /// <summary>
        /// The AddArea
        /// </summary>
        /// <param name="area">The area<see cref="ToastContainerControl"/></param>
        internal static void AddContainer(ToastContainerControl area)
        {
            Areas.Add(area);
        }
    }
}
