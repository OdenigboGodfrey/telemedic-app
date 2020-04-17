using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Controller;
using Newtonsoft.Json.Linq;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FundAccountPage : ContentPage
	{
		public FundAccountPage ()
		{
			InitializeComponent ();
		}

        public FundAccountPage(int ID)
        {
            InitializeComponent();
            _Currency.Text = Utilities.Currency;
        }

        private async void _Fund_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(_Amount.Text) || !String.IsNullOrEmpty(_Amount.Text))
            {
                Dictionary<int, String[]> Data = new Dictionary<int, string[]>
                {
                    { 0 , new String[]{ "user_id", Utilities.ID.ToString() } },
                    { 1 , new String[]{ "amount", _Amount.Text } }
                };

                try
                {
                    String ResponseJson = await TransactionController.FundAccount(Utilities.PostDataEncoder(Data));

                    var DecodedJson = JObject.Parse(ResponseJson);

                    if (Convert.ToBoolean(DecodedJson["status"]))
                    {
                        //return to previous page after user closes the alert dialog
                        await Utilities.CreateAlertDialog("Alert", DecodedJson["message"].ToString(), "Ok", delegate
                        {
                            Navigation.PopAsync();
                        });
                    }
                    else
                    {
                        await DisplayAlert("Alert", DecodedJson["message"].ToString(), "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alert", ex.Message, "Ok");
                }
            }
        }
    }
}