using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Telemedic.Model;
using Xamarin.Forms;


namespace Telemedic.Controller
{
    static class SignUpController
    {
        public static async Task<bool> CheckUniqueFields(FormUrlEncodedContent Data,String FieldName)
        {
            String ResponseJson = await SignUpModel.CheckUniqueFields(Data,FieldName);
            
            var DecodedJson = JObject.Parse(ResponseJson);

            return Convert.ToBoolean(DecodedJson["status"]);
            
        }

        public static async Task<object[]> SignUp(FormUrlEncodedContent Data)
        {
            String ResponseJson = await SignUpModel.SignUp(Data);
            var DecodedJson = JObject.Parse(ResponseJson);

            return new object[] { DecodedJson["status"], DecodedJson["message"] };
        }

        public static void CallingCode(ContentPage CallingPage, Picker PhonePicker, int SelectedIndex = 0)
        {
            ///Json object to save the decoded json
            JObject PhoneData = new JObject();
            
            ///access the PCL assets to get the JSON File for calling codes
            var assembly = CallingPage.GetType().Assembly;
            Stream stream = assembly.GetManifestResourceStream(Utilities.ResourceBaseAddress + "JSON.phone_to_countries.json");

            using (var reader = new StreamReader(stream))
            {
                var JSON = reader.ReadToEnd();
                PhoneData = JObject.Parse(JSON);
            }

            var SortedList = new List<String>();

            foreach (var CallingCode in PhoneData)
            {
                if (CallingCode.Value.ToString() != String.Empty) SortedList.Add(CallingCode.Key + " " + CallingCode.Value); //PhonePicker.Items.Add(CallingCode.Key + " "+ CallingCode.Value);
            }

            SortedList.Sort();

            foreach (var Child in SortedList)
            {
                PhonePicker.Items.Add(Child);
            }
            PhonePicker.SelectedIndex = SelectedIndex;
        }

        public static String GetCountryFromCallingCode(ContentPage CallingPage, String CallingCode)
        {
            JObject Data = new JObject();
            JObject CountryData = new JObject();

            String CountryShortCode = "NG", CountryName = "Nigeria";

            var assembly = CallingPage.GetType().Assembly;
            Stream CallingCodeStream = assembly.GetManifestResourceStream(Utilities.ResourceBaseAddress + "JSON.phone_to_countries.json");
            Stream CountriesStream = assembly.GetManifestResourceStream(Utilities.ResourceBaseAddress + "JSON.country_names.json");

            using (var reader = new StreamReader(CallingCodeStream))
            {
                var JSON = reader.ReadToEnd();
                Data = JObject.Parse(JSON);
            }

            foreach (var Child in Data)
            {
                if (Child.Value.Equals(CallingCode)) CountryShortCode = Child.Key; break;
            }

            using (var reader = new StreamReader(CountriesStream))
            {
                var JSON = reader.ReadToEnd();
                ///override previous data
                Data = JObject.Parse(JSON);
            }

            ///search through the countries for the country shortcode
            foreach (var Child in Data)
            {
                if (Child.Value.Equals(CountryShortCode)) CountryName = Child.Key; break;
            }

            return CountryName;
        }
    }
}
