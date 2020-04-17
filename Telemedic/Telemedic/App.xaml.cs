using System;
using System.IO;
using System.Threading.Tasks;
using Telemedic.Model.Local;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Telemedic
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            NavigationPage NavPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = (Color)App.Current.Resources["_MedAppLightBlue"],
                BarTextColor = Color.White
            };

            MainPage = NavPage;
            MainPage.BackgroundColor = (Color)App.Current.Resources["_MedAppChatBackground"];
            

            //MainPage.ChildAdded += delegate
            //{
            //    if (NavPage.Navigation.NavigationStack[NavPage.Navigation.NavigationStack.Count - 1].ToString().Equals("Telemedic.SettingsPage") )
            //    {
            //        App.Current.MainPage.DisplayAlert("Alert","Settings","OK");
            //        NavPage.BarBackgroundColor = Color.Transparent;
            //        NavPage.Navigation.NavigationStack[NavPage.Navigation.NavigationStack.Count - 1].BackgroundImage = "Blur.jpg";
            //        MainPage = NavPage;
            //    }
            //};


        }

        protected async override void OnStart()
        {
            // Handle when your app starts
            await LocalDB.CreateDB();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            
        }
    }

    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }

    public interface IPermission
    {
        void GetPermissions();

        bool CheckPermission(String Permission);
    }

}
