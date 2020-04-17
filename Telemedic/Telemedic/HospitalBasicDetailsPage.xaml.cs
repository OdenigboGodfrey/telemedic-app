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
	public partial class HospitalBasicDetailsPage : ContentPage
	{
		public HospitalBasicDetailsPage ()
		{
			InitializeComponent ();
		}

        public HospitalBasicDetailsPage(int ID)
        {
            InitializeComponent();
            _Speciality.Items.Add("Select Speciality");
            _Speciality.SelectedIndex = 0;
        }

        private void _Submit_Clicked(object sender, EventArgs e)
        {

        }
    }
}