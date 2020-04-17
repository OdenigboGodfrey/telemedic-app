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
	public partial class SearchLabList : ContentPage
	{
		public SearchLabList ()
		{
			InitializeComponent ();
            //_ParentStack.Children.Add(MedPractListTemplate.ListTemplate01(0, MedicalPractitionerType.Laboratory, "St. John's Lab", "Carry out any test, even Spiritual test!", 5.0, Utilities.Source("icon_lab.jpg", typeof(SearchLabList)), 4.0, "30 Wethral Rd, Owerri, Imo State."));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (CrossConnectivity.IsSupported && CrossConnectivity.Current.IsConnected)
            {
                await AppointmentController.ListMedicalPractioner(_ParentStack, MedicalPractitionerType.Laboratory);
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
                await AppointmentController.ListMedicalPractioner(_ParentStack, MedicalPractitionerType.Laboratory);
            }
        }

        private async Task HandleSearch()
        {
            await AppointmentController.SearchMedicalPractioner(_MedPractName.Text, MedicalPractitionerType.Laboratory, _ParentStack);
        }

        private void OpenProfile(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LabProfile());
        }

        private void _MedPractName_Focused(object sender, FocusEventArgs e)
        {
            //_ParentStack.Children.Add(MedPractListTemplate.ListTemplate01(0, MedicalPractitionerType.Laboratory, "St. John's Lab", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", 4.5, Utilities.Source("icon_lab.jpg", typeof(SearchLabList)), 4.0, "30 Wethral Rd, Owerri, Imo State."));
        }
    }
}