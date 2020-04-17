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
	public partial class PharmacyProfile : ContentPage
	{
		public PharmacyProfile ()
		{
			InitializeComponent ();
		}

        public PharmacyProfile(int PharmacyID)
        {
            InitializeComponent();
            dp_pharm.Source = Utilities.Source("icon_pharmacy.jpg",typeof(PharmacyProfile));
        }
    }
}