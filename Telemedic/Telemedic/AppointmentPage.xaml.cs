using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Templates.Enums;

namespace Telemedic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppointmentPage : ContentPage
	{
		public AppointmentPage ()
		{
			InitializeComponent ();
            icon_doctor.Source = Utilities.Source("doctor_icon.png",typeof(AppointmentPage) );
            icon_pharmacy.Source = Utilities.Source("pharmacy_icon.png",typeof(AppointmentPage) );
            icon_lab.Source = Utilities.Source("laboratory_icon.png",typeof(AppointmentPage) );
            icon_hospital.Source = Utilities.Source("hospital_icon.png",typeof(AppointmentPage) );
		}

        private void TapGestureRecognizerDoc(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchDoctors());
        }

        private void TapGestureRecognizerPhar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchPharmacyList());
        }

        private void TapGestureRecognizerLab(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchLabList());
        }

        private void TapGestureRecognizerHos(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchHospitalList());
        }
    }
}