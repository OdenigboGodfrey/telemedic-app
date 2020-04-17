using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Templates.Enums;
using Telemedic.Controller;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailedWaitingPatient : ContentPage
	{
        private int AppointmentID = 0, ID = 0;

        public DetailedWaitingPatient()
        {
            InitializeComponent();
        }
        //_PatientPic

        public DetailedWaitingPatient(int ID, int AppointmentID, String PatientName, String BookingTime, String Description, ImageSource ImageAddress, Status CurrentStatus, String PhoneNo)
        {
            InitializeComponent();
            _PatientPic.Source = ImageAddress;
            _PatientName.Text = PatientName;
            _BookedTime.Text = BookingTime;
            _PatientPhoneNo.Text = PhoneNo;

            if (CurrentStatus != Status.Waiting)
            {
                _AcceptDeclineStack.IsVisible = false;
            }
            _Description.Text = Description;

            this.AppointmentID = AppointmentID;
            this.ID = ID;
        }

        public DetailedWaitingPatient(int ID, String PatientName, String BookingTime, String Description, ImageSource ImageAddress)
        {
            InitializeComponent();
            _PatientPic.Source = ImageAddress;
            _PatientName.Text = PatientName;
            _BookedTime.Text = BookingTime;
            _AcceptDeclineStack.IsVisible = false;

            _Description.Text = Description;
        }


        private async void _Decline_Clicked(object sender, EventArgs e)
        {
            await HandleAppointment(-1);
        }

        private async void _Accept_Clicked(object sender, EventArgs e)
        {
            await HandleAppointment(1);
        }

        private async Task HandleAppointment(int Status)
        {
            int Result = await AppointmentController.AppointmentStatus(Status, AppointmentID, ID);

            switch (Result)
            {
                case -1:
                    ///appointment was declined
                    await Utilities.CreateAlertDialog("Alert", "Appointment Declined.", "Ok", delegate
                    {
                        Navigation.PopAsync();
                    });

                    break;
                case -2:
                    ///appointment was declined
                    await Utilities.CreateAlertDialog("Alert", "Appointment Acceptance Failed.", "Ok", delegate
                    {
                        Navigation.PopAsync();
                    });
                    break;
                case 0:

                    ///if 0 is returned, it means booking was accepted but default message wasnt sent.
                    ///so try to resend the message
                    ///if that fails set the appointment status back to 0

                    bool InitMessage = await ChatController.SendChat(ID, "{Appointment Accepted, Default Message}");
                    if (InitMessage)
                    {
                        await Utilities.CreateAlertDialog("Alert", "Appointment Accepted.", "Ok", delegate
                        {
                            Navigation.PopAsync();
                        });
                    }
                    else
                    {
                        //roll back changes if message fails to send
                        await AppointmentController.AppointmentStatus(0, AppointmentID, ID);
                        await Utilities.CreateAlertDialog("Alert", "Failed to notify Client.", "Ok", delegate
                        {
                            Navigation.PopAsync();
                        });
                    }

                    break;
                case 1:
                    await Utilities.CreateAlertDialog("Alert", "Appointment Accepted.", "Ok", delegate
                    {
                        Navigation.PopAsync();
                    });
                    break;
            }
        }
    }
}