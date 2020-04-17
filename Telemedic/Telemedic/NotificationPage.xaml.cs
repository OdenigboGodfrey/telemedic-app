using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Telemedic.Controller;
using Telemedic.Templates;
using Telemedic.Templates.Enums;
using System.Collections.Generic;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationPage : ContentPage
	{
        public static Dictionary<int, List<object>> NotificationData = new Dictionary<int, List<object>>();
        

		public NotificationPage ()
		{
			InitializeComponent ();

            _BgImage.Source = Utilities.Source("topbar.jpg", typeof(NotificationPage));

            Utilities.IOSPageFitter(_ParentStack, Device.RuntimePlatform, 0, Utilities.IOSPaddingTop, 0, 0);

            //_NotificationListAll.Children.Add(NotificationTemplate.Notification01("", "SendChat Content MEssage", "09:40pm", NotificationType.Chat, 0));
            //_NotificationListAll.Children.Add(NotificationTemplate.Notification01("", "Reminder Content MEssage", "10:40pm", NotificationType.Reminder, 0));
            //_NotificationListAll.Children.Add(NotificationTemplate.Notification01("", "Reminder Content Message", "011:40pm", NotificationType.Reminder, 0));
        }

        public NotificationPage(int ID)
        {
            InitializeComponent();

            _BgImage.Source = Utilities.Source("topbar.jpg", typeof(NotificationPage));
            Utilities.IOSPageFitter(_ParentStack, Device.RuntimePlatform, 0, Utilities.IOSPaddingTop, 0, 0);

            //_NotificationListAll.Children.Add(NotificationTemplate.Notification01("", "SendChat Content Message", "09:40pm", NotificationType.Chat, 0));
            //_NotificationListAll.Children.Add(NotificationTemplate.Notification01("", "Reminder Content Message", "10:40pm", NotificationType.Reminder, 0));
            //_NotificationListAll.Children.Add(NotificationTemplate.Notification01("", "Reminder Content Message", "011:40pm", NotificationType.Reminder, 0));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await NotificationController.LoadNotifications(NotificationData, _NotificationListAll);
        }
    }
}