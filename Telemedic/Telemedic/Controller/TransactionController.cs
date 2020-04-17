using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

using Telemedic.Templates.Enums;
using Telemedic.Model;
using Telemedic.Templates;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace Telemedic.Controller
{
    static class TransactionController
    {
        /**
         * sumarry controller function for funding account 
         * param name="Data" the data to POST 
         * returns Task<String> of the json message
         * **/
        public static async Task<String> FundAccount(FormUrlEncodedContent Data)
        {
            return await TransactionModel.FundAccount(Data);
        }

        /**
         * sumarry Loads the transaction history to a StackLayout
         * param name="Stack" the stacklayout where the transaction history should be loaded
         * **/
        public static async Task LoadTransactionHistoryToStack(StackLayout Stack,Label Balance)
        {
            Stack.Children.Clear();

            Dictionary<int, String[]> Data = new Dictionary<int, string[]>
            {
                { 0 , new String[]{ "user_id", Utilities.ID.ToString() } },
            };

            
            //try
            //{
                String ResponseJson = await TransactionModel.TransactionHistory(Utilities.PostDataEncoder(Data));

                var DecodedJson = JObject.Parse(ResponseJson);
                Balance.Text = Utilities.Currency + " " + Convert.ToDouble(DecodedJson["transactions"]["balance"]);
                Utilities.Balance = Convert.ToDouble(DecodedJson["transactions"]["balance"]);

                await Utilities.RunTask(delegate 
                {
                    foreach (var Transaction in DecodedJson["transactions"]["history"])
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            try
                            {
                                Stack.Children.Add(WalletTemplate.TransactionTemplate01(
                                    Transaction["amount"].ToString(), ((Transaction["action"].ToString() == "Deposit") ? TransactionType.Credit : TransactionType.Debit), "Date " + DateTime.Parse(Transaction["time"].ToString()).ToShortDateString() + "\n" + Transaction["reason"].ToString())
                                );
                            }
                            catch (Exception exx)
                            {
                                App.Current.MainPage.DisplayAlert("Alert", exx.Message, "Ok");
                            }
                            
                        });
                    }
                });
            //}
            //catch (Exception ex)
            //{
//                await App.Current.MainPage.DisplayAlert("Alert",ex.StackTrace,"Ok");
  //          }
            
            
        }


        public static async Task Withdraw(int Amount)
        {
            
            //user_id & amount

            Dictionary<int, String[]> Data = new Dictionary<int, string[]>
            {
                { 0 , new String[]{ "user_id", Utilities.ID.ToString() } },
                { 1 , new String[]{ "amount", Amount.ToString() } },
            };

            try
            {
                String ResponseJson = await GeneralModel.PostAsync(Utilities.PostDataEncoder(Data), "user/withdraw");
                await App.Current.MainPage.DisplayAlert("Alert", ResponseJson, "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            }
            
            
            //var DecodedJson = JObject.Parse(ResponseJson);

        }
        
    }
}
