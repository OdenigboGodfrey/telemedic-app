using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Templates;
using Telemedic.Templates.Enums;

namespace Telemedic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LabProfile : ContentPage
	{
		public LabProfile ()
		{
			InitializeComponent ();
            dp_lab.Source = Utilities.Source("icon_lab.jpg", typeof(LabProfile));
        }

        public LabProfile(int LabID)
        {
            InitializeComponent();
            dp_lab.Source = Utilities.Source("icon_lab.jpg", typeof(LabProfile));
        }
    }
}