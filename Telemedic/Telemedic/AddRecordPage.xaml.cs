using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Controller;
using Telemedic.Templates.Enums;

namespace Telemedic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddRecordPage : ContentPage
	{
        RecordType UserRecordType;
        int ID;

        public AddRecordPage ()
		{
			InitializeComponent ();
		}

        public AddRecordPage(int ID,RecordType UserRecordType)
        {
            InitializeComponent();

            this.ID = ID;
            this.UserRecordType = UserRecordType;
        }

        private async void _Submit_Clicked(object sender, EventArgs e)
        {
            bool Saved = await MedicalRecordController.PostMedicalRecords(_RecordContent.Text, _RecordTitle.Text, ID, UserRecordType);

            if (Saved)
            {
                await Utilities.CreateAlertDialog("Alert", "Record Saved", "Ok", delegate
                {
                    Navigation.PopAsync();
                });

            }
            else
            {
                await DisplayAlert("Alert", "Failed to save record.", "Ok");
            }
        }
    }
}