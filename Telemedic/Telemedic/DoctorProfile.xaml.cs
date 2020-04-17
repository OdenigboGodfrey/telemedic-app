using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DoctorProfile : ContentPage
	{
		public DoctorProfile ()
		{
			InitializeComponent ();
            dp_doctor.Source = Utilities.Source("doctor_ogar.JPG", typeof(DoctorProfile));
        }

        public DoctorProfile(int ID, int DoctorID, double Location, double Stars, int Fee, String WorkTime)
        {
            InitializeComponent();
            dp_doctor.Source = Utilities.Source("doctor_ogar.JPG", typeof(DoctorProfile));
            Utilities.CreateStars(_StarsStack, Stars, typeof(DoctorProfile));
            _Fee.Text = Fee + Utilities.Currency + "/Session";
            _WorkingTime.Text = WorkTime;
            _Location.Text = Location + Utilities.DistanceType;
            _ClockIcon.Source = Utilities.Source("ic_access_time_ash.png", typeof(DoctorProfile));
            _DollarIcon.Source = Utilities.Source("ic_monetizaion_on_ash.png", typeof(DoctorProfile));
        }
    }
}