using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Controller;
using Plugin.Connectivity;
using Telemedic.Model.Local;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        bool IsMedic = Utilities.IsMedic;
        public SettingsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    //Utilities.AuthControlHider()
                    _ForIOS.IsVisible = false;
                    break;
                case Device.iOS:
                    _ForUWP.IsVisible = true;
                    _ForAndroid.IsVisible = false;
                    break;
                case Device.UWP:
                    _ForUWP.IsVisible = true;
                    _ForAndroid.IsVisible = false;
                    break;
            };

            _BgImage.Source = Utilities.Source("topbar.jpg", typeof(SettingsPage));
            _ProfilePic.Source = Utilities.Source("doc_anim.jpg", typeof(SettingsPage));
            _AndroidProfilePic.Source = Utilities.Source("doc_anim.jpg", typeof(SettingsPage));
            _MessagesIcon.Source = Utilities.Source("ic_message.png", typeof(SettingsPage));
            _PhoneNoIcon.Source = Utilities.Source("ic_local_phone_ash.png", typeof(SettingsPage));
            _UsernameIcon.Source = Utilities.Source("ic_person_outline.png", typeof(SettingsPage));
            _FullNameIcon.Source = Utilities.Source("ic_settings.png", typeof(SettingsPage));
            _DOBIcon.Source = Utilities.Source("ic_date_range.png", typeof(SettingsPage));
            _2faIcon.Source = Utilities.Source("ic_lock.png", typeof(SettingsPage));
            _ChangePasswordIcon.Source = Utilities.Source("ic_lock.png", typeof(SettingsPage));
            _LocationIcon.Source = Utilities.Source("baseline_location_on_black_48dp.png", typeof(SettingsPage));

            Utilities.IOSPageFitter(_ParentStack, Device.RuntimePlatform, 0, Utilities.IOSPaddingTop, 0, 0);


            /**bool IsMedic determines which control would be shown to the user by hiding elements of a certain group class**/

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
                _PhoneNo.Text = Result["phone"];
                _FullName.Text = Result["name"];
                _DOB.Text = Result["dob"];
                _Email.Text = Result["email"];
            }
            else
            {
                var Result = await UserDetailsHandler.GetUserDetails();

                _Username.Text = Result.FullName;
                _UserAddress.Text = Result.Address;
                _PhoneNo.Text = Result.PhoneNo;
                _FullName.Text = Result.FullName;
                _DOB.Text = Result.DOB;
                _Email.Text = Result.Email;
            }
        }

        public void UpdateProfileLabelTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UpdateProfile());
        }

        public void BioDataTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BioDataPage());
        }

        public void SetSpecialityAndHospitalName(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HospitalBasicDetailsPage(Utilities.ID));
        }

        public void SetTimeAndBankDetails(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MedPractWorkSettings(Utilities.ID));
        }

        private void _OnlineForum_Tapped(object sender, EventArgs e)
        {

        }

        private void ChangePasswordTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePasswordPage(Utilities.ID));
        }

        private void _BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}