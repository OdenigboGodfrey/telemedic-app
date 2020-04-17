using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Controller;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MedPractWorkSettings : ContentPage
	{
		public MedPractWorkSettings ()
		{
			InitializeComponent ();
		}

        public MedPractWorkSettings(int ID)
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Dictionary<String, String> Data = await MedicalPractitionerController.GetProfileDetails();

            String[] WorkingTime = Data["WorkingHours"].Split('-');

            _Price.Placeholder = Data["Charge"];

            _StartTime.Time = DateTime.Parse(WorkingTime[0]).TimeOfDay;
            _EndTime.Time = DateTime.Parse(WorkingTime[1]).TimeOfDay;

            _AccountName.Placeholder = (Data["AccountName"].Contains("n/a")) ? "Account Name " + Data["AccountName"] : Data["AccountName"];
            _AccountNumber.Placeholder = (Data["AccountNumber"] == "00000XXXXX") ? "Account Number " + Data["AccountNumber"] : Data["AccountNumber"];
            _BankName.Placeholder = (Data["BankName"] == "n/a") ? "Bank Name " + Data["BankName"] : Data["BankName"];
        }

        private async void _Submit_Clicked(object sender, EventArgs e)
        {
            /**do save, if entry is left blank, submit the placeholder
             * if entry is n/a submit back n/a to the DB
             * **/
            bool Result = await MedicalPractitionerController.PostProfileDetails((String.IsNullOrEmpty(_AccountNumber.Text) || String.IsNullOrWhiteSpace(_AccountNumber.Text)) ? _AccountNumber.Placeholder.Replace("Account Number", "") : _AccountNumber.Text, (String.IsNullOrEmpty(_Price.Text) || String.IsNullOrWhiteSpace(_Price.Text) ? _Price.Placeholder : _Price.Text), (String.IsNullOrEmpty(_BankName.Text) || String.IsNullOrWhiteSpace(_BankName.Text)) ? _BankName.Placeholder.Replace("Bank Name", "") : _BankName.Text, (String.IsNullOrEmpty(_AccountName.Text) || String.IsNullOrWhiteSpace(_AccountName.Text)) ? _AccountName.Placeholder.Replace("Account Name", "") : _AccountName.Text, Utilities.TimeIn12Hours(_StartTime.Time) + " - " + Utilities.TimeIn12Hours(_EndTime.Time));

            if (Result) await Navigation.PushAsync(new MedPractWorkSettingsDisplay(Convert.ToInt32((String.IsNullOrEmpty(_Price.Text) || String.IsNullOrWhiteSpace(_Price.Text)) ? _Price.Placeholder : _Price.Text), Utilities.TimeIn12Hours(_StartTime.Time) + " - " + Utilities.TimeIn12Hours(_EndTime.Time), (String.IsNullOrEmpty(_AccountName.Text) || String.IsNullOrWhiteSpace(_AccountName.Text)) ? _AccountName.Placeholder.Replace("Account Name", "") : _AccountName.Text, (String.IsNullOrEmpty(_BankName.Text) || String.IsNullOrWhiteSpace(_BankName.Text)) ? _BankName.Placeholder.Replace("Bank Name", "") : _BankName.Text, (String.IsNullOrEmpty(_AccountNumber.Text) || String.IsNullOrWhiteSpace(_AccountNumber.Text)) ? _AccountNumber.Placeholder.Replace("Account Number", "") : _AccountNumber.Text));
            
        }
    }
}