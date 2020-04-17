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
	public partial class WithdrawalPage : ContentPage
	{
		public WithdrawalPage ()
		{
			InitializeComponent ();
		}

        private void _Submit_Clicked(object sender, EventArgs e)
        {

        }

        private void _Amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_Amount.Text.Length > 0 && !(_Submit.BackgroundColor == (Color)App.Current.Resources["_MedAppLightBlue"]))
            {
                _Submit.BackgroundColor = (Color)App.Current.Resources["_MedAppLightBlue"];
            }
            else if(_Amount.Text.Length == 0)
            {
                _Submit.BackgroundColor = (Color)App.Current.Resources["_MedAppAsh"];
            }
        }
    }
}