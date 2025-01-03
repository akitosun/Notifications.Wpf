using System.Threading.Tasks;
using System.Windows.Media;
using Caliburn.Micro;

namespace Notifications.Wpf.Caliburn.Micro.Sample.ViewModels
{
    public class NotificationViewModel : PropertyChangedBase
    {
        private readonly IToastNotificationService _manager;

        public string Title { get; set; }
        public string Message { get; set; }

        public NotificationViewModel(IToastNotificationService manager)
        {
            _manager = manager;
        }

        public async void Ok()
        {
            await Task.Delay(500);
            _manager.Show(new ToastContent { Title ="Success!", Message = "Ok button was clicked.", Type = ToastNotificationType.Success});
        }

        public async void Cancel()
        {
            await Task.Delay(500);
            _manager.Show(new ToastContent { Title = "Error!",  Message = "Cancel button was clicked!", Type = ToastNotificationType.Error});
        }
    }
}
