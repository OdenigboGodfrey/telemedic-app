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
using Plugin.Connectivity;

namespace Telemedic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchDoctorsList : ContentPage
	{
        public SearchDoctorsList()
        {
            InitializeComponent();
            //_ParentStack.Children.Add(MedPractListTemplate.ListTemplate02(0,MedicalPractitionerType.Doctor,"Dr Silas Ogar", "I have grown in my profession to accept nothing less than necessary", 3.5,Utilities.Source("doc_anim.jpg", typeof(SearchDoctorsList)),4.0, "30 Wethral Rd, Owerri, Imo State."));
            //_ParentStack.Children.Add(MedPractListTemplate.ListTemplate02(0, MedicalPractitionerType.Doctor, "Dr Silas Ogar", "I have grown in my profession to accept nothing less than necessary", 4.5, Utilities.Source("doc_anim.jpg", typeof(SearchDoctorsList)), 4.0, "30 Wethral Rd, Owerri, Imo State."));
            //_ParentStack.Children.Add(MedPractListTemplate.ListTemplate02(0, MedicalPractitionerType.Doctor, "Dr Silas Ogar", "I have grown in my profession to accept nothing less than necessary", 3.0, Utilities.Source("doc_anim.jpg", typeof(SearchDoctorsList)), 4.0, "30 Wethral Rd, Owerri, Imo State."));
            //_ParentStack.Children.Add(MedPractListTemplate.ListTemplate02(0, MedicalPractitionerType.Doctor, "Dr Silas Ogar", "I have grown in my profession to accept nothing less than necessary", 5.0, Utilities.Source("doc_anim.jpg", typeof(SearchDoctorsList)), 4.0, "30 Wethral Rd, Owerri, Imo State."));
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (CrossConnectivity.IsSupported && CrossConnectivity.Current.IsConnected)
            {
                await AppointmentController.ListMedicalPractioner(_ParentStack, MedicalPractitionerType.Doctor);
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
                await AppointmentController.ListMedicalPractioner(_ParentStack, MedicalPractitionerType.Doctor);
            }
        }

        private async Task HandleSearch()
        {
            await AppointmentController.SearchMedicalPractioner(_MedPractName.Text, MedicalPractitionerType.Doctor, _ParentStack);
        }

        private void _MedPractName_Focused(object sender, FocusEventArgs e)
        {
            //_ParentStack.Children.Add(MedPractListTemplate.ListTemplate01(0, MedicalPractitionerType.Doctor, "Dr Silas Ogar", "I have grown in my profession to accept nothing less than necessary", 4.5, Utilities.Source("doc_anim.jpg", typeof(SearchDoctorsList)), 4.0, "30 Wethral Rd, Owerri, Imo State."));
        }
    }
}