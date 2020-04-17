using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Telemedic.Model;
using Newtonsoft.Json.Linq;

namespace Telemedic.Controller
{
    class MedicalPractitionerController
    {

        /// <summary>
        /// GetProfileDetails gets the users [account number,hourly charge, bank name, account name, working hours]
        /// </summary>
        /// <returns>
        /// Dictionary<String,String> holding the data with keys [AccountNumber, Charge, BankName, AccountName, WorkingHours]
        /// </returns>
        public static async Task<Dictionary<String,String>> GetProfileDetails()
        {
            Dictionary<String, String> ResultData = new Dictionary<String, String>();

            try
            {
                String ResultJson = await GeneralModel.GetAsync("user/profile/medic/" + Utilities.MedicID);
                
                var DecodedJson = JObject.Parse(ResultJson);
                if ((bool)DecodedJson["status"])
                {
                    var info = DecodedJson["info"];

                    ResultData.Add("AccountNumber", info["account_number"].ToString());
                    ResultData.Add("Charge", info["hourly_charge"].ToString());
                    ResultData.Add("BankName", info["bank_name"].ToString());
                    ResultData.Add("AccountName", info["account_name"].ToString());
                    ResultData.Add("WorkingHours", info["working_hours"].ToString());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Unknown Medic ID", "Ok");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            }
            return ResultData;
        }

        /// <summary>
        /// PostProfileDetails posts the user details
        /// </summary>
        /// <returns>
        /// String message showing status of the post
        /// </returns>
        public static async Task<bool> PostProfileDetails(String AccountNumber,String Charge,String BankName,String AccountName,String WorkingHours)
        {
            Dictionary<int, String[]> Data = new Dictionary<int, string[]>
            {
                { 0 , new String[]{ "account_number", AccountNumber.ToString() } },
                { 1 , new String[]{ "hourly_charge", Charge.ToString() } },
                { 2 , new String[]{ "bank_name", BankName.ToString() } },
                { 3 , new String[]{ "account_name", AccountName.ToString() } },
                { 4 , new String[]{ "working_hours", WorkingHours.ToString() } },
            };

            String ResultJson = await GeneralModel.PostAsync(Utilities.PostDataEncoder(Data), "user/profile/medic/" + Utilities.MedicID);
            var DecodedJson = JObject.Parse(ResultJson);
            if ((bool)DecodedJson["status"]) return true;
            return false;
        }

        /// <summary>
        /// GetBioDetails gets the medics bio story
        /// </summary>
        /// <returns>
        /// a string of the bio data
        /// </returns>
        public static async Task<String> GetBioDetails()
        {
            String ResultJson = await GeneralModel.GetAsync("user/profile/bio/" + Utilities.MedicID);
            var DecodedJson = JObject.Parse(ResultJson);

            return (DecodedJson["bio"].ToString() == String.Empty) ? "" : DecodedJson["bio"].ToString();
        }

        public static async Task<String> GetBioDetails(int MedicID)
        {
            String ResultJson = await GeneralModel.GetAsync("user/profile/bio/" + MedicID);
            var DecodedJson = JObject.Parse(ResultJson);

            return (DecodedJson["bio"].ToString() == String.Empty) ? "" : DecodedJson["bio"].ToString();
        }

        public static async Task<bool> PostBioDetails(String BioDetail)
        {
            Dictionary<int, String[]> Data = new Dictionary<int, string[]>
            {
                { 0 , new String[]{ "bio", BioDetail } },
            };

            try
            {
                String ResultJson = await GeneralModel.PostAsync(Utilities.PostDataEncoder(Data), "user/profile/bio/" + Utilities.MedicID);
                var DecodedJson = JObject.Parse(ResultJson);
                return (bool)DecodedJson["status"];
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Alert","An Error occurred while fecting your Bio Data","Ok");
                return false;
            }
        }

        public static async Task<bool> PostReview(String Review, int Stars, int MedicID,String Title)
        {
            //user/rate?medic_id=&user_id=
            Dictionary<int, String[]> Data = new Dictionary<int, string[]>
            {
                { 0 , new String[]{ "user_id", Utilities.ID.ToString() } },
                { 1 , new String[]{ "medic_id", MedicID.ToString() } },
                { 2 , new String[]{ "review", "{Title [length=" + Title.Length + "]:" + Title + "}{Content [length=" + Review.Length +"]:"+ Review +"}" } },
                { 3 , new String[]{ "rate", Stars.ToString() } },
            };

            try
            {
                String ResultJson = await GeneralModel.PostAsync(Utilities.PostDataEncoder(Data), "user/rate");
                var DecodedJson = JObject.Parse(ResultJson);

                return (bool)DecodedJson["status"];
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                return false;
            }

        }

        public static async Task<Dictionary<String,String>> GetReview(int UserID)
        {
            Dictionary<String, String> ReturnedData = new Dictionary<String, String>();

            try
            {
                String ResultJson = await GeneralModel.GetAsync("user/rate?user_id=" + UserID + "&medic_id="+Utilities.MedicID);
                var DecodedJson = JObject.Parse(ResultJson);

                if ((bool)DecodedJson["status"])
                {
                    String StoredContent = DecodedJson["info"]["review"].ToString();

                    String Title = StoredContent.Substring(StoredContent.IndexOf('{') + 1, ((StoredContent.IndexOf('}') - 1)));

                    int TitleLength = Convert.ToInt32(Title.Substring(Title.IndexOf('[') + 1, ((Title.IndexOf(']')) - (Title.IndexOf('[') + 1))).Replace("length=", ""));

                    Title = StoredContent.Substring((StoredContent.IndexOf(':') + 1), TitleLength);

                    ///strip out title  
                    StoredContent = StoredContent.Replace("{Title [length=" + TitleLength + "]:" + Title + "}", "");

                    String Content = StoredContent.Substring(StoredContent.IndexOf('{') + 1, ((StoredContent.IndexOf('}') - 1)));

                    int ContentLength = Convert.ToInt32(Content.Substring(Content.IndexOf('[') + 1, ((Content.IndexOf(']')) - (Content.IndexOf('[') + 1))).Replace("length=", ""));

                    Content = StoredContent.Substring((StoredContent.IndexOf(':') + 1), ContentLength);

                    ReturnedData.Add("Title", Title);
                    ReturnedData.Add("Review", Content);
                    ReturnedData.Add("Stars", DecodedJson["info"]["star"].ToString());
                }
                
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            }

            return ReturnedData;
        }
    }
}
