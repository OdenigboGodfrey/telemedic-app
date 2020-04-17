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
	public partial class UpdateProfile : ContentPage
	{
		public UpdateProfile ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this,true);
		}
	}
}