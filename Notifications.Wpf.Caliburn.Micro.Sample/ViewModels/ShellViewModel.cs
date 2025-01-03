﻿using System;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;

namespace Notifications.Wpf.Caliburn.Micro.Sample.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        private readonly IToastNotificationService _manager;

        public ShellViewModel(IToastNotificationService manager)
        {
            _manager = manager;
        }

        public void Show()
        {
            var content = new NotificationViewModel(_manager)
            {
                Title = "Custom notification.",
                Message = "Click on buttons!"
            };

            _manager.Show(content, expirationTime: TimeSpan.FromSeconds(30));          
        }

        public void ShowInWindow()
        {
            _manager.Show(new ToastContent { Title ="Message", Message = "Message in window"}, areaName: "WindowArea");
        }
    }
}