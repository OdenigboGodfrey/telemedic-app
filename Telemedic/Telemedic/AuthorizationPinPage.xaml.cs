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
	public partial class AuthorizationPinPage : ContentPage
	{
        bool isMedic = false;
		public AuthorizationPinPage ()
		{
			InitializeComponent ();
		}

        public AuthorizationPinPage(int ID,bool isMedic)
        {
            InitializeComponent();
            this.isMedic = isMedic;

            if (isMedic)
            {
                Utilities.AuthControlHider(_ParentGrid, "For-User");
            }
            else
            {
                Utilities.AuthControlHider(_ParentGrid, "For-Doctor");
            }
        }

        private void _UserSubmit_Clicked(object sender, EventArgs e)
        {

        }

        private void _DoctorSubmit_Clicked(object sender, EventArgs e)
        {

        }
    }
}