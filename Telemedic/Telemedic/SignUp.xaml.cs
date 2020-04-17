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
	public partial class SignUp : ContentPage
	{
		public SignUp ()
		{
			InitializeComponent ();
            _Overlap_1.Source = MainPage.Blur;
            _MedGang.Source = Utilities.Source("Med_Gang.png", typeof(SignUp));
            SizeChanged += delegate
            {
                Utilities.DoResize(_ParentGrid);
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Utilities.DoResize(_ParentGrid);

        }
        private void _AsUser_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GeneralForm("user"));
        }

        private void _AsHealthProfessional_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GeneralForm("HP"));
        }
    }
}