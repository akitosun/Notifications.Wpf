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
        /// Defines the _window
        /// </summary>
        private static NotificationsOverlayWindow _window;

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
        /// <param name="content">The content<see cref="object"/></param>
        /// <param name="areaName">The areaName<see cref="string"/></param>
        /// <param name="expirationTime">The expirationTime<see cref="TimeSpan?"/></param>
        /// <param name="onClick">The onClick<see cref="Action"/></param>
        /// <param name="onClose">The onClose<see cref="Action"/></param>
        public void Show(object content, string areaName = "", TimeSpan? expirationTime = null, Action onClick = null,
            Action onClose = null)
        {
            if (!_dispatcher.CheckAccess())
            {
                _dispatcher.BeginInvoke(
                    new Action(() => Show(content, areaName, expirationTime, onClick, onClose)));
                return;
            }

            if (expirationTime == null) expirationTime = TimeSpan.FromSeconds(5);

            if (areaName == string.Empty && _window == null)
            {
                var workArea = SystemParameters.WorkArea;

                _window = new NotificationsOverlayWindow
                {
                    Left = workArea.Left,
                    Top = workArea.Top,
                    Width = workArea.Width,
                    Height = workArea.Height
                };

                _window.Show();
            }

            foreach (var area in Areas.Where(a => a.Name == areaName))
            {
                area.Show(content, (TimeSpan)expirationTime, onClick, onClose);
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
