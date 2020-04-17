using Plugin.Toasts;
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
	public partial class SecondaryMainPage : ContentPage
	{
        public static ImageSource Blur;
        public SecondaryMainPage ()
		{
            InitializeComponent();

            /** set image sources for UI Image controls **/
            Blur = Utilities.Source("Blur.jpg", typeof(MainPage));

            _Overlap_1.Source = Blur;
            _MedLogo.Source = Utilities.Source("Med_Logo.png", typeof(MainPage));
            
            /** hide the toolbar for this page as it is the mainpage **/
            NavigationPage.SetHasNavigationBar(this, false);
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

        private void _logIn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private void _signUp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUp(), false);
        }
    }
}