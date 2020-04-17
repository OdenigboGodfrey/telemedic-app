using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telemedic.Model;

namespace Telemedic.Controller
{
    static class SettingsController
    {
        public static async Task<Dictionary<String,String>> GetSettings()
        {
            Dictionary<String, String> MyData = new Dictionary<string, string>();

            String ResultJson = await GeneralModel.GetAsync("user/profile/settings/" + Utilities.ID);

            var DecodedJson = JObject.Parse(ResultJson);

            MyData.Add("name", DecodedJson["info"]["name"].ToString());
            MyData.Add("email", DecodedJson["info"]["email"].ToString());
            MyData.Add("phone", DecodedJson["info"]["phone"].ToString());
            MyData.Add("dob", DecodedJson["info"]["date_of_birth"].ToString());
            MyData.Add("gender", DecodedJson["info"]["gender"].ToString());
            MyData.Add("BloodGroup", DecodedJson["info"]["blood_group"].ToString());
            MyData.Add("address", DecodedJson["info"]["address"].ToString());

            return MyData;
        }

        public static async Task<Dictionary<String, String>> GetSettings(int ID)
        {
            Dictionary<String, String> MyData = new Dictionary<string, string>();

            String ResultJson = await GeneralModel.GetAsync("user/profile/settings/" + ID);

            var DecodedJson = JObject.Parse(ResultJson);

            MyData.Add("name", DecodedJson["info"]["name"].ToString());
            MyData.Add("email", DecodedJson["info"]["email"].ToString());
            MyData.Add("phone", DecodedJson["info"]["phone"].ToString());
            MyData.Add("dob", DecodedJson["info"]["date_of_birth"].ToString());
            MyData.Add("gender", DecodedJson["info"]["gender"].ToString());
            MyData.Add("BloodGroup", DecodedJson["info"]["blood_group"].ToString());
            MyData.Add("address", DecodedJson["info"]["address"].ToString());
            MyData.Add("IsMedic", DecodedJson["info"]["is_medic"].ToString());

            if ((bool)DecodedJson["info"]["is_medic"])
            {
                MyData.Add("MedicCategory", DecodedJson["info"]["category"].ToString());
                MyData.Add("MedicId", DecodedJson["info"]["medic_id"].ToString());
            }

            return MyData;
        }

        public static async Task<bool> PostSettings(bool ChangePassword, String Phone = "", String OldPassword = "", String NewPassword = "")
        {
            Dictionary<int, String[]> Data = new Dictionary<int, string[]>
            {
                { 0 , new String[]{ "user_id", Utilities.ID.ToString() } },
            };

            if (!ChangePassword)
            {
                Data.Add(1, new String[] { "phone", Phone });
            }
            else
            {
                Data.Add(1, new String[] { "change_password", "1" });
                Data.Add(2, new String[] { "old_password", OldPassword });
                Data.Add(3, new String[] { "new_password", NewPassword });
            }

            try
            {
                String ResultJson = await GeneralModel.PostAsync(Utilities.PostDataEncoder(Data), "user/profile/settings/" + Utilities.ID);

                var DecodedJson = JObject.Parse(ResultJson);

                return (bool)DecodedJson["status"];
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                return false;
            }
            
        }
    }
}
