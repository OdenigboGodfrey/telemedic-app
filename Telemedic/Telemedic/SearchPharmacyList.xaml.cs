using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Templates;
using Telemedic.Templates.Enums;
using Telemedic.Controller;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPharmacyList : ContentPage
	{
		public SearchPharmacyList ()
		{
			InitializeComponent ();
            //dp_pharm.Source = Utilities.Source("icon_pharmacy.jpg", typeof(SearchPharmacyList));
            //dp_pharm1.Source = Utilities.Source("icon_pharmacy.jpg", typeof(SearchPharmacyList));
            //dp_pharm2.Source = Utilities.Source("icon_pharmacy.jpg", typeof(SearchPharmacyList));
            //dp_pharm3.Source = Utilities.Source("icon_pharmacy.jpg", typeof(SearchPharmacyList));

            //_ParentStack.Children.Add(MedPractListTemplate.ListTemplate01(0, MedicalPractitionerType.Pharmacy, "Pina Pharmacy", "Provide high quality drugs at low rates", 4.5, Utilities.Source("icon_pharmacy.jpg", typeof(SearchPharmacyList)), 4.0, "30 Wethral Rd, Owerri, Imo State."));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (CrossConnectivity.IsSupported && CrossConnectivity.Current.IsConnected)
            {
                await AppointmentController.ListMedicalPractioner(_ParentStack, MedicalPractitionerType.Pharmacy);
            }
            else
            {
                _ParentStack.Children.Add(new Label { Text = "No internet connection, please try again when there is an active connection.", Style = App.Current.Resources["_EmptyLabelTemplate"] as Style });
            }
        }

        private async void _MedPractName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_MedPractName.Text.Length >= 3)
            {
                await HandleSearch();
            }
            else
            {
                await AppointmentController.ListMedicalPractioner(_ParentStack, MedicalPractitionerType.Pharmacy);
            }
        }

        private async Task HandleSearch()
        {
            await AppointmentController.SearchMedicalPractioner(_MedPractName.Text, MedicalPractitionerType.Pharmacy, _ParentStack);
        }

        private void OpenProfile(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HospitalProfile());
        }

        private void _MedPractName_Focused(object sender, FocusEventArgs e)
        {
            //_ParentStack.Children.Add(MedPractListTemplate.ListTemplate01(0, MedicalPractitionerType.Pharmacy, "Pina Pharmacy", "Provide high quality drugs at low rates", 4.5, Utilities.Source("icon_pharmacy.jpg", typeof(SearchPharmacyList)), 4.0, "30 Wethral Rd, Owerri, Imo State."));
        }
    }
}