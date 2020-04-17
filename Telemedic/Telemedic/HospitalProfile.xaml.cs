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
	public partial class HospitalProfile : ContentPage
	{
		public HospitalProfile ()
		{
			InitializeComponent ();
            dp_hos.Source = Utilities.Source("icon_hospital.jpg", typeof(LabProfile));
        }

        public HospitalProfile(int HospitalID)
        {
            InitializeComponent();
            dp_hos.Source = Utilities.Source("icon_hospital.jpg", typeof(LabProfile));
        }
    }
}