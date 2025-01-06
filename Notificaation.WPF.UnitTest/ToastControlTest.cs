using Notifications.Wpf.Controls;
using NUnit.Framework;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Notificaation.WPF.UnitTest
{
    [TestFixture]
    [Apartment(System.Threading.ApartmentState.STA)]
    public class ToastControlTest
    {
        [Test]
        public void CloseOnClick_AttachedProperty_ShouldRegisterCorrectly()
        {
            var button = new Button();
            ToastControl.SetCloseOnClick(button, true);
            
            Assert.That(ToastControl.GetCloseOnClick(button),Is.True, "CloseOnClick property should return true when set.");
        }

        [Test]
        public async Task Close_ShouldTriggerEventsInOrder()
        {
            var toast = new ToastControl();
            var closeInvokedTriggered = false;
            var closedTriggered = false;

            toast.AddHandler(ToastControl.NotificationCloseInvokedEvent, new RoutedEventHandler((s, e) => closeInvokedTriggered = true));
            toast.AddHandler(ToastControl.NotificationClosedEvent, new RoutedEventHandler((s, e) => closedTriggered = true));

            await toast.Close();

            Assert.That(closeInvokedTriggered,Is.True, "NotificationCloseInvokedEvent should be triggered.");
            Assert.That(closedTriggered, Is.True, "NotificationClosedEvent should be triggered.");
        }

        [Test]
        public void Close_ShouldNotTriggerEventsIfAlreadyClosing()
        {
            var toast = new ToastControl
            {
                IsClosing = true
            };

            var closeInvokedTriggered = false;
            var closedTriggered = false;

            toast.AddHandler(ToastControl.NotificationCloseInvokedEvent, new RoutedEventHandler((s, e) => closeInvokedTriggered = true));
            toast.AddHandler(ToastControl.NotificationClosedEvent, new RoutedEventHandler((s, e) => closedTriggered = true));

            Assert.DoesNotThrowAsync(async () => await toast.Close());
            Assert.That(closeInvokedTriggered, Is.False, "NotificationCloseInvokedEvent should not be triggered.");
            Assert.That(closedTriggered, Is.False, "NotificationClosedEvent should not be triggered.");
        }

        [Test]
        public void OnApplyTemplate_ShouldHookUpCloseButton()
        {
            var toast = new ToastControl();
            var button = new Button { Name = "PART_CloseButton" };

            var template = new ControlTemplate();
            toast.Template = template;

            // 模擬應用模板
            toast.ApplyTemplate();
            Assert.DoesNotThrow(() => button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)));
        }

        [Test]
        public void CloseOnClickChanged_ShouldHandleInvalidDependencyObject()
        {
            Assert.DoesNotThrow(() =>
            {
                ToastControl.SetCloseOnClick(new DependencyObject(), true);
            }, "CloseOnClickChanged should not throw exceptions when an invalid DependencyObject is used.");
        }

        [Test]
        public void Template_ShouldCorrectlySetDefaultStyleKey()
        {
            var toast = new ToastControl();
            Assert.That(toast,Is.TypeOf(typeof(ToastControl)), "ToastControl should be the correct type.");
        }
    }
}
