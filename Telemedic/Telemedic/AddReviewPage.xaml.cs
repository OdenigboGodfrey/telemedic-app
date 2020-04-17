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
	public partial class AddReviewPage : ContentPage
	{
        private int StarCounter = 0, MedicID = 0;

		public AddReviewPage ()
		{
			InitializeComponent ();
            _OneStar.Source = Utilities.Source("ic_empty_star_yellow.png", typeof(AddReviewPage));
            _TwoStar.Source = Utilities.Source("ic_empty_star_yellow.png", typeof(AddReviewPage));
            _ThreeStar.Source = Utilities.Source("ic_empty_star_yellow.png", typeof(AddReviewPage));
            _FourStar.Source = Utilities.Source("ic_empty_star_yellow.png", typeof(AddReviewPage));
            _FiveStar.Source = Utilities.Source("ic_empty_star_yellow.png", typeof(AddReviewPage));
        }

        public AddReviewPage(int ID)
        {
            InitializeComponent();

            MedicID = ID;

            _OneStar.Source = Utilities.Source("ic_empty_star_yellow.png", typeof(AddReviewPage));
            _TwoStar.Source = Utilities.Source("ic_empty_star_yellow.png", typeof(AddReviewPage));
            _ThreeStar.Source = Utilities.Source("ic_empty_star_yellow.png", typeof(AddReviewPage));
            _FourStar.Source = Utilities.Source("ic_empty_star_yellow.png", typeof(AddReviewPage));
            _FiveStar.Source = Utilities.Source("ic_empty_star_yellow.png", typeof(AddReviewPage));
        }

        private void OneStar_Tapped(object sender, EventArgs e)
        {
            StarCounter = 1;
            SetFavouriteStars(StarCounter);
        }

        private void TwoStar_Tapped(object sender, EventArgs e)
        {
            StarCounter = 2;
            SetFavouriteStars(StarCounter);
        }

        private void ThreeStar_Tapped(object sender, EventArgs e)
        {
            StarCounter = 3;
            SetFavouriteStars(StarCounter);
        }

        private void FourStar_Tapped(object sender, EventArgs e)
        {
            StarCounter = 4;
            SetFavouriteStars(StarCounter);
        }

        private void FiveStar_Tapped(object sender, EventArgs e)
        {
            StarCounter = 5;
            SetFavouriteStars(StarCounter);
        }

        private void SetFavouriteStars(int Count)
        {
            for (int i = 0; i < _FavouriteStarStack.Children.Count;i++)
            {
                if (i < Count)
                {
                    ((Image)_FavouriteStarStack.Children[i]).Source = Utilities.Source("ic_star_yellow.png", typeof(AddReviewPage));
                }
                else
                {
                    ((Image)_FavouriteStarStack.Children[i]).Source = Utilities.Source("ic_empty_star_yellow.png", typeof(AddReviewPage));
                }

            }
        }

        private async void _SubmitReview_Clicked(object sender, EventArgs e)
        {
            bool Result = await MedicalPractitionerController.PostReview(_ReviewMessage.Text, StarCounter, MedicID, _Title.Text);
            if (Result)
            {
                await Utilities.CreateAlertDialog("Alert", "Rating submitted.", "Ok", delegate
                {
                    Navigation.PopAsync();
                });
                
            }
            else
            {
                await DisplayAlert("Alert", "Rating failed to submit.", "Ok");
            }
        }
    }
}