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
	public partial class MedicalRequestConfirmationDialog : ContentPage
	{
		public MedicalRequestConfirmationDialog ()
		{
			InitializeComponent ();
		}

        public MedicalRequestConfirmationDialog(int ID)
        {
            InitializeComponent();
        }

        private void _Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void _OK_Clicked(object sender, EventArgs e)
        {
            //handle ok

            Navigation.PopAsync();
        }
    }
}