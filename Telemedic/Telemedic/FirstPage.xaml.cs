using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FirstPage : ContentPage
	{
        Map DefaultMap = new Map
        {
            IsShowingUser = true,
            MapType = MapType.Street,
        };

        public FirstPage ()
		{
			InitializeComponent ();
            DefaultMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(6.5244, 3.3792), Distance.FromMiles(1.0)
                    )
                );
            //Map.IsShowingUser = true;
        }

        public FirstPage(int ID)
        {
            InitializeComponent();
        }

        

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var MapPermissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (MapPermissionStatus != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                {
                    await DisplayAlert("Alert", "Location needed for application use.", "OK");
                }

                var Results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                MapPermissionStatus = Results[Permission.Location];
            }

            if (MapPermissionStatus == PermissionStatus.Granted)
            {
                //var results = await CrossGeolocator.Current.GetPositionAsync(10000);
                //LabelGeolocation.Text = "Lat: " + results.Latitude + " Long: " + results.Longitude;

                 _MapGrid.Children.Add(DefaultMap);

                try
                {
                    var locator = CrossGeolocator.Current;
                    var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(60));
                    DefaultMap.MoveToRegion(
                        MapSpan.FromCenterAndRadius(
                            new Position(position.Latitude, position.Longitude), Distance.FromKilometers(8.0)
                            )
                        );

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alert", ex.Message, "Okay");
                }
            }
            else if (MapPermissionStatus != PermissionStatus.Unknown)
            {
                await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
            }

            
            Utilities.IOSPageFitter(_ParentGrid, Device.RuntimePlatform, 0, Utilities.IOSPaddingTop, 0, 0);

            // Add your pins here
            
        }

        private void HandleEmergency(object sender, EventArgs e)
        {
            DisplayAlert("Alert", "Still working...", "OK");
        }

        private void HandleBookings(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AppointmentPage());
        }

        private void _Doctor_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchDoctorsList());
        }

        private void _Pharmacy_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchPharmacyList());
        }

        private void _Lab_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchLabList());
        }

        private void _Hospital_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchHospitalList());
        }
    }
}