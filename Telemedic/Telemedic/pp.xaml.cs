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
	public partial class pp : ContentPage
	{
		public pp ()
		{
			InitializeComponent ();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Utilities.SendNotification("Title 1", "Description 1");
        }
    }
}