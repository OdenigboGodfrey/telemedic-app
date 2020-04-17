using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Telemedic.Controller
{
    static class MedicalCategoriesController
    {
        private static Dictionary<int, String> MedicalCategories = new Dictionary<int, string>();
        /**
         * summary this method returns the medical categories as a Dictionary<int,string>
         * returns a Dictionary<int,string>
         * **/
        public static async Task<Dictionary<int, string>> GetMedicalCategoriesAsync()
        {
            if (MedicalCategories.Count == 0)
            {
                //dictionary is empty, populate
                try
                {
                    var ResponseJson = await Model.MedicalCategoriesModel.MedicalCategories();
                    var DecodedJson = JObject.Parse(ResponseJson);

                    foreach (var cat in DecodedJson["categories"])
                    {
                        MedicalCategories.Add(Convert.ToInt32(cat["id"]), cat["title"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "ok");
                }
            }

            return MedicalCategories;
        }

        /**
         * summary this method loads the medical categories into a picker
         * **/
        public static async void LoadMedicalCategoriesToList(Picker MedicalCatPicker)
        {
            Dictionary<int, String> MedCats = await GetMedicalCategoriesAsync();
            foreach (int i in MedCats.Keys)
            {
                MedicalCatPicker.Items.Add(MedCats[i]);
            }
            MedicalCatPicker.SelectedIndex = 0;
        }
        /**
         * returns the ID of the medical category passed
         * param name="MedicalCategoryTitle" the selected medical category
         * **/
        public static async Task<int> GetMedicalCategoryId(object MedicalCategoryTitle)
        {
            Dictionary<int, String> MedCats = await GetMedicalCategoriesAsync();
            if (MedCats.ContainsValue(MedicalCategoryTitle.ToString()))
            {
                int MedCatKey = -1;

                foreach (int key in MedCats.Keys)
                {
                    if (MedCats[key].Equals(MedicalCategoryTitle))
                    {
                        MedCatKey = key;
                    }
                }
                 
                return MedCatKey;
            }
            else
            {
                return -1;
            }
        }
    }
}
