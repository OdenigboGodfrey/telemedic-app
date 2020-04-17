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
	public partial class ChangePasswordPage : ContentPage
	{
		public ChangePasswordPage ()
		{
			InitializeComponent ();
            _ActionType.Items.Add("Change Password");
            _ActionType.Items.Add("Edit Basic Details");


            _ActionType.SelectedIndex = 0;
        }

        public ChangePasswordPage(int ID)
        {
            InitializeComponent();
            _ActionType.Items.Add("Change Password");
            _ActionType.Items.Add("Edit Basic Details");
            

            _ActionType.SelectedIndex = 0;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            bool Result = false;
            switch (_ActionType.SelectedIndex)
            {
                case 0:
                    //change password
                    if (_NewPassword.Text == _NewPasswordConfirm.Text)
                    {
                        Result = await SettingsController.PostSettings(true, _OldPassword.Text, _NewPasswordConfirm.Text);
                        if (Result)
                        {
                            await DisplayAlert("Alert", "Password Changed.", "Ok");
                        }
                        else
                        {
                            await DisplayAlert("Alert", "Failed to change Password.", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Passwords don't match.", "Ok");
                    }
                    
                    break;
                case 1:
                    Result = await SettingsController.PostSettings(false, _PhoneNo.Text);
                    if (Result)
                    {
                        await DisplayAlert("Alert", "Basic Details Changed.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Failed to change Basic Details.", "Ok");
                    }
                    break;
            }
        }

        private void _ActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (_ActionType.SelectedIndex)
            {
                case 0:
                    _BasicDetailsPanel.IsVisible = false;
                    _ChangePasswordPanel.IsVisible = true;
                    break;
                case 1:
                    _BasicDetailsPanel.IsVisible = true;
                    _ChangePasswordPanel.IsVisible = false;
                    break;
            }
        }
    }
}