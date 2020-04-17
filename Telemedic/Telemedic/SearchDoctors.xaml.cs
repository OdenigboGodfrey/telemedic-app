using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchDoctors : ContentPage
	{
		public SearchDoctors ()
		{
			InitializeComponent ();

            icon_home_care.Source = Utilities.Source("generalmedicine10.png", typeof(SearchDoctors));
            icon_paediatrics.Source = Utilities.Source("paediatrics2.png", typeof(SearchDoctors));
            icon_dem.Source = Utilities.Source("dermatologist3.png", typeof(SearchDoctors));
            icon_sexiologist.Source = Utilities.Source("sexiologist4.png", typeof(SearchDoctors));
            icon_dentistry.Source = Utilities.Source("dentistry5.png", typeof(SearchDoctors));
            icon_mental_health.Source = Utilities.Source("mentalhealth6.png", typeof(SearchDoctors));
            icon_eye_care.Source = Utilities.Source("eyecare7.png", typeof(SearchDoctors));
            icon_gynecologist.Source = Utilities.Source("gynecologist11.png", typeof(SearchDoctors));
            icon_radiologist.Source = Utilities.Source("radiologist14.png", typeof(SearchDoctors));
            icon_urgent_care.Source = Utilities.Source("urgentcare8.png", typeof(SearchDoctors));
            icon_orthopedic.Source = Utilities.Source("orthopedic12.png", typeof(SearchDoctors));
            icon_homeoparthy.Source = Utilities.Source("homeoparthy13.png", typeof(SearchDoctors));
            icon_general_medicine.Source = Utilities.Source("generalmedicine10.png", typeof(SearchDoctors));
            icon_infectious_disease.Source = Utilities.Source("infectiousdiseases9.png", typeof(SearchDoctors));
            icon_otologist.Source = Utilities.Source("otologist15.png", typeof(SearchDoctors));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchDoctorsList());
        }
    }
}