using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Telemedic.Templates;
using Telemedic.Templates.Enums;
using Telemedic.Controller;

namespace Telemedic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WaitingRoom : ContentPage
	{
		public WaitingRoom ()
		{
			InitializeComponent ();

            _BgImage.Source = Utilities.Source("topbar.jpg", typeof(WaitingRoom));
            DoLoad();
            Utilities.IOSPageFitter(_ParentStack, Device.RuntimePlatform, 0, Utilities.IOSPaddingTop, 0, 0);
        }

        public WaitingRoom(int ID)
        {
            InitializeComponent();
            DoLoad();
            Utilities.IOSPageFitter(_ParentStack, Device.RuntimePlatform, 0, Utilities.IOSPaddingTop, 0, 0);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await AppointmentController.WaitingList(_WaitingStack);
        }


        private void DoLoad()
        {
            //_WaitingStack.Children.Add(WaitingRoomTemplate.WaitingTemplate02(0, "Silas Ogar", Status.Approved,"8:00 AM","", Utilities.Source("doc_anim.jpg", typeof(WaitingRoomTemplate))));
            //_WaitingStack.Children.Add(WaitingRoomTemplate.WaitingTemplate02(0, "Silas Ogar Jnr", Status.Declined, "8:30 AM", "", Utilities.Source("doc_anim.jpg", typeof(WaitingRoomTemplate))));
            //_WaitingStack.Children.Add(WaitingRoomTemplate.WaitingTemplate02(0, "Silas Ogar Snr", Status.Waiting, "9:00 AM", "", Utilities.Source("doc_anim.jpg", typeof(WaitingRoomTemplate))));
        }
    }
}