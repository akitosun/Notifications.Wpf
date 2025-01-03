namespace Notifications.Wpf.Controls
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="ToastContainerControl" />
    /// </summary>
    public class ToastContainerControl : Control
    {
        /// <summary>
        /// Gets or sets the Position
        /// </summary>
        public ToastNotificationPosition Position
        {
            get { return (ToastNotificationPosition)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...

        /// <summary>
        /// Defines the PositionProperty
        /// </summary>
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(ToastNotificationPosition), typeof(ToastContainerControl), new PropertyMetadata(ToastNotificationPosition.BottomRight));

        /// <summary>
        /// Gets or sets the MaxItems
        /// </summary>
        public int MaxItems
        {
            get { return (int)GetValue(MaxItemsProperty); }
            set { SetValue(MaxItemsProperty, value); }
        }

        /// <summary>
        /// Defines the MaxItemsProperty
        /// </summary>
        public static readonly DependencyProperty MaxItemsProperty =
            DependencyProperty.Register("MaxItems", typeof(int), typeof(ToastContainerControl), new PropertyMetadata(int.MaxValue));

        /// <summary>
        /// Defines the _items
        /// </summary>
        private IList _items;

        /// <summary>
        /// Defines the _lockObj
        /// </summary>
        private readonly object _lockObj = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ToastContainerControl"/> class.
        /// </summary>
        public ToastContainerControl()
        {
            ToastNotificationService.AddContainer(this);
        }

        /// <summary>
        /// Initializes static members of the <see cref="ToastContainerControl"/> class.
        /// </summary>
        static ToastContainerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToastContainerControl),
                new FrameworkPropertyMetadata(typeof(ToastContainerControl)));
        }

        /// <summary>
        /// The OnApplyTemplate
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var itemsControl = GetTemplateChild("PART_Items") as Panel;
            _items = itemsControl?.Children;
        }

        /// <summary>
        /// The Show
        /// </summary>
        /// <param name="content">The content<see cref="object"/></param>
        /// <param name="expirationTime">The expirationTime<see cref="TimeSpan"/></param>
        /// <param name="onClick">The onClick<see cref="Action"/></param>
        /// <param name="onClose">The onClose<see cref="Action"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task Show(object content, TimeSpan expirationTime, Action onClick, Action onClose)
        {
            var toastControl = new ToastControl
            {
                Content = content
            };

            toastControl.MouseLeftButtonDown += (sender, args) =>
            {
                if (onClick != null)
                {
                    onClick.Invoke();
                    (sender as ToastControl)?.Close();
                }
            };
            toastControl.NotificationClosed += (sender, args) => onClose?.Invoke();
            toastControl.NotificationClosed += OnNotificationClosed;

            if (!IsLoaded)
            {
                return;
            }

            var window = Window.GetWindow(this);
            var x = PresentationSource.FromVisual(window);
            if (x == null)
            {
                return;
            }

            lock (_lockObj)
            {
                _items.Add(toastControl);

                if (_items.OfType<ToastControl>().Count(i => !i.IsClosing) > MaxItems)
                {
                    _items.OfType<ToastControl>().First(i => !i.IsClosing).Close().RunSynchronously();
                }
            }

            if (expirationTime == TimeSpan.MaxValue)
            {
                return;
            }
            await Task.Delay(expirationTime);
            toastControl.Close().RunSynchronously();
        }

        /// <summary>
        /// The OnNotificationClosed
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="routedEventArgs">The routedEventArgs<see cref="RoutedEventArgs"/></param>
        private void OnNotificationClosed(object sender, RoutedEventArgs routedEventArgs)
        {
            var notification = sender as ToastControl;
            _items.Remove(notification);
        }
    }
}
