using Plugin.Connectivity;
using System;
using Telemedic.Controller;
using Telemedic.Model.Local;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
        bool IsMedic = Utilities.IsMedic;
        TabbedPage ParentPage;

        public ProfilePage ()
		{
			InitializeComponent ();

            ParentPage = this.Parent as TabbedPage;

            /** if method which controls the display of image in the profilepage**/
            if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.iOS)
            {
                _ForUWP.IsVisible = true;
                _ForAndroid.IsVisible = false;
            }

            _BgImage.Source = Utilities.Source("topbar.jpg", typeof(ProfilePage));
            _ProfilePic.Source = Utilities.Source("IMG_5204.JPG", typeof(ProfilePage));
            _AndroidProfilePic.Source = Utilities.Source("doc_anim.jpg", typeof(ProfilePage));
            _MedicalHistoryIcon.Source = Utilities.Source("ic_explore.png", typeof(ProfilePage));
            _SettingsIcon.Source = Utilities.Source("ic_settings.png", typeof(ProfilePage));
            _HelpIcon.Source = Utilities.Source("ic_help.png", typeof(ProfilePage));
            _AboutIcon.Source = Utilities.Source("ic_info.png", typeof(ProfilePage));
            _LocationIcon.Source = Utilities.Source("baseline_location_on_black_48dp.png", typeof(ProfilePage));

            if (!IsMedic)
            {
                /**hide medpract elements from normal user**/
                Utilities.AuthControlHider(_ParentStack, "For-Doctor");
            }
            else
            {
                Utilities.AuthControlHider(_ParentStack, "For-User");
            }

            Utilities.IOSPageFitter(_ParentStack, Device.RuntimePlatform, 0, Utilities.IOSPaddingTop, 0, 0);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (CrossConnectivity.IsSupported && CrossConnectivity.Current.IsConnected)
            {
                var Result = await SettingsController.GetSettings();

                _Username.Text = Result["name"];
                _UserAddress.Text = Result["address"];
            }
            else
            {
                var Result = await UserDetailsHandler.GetUserDetails();

                _Username.Text = Result.FullName;
                _UserAddress.Text = Result.Address;
            }
        }

        public void UpdateProfileLabelTapped(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new UpdateProfile());
        }

        public void MessageGridTapped(object sender, EventArgs e)
        {
            
            ParentPage.CurrentPage = ParentPage.Children[1];

        }

        public void WalletGridTapped(object sender, EventArgs e)
        {
            ParentPage.CurrentPage = ParentPage.Children[2];
        }

        public void NotificationGridTapped(object sender, EventArgs e)
        {
            ParentPage.CurrentPage = ParentPage.Children[3];
        }

        public void MedicalHistoryGridTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MedicalHistoryPage(0,false,0));
        }

        private void SettingsLabelTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }

        //
        private void _OnlineForum_Tapped(object sender, EventArgs e)
        {
            //handle forum stuffs
        }

        private void _AppointmentHistory_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AppointmentHistoryPage(1,Utilities.IsMedic));
        }

        private void _WaitingList_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WaitingRoom(1));
        }


    }
}