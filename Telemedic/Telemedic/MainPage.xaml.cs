using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Telemedic
{
    public partial class MainPage : ContentPage
    {
        public static ImageSource Blur;
        public MainPage()
        {
            InitializeComponent();

            /** set image sources for UI Image controls **/
            Blur = Utilities.Source("Blur.jpg", typeof(MainPage));

            _TeleMedicLogo.Source = Utilities.Source("Med_Logo.png", typeof(MainPage));

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
            Device.StartTimer(TimeSpan.FromSeconds(2), delegate
            {
                Navigation.PushAsync(new SecondaryMainPage());
                Navigation.RemovePage(this);
                return true;
            });
        }

        
    }
}
