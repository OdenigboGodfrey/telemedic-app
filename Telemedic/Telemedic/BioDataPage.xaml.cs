using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Controller;

namespace Telemedic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BioDataPage : ContentPage
	{
		public BioDataPage ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            String BioData = await MedicalPractitionerController.GetBioDetails();
            if (BioData != String.Empty) { _BioContent.Text = BioData; } else _BioContent.Placeholder = "No Bio Data Yet, Please Fill it in";
        }

        private async void  _Submit_Clicked(object sender, EventArgs e)
        {
            bool Result = await MedicalPractitionerController.PostBioDetails(_BioContent.Text);
            if (Result) await DisplayAlert("Alert","Bio Data Updated.","Ok");

            //String BioData = await MedicalPractitionerController.GetBioDetails();
            //if (BioData != String.Empty) { _BioContent.Text = BioData; } else _BioContent.Placeholder = "No Bio Data Yet, Please Fill it in";
        }
    }
}