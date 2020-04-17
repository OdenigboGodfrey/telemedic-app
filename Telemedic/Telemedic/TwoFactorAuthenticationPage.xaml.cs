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
	public partial class TwoFactorAuthenticationPage : ContentPage
	{
		public TwoFactorAuthenticationPage ()
		{
			InitializeComponent ();
		}

        public TwoFactorAuthenticationPage(int ID)
        {
            InitializeComponent();
        }
    }
}