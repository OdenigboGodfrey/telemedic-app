using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedic.Controller;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Telemedic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReviewPage : ContentPage
	{
        int ID;
        public ReviewPage()
        {
            InitializeComponent();
        }

        public ReviewPage(int ID)
        {
            InitializeComponent();
            this.ID = ID;
        }

        public ReviewPage(int ID, String Message, String Title, double Stars)
        {
            InitializeComponent();

            Utilities.CreateStars(_FavouriteStarStack, Stars, typeof(ReviewPage));
            //for (int i = 0; i < Math.Floor(Stars);i++)
            //{
            //    _FavouriteStarStack.Children.Add(
            //        new Image
            //        {
            //            Source = Utilities.Source("ic_star_yellow.png", typeof(ReviewPage)),
            //            WidthRequest = 30,
            //            HeightRequest = 30
            //        });
            //}

            //if (Math.Floor(Stars) != Math.Ceiling(Stars))
            //{
            //    _FavouriteStarStack.Children.Add(
            //        new Image
            //        {
            //            Source = Utilities.Source("ic_half_star_yellow.png", typeof(ReviewPage)),
            //            WidthRequest = 30,
            //            HeightRequest = 30
            //        });
            //}
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Dictionary<String, String> Data = await MedicalPractitionerController.GetReview(ID);

            _Title.Text = Data["Title"];
            _ReviewMessage.Text = Data["Review"];
            Utilities.CreateStars(_FavouriteStarStack, Convert.ToInt32(Data["Stars"]), typeof(ReviewPage));
        }
    }
}