using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Templates.Enums;
using Telemedic.Controller;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BookAppointmentDoctor : ContentPage
	{
        int MPID;
        MedicalPractitionerType MPType;

        BookAppointmentDetails BAD;

        public BookAppointmentDoctor ()
		{
			InitializeComponent ();
		}

        public BookAppointmentDoctor(int MPID, MedicalPractitionerType MPType, ImageSource MPImage, String MedPractName, String Address, String BookingTime)
        {
            try
            {
                InitializeComponent();
                _CountryCode.Items.Add("+234");
                _CountryCode.Items.Add("+235");
                _CountryCode.Items.Add("+236");
                _CountryCode.SelectedIndex = 0;

                dp_doctor.Source = MPImage;
                this.MPID = MPID;
                this.MPType = MPType;
            }
            catch (Exception ex) { DisplayAlert("Alert", ex.Message, "Ok"); }

        }

        public BookAppointmentDoctor(BookAppointmentDetails BAD)
        {
            try
            {
                InitializeComponent();
                _CountryCode.Items.Add("+234");
                _CountryCode.Items.Add("+235");
                _CountryCode.Items.Add("+236");
                _CountryCode.SelectedIndex = 0;

                dp_doctor.Source = BAD.MPImage;
                _Address.Text = BAD.Address;
                _WorkTime.Text = BAD.BookingTime;
                _Name.Text = BAD.MedPractName;
                this.BAD = BAD;

            }
            catch (Exception ex) { DisplayAlert("Alert", ex.Message, "Ok"); }

        }

        

        private async void HandleBooking(object sender, EventArgs e)
        {
            try
            {
                //_Description
                //if (_Description.Text == String.Empty)
                //{
                //    await DisplayAlert("Alert", pickTime.Time.ToString(), "ok");
                //}
                //else
                //{
                //    await DisplayAlert("Alert", pickTime.Time.ToString(_Description.Text), "ok");
                //}
                DateTime BeginWorkingHours = DateTime.Parse(_WorkTime.Text.Split('-')[0]);
                DateTime EndWorkingHours = DateTime.Parse(_WorkTime.Text.Split('-')[1]);

                if (pickTime.Time.Hours >= BeginWorkingHours.Hour && pickTime.Time.Hours < EndWorkingHours.Hour)
                {
                    bool xx = await AppointmentController.BookAppointment(Utilities.ID, pickDate.Date.ToString("yyyy-MM-dd"), pickTime.Time, MPType, BAD.MPID, _Description.Text, _CountryCode.SelectedItem + _UserNo.Text);

                    if (xx) await Utilities.CreateAlertDialog("Alert", "Appointment Booked", "Okay", delegate
                    {
                        Navigation.PushAsync(new AppointmentHistoryPage(0, Utilities.IsMedic));
                        Navigation.RemovePage(this);
                    });
                }
                else
                {
                    await DisplayAlert("Alert", "Please book a time between the shown working hours.", "ok");
                }
                
            }
            catch(Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "ok");
            }


        }
    }
}