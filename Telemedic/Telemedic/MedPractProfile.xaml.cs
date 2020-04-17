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
	public partial class MedPractProfile : ContentPage
	{
        MedPractProfileDetails MPPD;

        public MedPractProfile()
        {
            InitializeComponent();
        }

        public MedPractProfile(MedPractProfileDetails MPPD)
        {
            InitializeComponent();
            dp_doctor.Source = Utilities.Source("doc_anim.jpg", typeof(DoctorProfile));

            Utilities.CreateStars(_StarsStack, MPPD.Stars, typeof(DoctorProfile));

            _DoctorName.Text = MPPD.MPName;
            _Address.Text = MPPD.Address;
            _Description.Text = MPPD.Description;
            _Fee.Text = MPPD.Fee + Utilities.Currency + "/Session";
            _WorkingTime.Text = MPPD.WorkTime;
            _Location.Text = MPPD.Location + Utilities.DistanceType;
            _ClockIcon.Source = Utilities.Source("ic_access_time_ash.png", typeof(DoctorProfile));
            _DollarIcon.Source = Utilities.Source("ic_monetizaion_on_ash.png", typeof(DoctorProfile));
            

            this.MPPD = MPPD;

        }

        private void BtnBook_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new BookAppointmentDoctor(new BookAppointmentDetails(MPPD.MPID, MPPD.MPType, MPPD.MPImage, MPPD.MPName, MPPD.Address, MPPD.WorkTime)));
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", ex.Message, "ok");
            }

        }
    }
}