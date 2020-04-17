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
	public partial class OTPPage : ContentPage
	{
		
        private GeneralDetails GD;
        private int ResendOTPCounter = 1;

        public OTPPage()
        {
            InitializeComponent();
        }

        public OTPPage(String OTPType)
        {
            InitializeComponent();
        }


        public OTPPage(GeneralDetails GD)
        {
            InitializeComponent();
            this.GD = GD;
            _VerificationEntry1.Focus();
            ResendOTPCountDown(60 * ResendOTPCounter);
        }

        public OTPPage(OTPData OTPData)
        {
            InitializeComponent();
            _VerificationEntry1.Text = OTPData.OTPCode1;
            _VerificationEntry2.Text = OTPData.OTPCode2;
            _VerificationEntry3.Text = OTPData.OTPCode3;
            _VerificationEntry4.Text = OTPData.OTPCode4;
            _ResendOTP.IsEnabled = true;
        }

        private void _ResendOTP_Clicked(object sender, EventArgs e)
        {
            //Navigation.pus
        }

        private void _VerifyOTP_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_VerificationEntry1.Text))
            {

            }
        }

        private void _VerificationEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            int verification_counter = 0;
            /**
             * Loop throught the children of the grid elements and make sure all elements have been filled
             * **/
            foreach (Element element in _VerificationEntryGrid.Children)
            {
                if (!String.IsNullOrEmpty(((Entry)element).Text))
                {
                    verification_counter += 1;

                }
            }

            /**
             * handle auto focusing on the next entry
             * **/
            for (int i = 0; i < _VerificationEntryGrid.Children.Count; i++)
            {
                if (i == (_VerificationEntryGrid.Children.Count - 1))
                {
                    _VerificationEntryGrid.Children[i].Focus();
                    break;
                }

                if (String.IsNullOrEmpty(((Entry)_VerificationEntryGrid.Children[i]).Text))
                {
                    _VerificationEntryGrid.Children[i].Focus();
                    break;
                }
            }



            if (verification_counter == _VerificationEntryGrid.Children.Count)
            {
                Navigation.PushAsync(new OTPVerificationPage(new OTPData(_VerificationEntry1.Text, _VerificationEntry2.Text, _VerificationEntry3.Text, _VerificationEntry4.Text, GD)));
                Navigation.RemovePage(this);
            }
        }
        /**
         * creates method which counts down then enables the resend otp button
         * **/
        private void ResendOTPCountDown(int counter)
        {
            /** init testing phase**/
            //int counter = 60;
            Device.StartTimer(TimeSpan.FromSeconds(1), delegate
            {
                if (counter >= 0)
                {
                    _ResendOTPCounter.Text = "Resend OTP in " + Utilities.TimeInMinutesOrHours(counter, false);
                    counter--;
                    return true;
                }
                else
                {
                    counter = 60;
                    _ResendOTPCounter.Text = String.Empty;
                    _ResendOTP.IsEnabled = true;
                    return false;
                }
            });

        }
    }
}