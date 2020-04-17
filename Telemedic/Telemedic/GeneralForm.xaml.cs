using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Controller;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GeneralForm : ContentPage
	{
        String UserType, ShortCountryCode = "";
        private int PreviousSelectedIndex = 0;
        public GeneralForm ()
		{
			InitializeComponent ();
		}

        public GeneralForm(String UserType)
        {
            InitializeComponent();
            this.UserType = UserType;
            //_CountryCode.Items.Add("+234");
            //_CountryCode.Items.Add("+235");
            //_CountryCode.Items.Add("+236");
            //_CountryCode.Items.Add("+237");
            SignUpController.CallingCode(this, _CountryCode);
            //_CountryCode.SelectedIndex = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void _Submit_Clicked(object sender, EventArgs e)
        {
            try
            {
                String PhoneNo = _CountryCode.SelectedItem + _PhoneNo.Text;

                if (!String.IsNullOrEmpty(PhoneNo) && !String.IsNullOrEmpty(_ConfirmPassword.Text))
                {
                    String Email = (String.IsNullOrEmpty(_Email.Text)) ? "n/a" : _Email.Text;
                    //inform user if email/phone no is used
                    bool IsEmailAvail = false;

                    if (Email != "n/a")
                    {
                        IsEmailAvail = await SignUpController.CheckUniqueFields(Utilities.PostDataEncoder(new Dictionary<int, string[]>
                        {
                            { 0 , new String[]{ "email", _Email.Text } }
                        }), "e-mail");

                    }

                    bool IsPhoneAvail = await SignUpController.CheckUniqueFields(Utilities.PostDataEncoder(new Dictionary<int, string[]>
                    {
                        { 0 , new String[]{ "phone", PhoneNo } }
                    }), "phone-no");


                    if (IsEmailAvail)
                    {
                        if (IsPhoneAvail)
                        {
                            //confirm password
                            if (_ConfirmPassword.Text.Equals(_Password.Text))
                            {
                                await Navigation.PushAsync(new OTPPage(new GeneralDetails(Email, PhoneNo, _ConfirmPassword.Text, UserType, ShortCountryCode)));
                            }
                            else
                            {
                                await DisplayAlert("Alert", "Passwords don't match", "Okay");
                            }
                        }
                        else
                        {
                            await DisplayAlert("Alert", "Phone number is already registered ", "Okay");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Email address is already registered ", "Okay");
                    }

                }
                else
                {
                    //force validation
                    if (String.IsNullOrEmpty(_PhoneNo.Text))
                    {
                        _PhoneNo.PlaceholderColor = Color.Red;
                    }
                    else
                    {
                        _PhoneNo.PlaceholderColor = Color.Black;
                    }

                    if (String.IsNullOrEmpty(_Password.Text))
                    {
                        _Password.PlaceholderColor = Color.Red;
                    }
                    else
                    {
                        _Password.PlaceholderColor = Color.Black;
                    }

                    if (String.IsNullOrEmpty(_ConfirmPassword.Text))
                    {
                        _ConfirmPassword.PlaceholderColor = Color.Red;
                    }
                    else
                    {
                        _ConfirmPassword.PlaceholderColor = Color.Black;
                    }

                    await DisplayAlert("Alert", "Pleae fill the RED Fields", "Okay");
                }
            }
            catch (System.Net.WebException WebEx)
            {
                await DisplayAlert("Alert", Utilities.NoInternet, "Okay");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "Okay");
            }

        }

        private void _Next_Clicked(object sender, EventArgs e)
        {
        }

        private Dictionary<int, String[]> CheckUniqueFields(String FieldName)
        {

            Dictionary<int, String[]> Data = new Dictionary<int, string[]>
            {
                { 0 , new String[]{ (FieldName.Equals("phone")) ? "phone" : "email", (FieldName.Equals("phone")) ? _CountryCode.SelectedItem + _PhoneNo.Text : _Email.Text } }
            };

            return Data;
        }

        private void _CountryCode_Focused(object sender, FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                ///PreviousSelectedIndex
                _CountryCode.Items.Clear();
                SignUpController.CallingCode(this, _CountryCode, PreviousSelectedIndex);
            }
            else
            {
                PreviousSelectedIndex = _CountryCode.SelectedIndex;
                String SelectedItem = _CountryCode.SelectedItem.ToString();
                //DisplayAlert("Alert", "Alert", "Ok");

                _CountryCode.Items.Clear();

                _CountryCode.Items.Add(SelectedItem.Split(' ')[1]);
                _CountryCode.SelectedIndex = 0;

                ShortCountryCode = SelectedItem.Split(' ')[0];
            }


        }

        
    }
}