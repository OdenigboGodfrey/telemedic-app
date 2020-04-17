using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Telemedic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OTPVerificationPage : ContentPage
	{
        bool def = true;
        OTPData OTPData;
        public OTPVerificationPage()
        {
            InitializeComponent();
        }

        public OTPVerificationPage(OTPData otpdata)
        {
            InitializeComponent();
            this.OTPData = otpdata;
            FakeAnimate();
        }

        private void FakeAnimate()
        {

            int counter = 3;
            int pointer_counter = 0;
            Device.StartTimer(TimeSpan.FromSeconds(1), delegate
            {
                if (counter >= 0)
                {
                    counter--;
                    if (pointer_counter > 5)
                    {
                        pointer_counter = 0;
                    }
                    _OTPVerificationPageLabel.Text = "Verifying";
                    for (int i = 0; i < pointer_counter; i++)
                    {
                        _OTPVerificationPageLabel.Text += ".";
                    }
                    pointer_counter++;
                    return true;
                }
                else
                {
                    counter = 3;
                    if (def)
                    {
                        /** handle OTP HERE**/
                        if (OTPData.GD.UserType.Equals("user"))
                        {
                            Navigation.PushAsync(new UserPersonalDetailsPage(OTPData.GD));
                        }
                        else if (OTPData.GD.UserType.Equals("HP"))
                        {
                            Navigation.PushAsync(new MedicalPractionerPersonalDetailsPage(OTPData.GD));
                        }
                        //Utilities.ClearNavigationStack();
                        /** end testing phase**/
                    }
                    else
                    {
                        Navigation.PushAsync(new OTPPage(OTPData));
                    }
                    Navigation.RemovePage(this);
                    return false;
                }
            });
        }
    }
}