using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Controller;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedicalPractionerPersonalDetailsPage : ContentPage

    {
        GeneralDetails GD;
        public MedicalPractionerPersonalDetailsPage()
        {
            InitializeComponent();
            _Gender.Items.Add("Male");
            _Gender.Items.Add("Female");
            _Gender.SelectedIndex = 0;
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public MedicalPractionerPersonalDetailsPage(GeneralDetails GD)
        {
            InitializeComponent();
            this.GD = GD;
            _Gender.Items.Add("Male");
            _Gender.Items.Add("Female");
            _Gender.SelectedIndex = 0;

            _BloodGroup.Items.Add("A-");
            _BloodGroup.Items.Add("A+");
            _BloodGroup.Items.Add("B-");
            _BloodGroup.Items.Add("B+");
            _BloodGroup.Items.Add("O-");
            _BloodGroup.Items.Add("O+");
            _BloodGroup.Items.Add("AB-");
            _BloodGroup.Items.Add("AB+");

            _BloodGroup.SelectedIndex = 0;
            
            _State.Items.Add("Abia");
            _State.Items.Add("Enugu");
            _State.Items.Add("Imo");
            _State.SelectedIndex = 0;

            MedicalCategoriesController.LoadMedicalCategoriesToList(_MedCategory);
            _MedCategory.SelectedIndex = 0;

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void _SignUp_Clicked(object sender, EventArgs e)
        {
            Dictionary<int, String[]> KeyValues = new Dictionary<int, string[]>();
            bool IsCompleted = false;
            try
            {

                int MedicCategoryID = await MedicalCategoriesController.GetMedicalCategoryId(_MedCategory.SelectedItem);

                KeyValues.Add(0, new String[] { "full_name", _FullName.Text });
                KeyValues.Add(1, new String[] { "gender", (_Gender.SelectedItem.Equals("Male")) ? "m" : "f" });
                KeyValues.Add(2, new String[] { "phone", GD.Phone });
                KeyValues.Add(3, new String[] { "address", _HomeAddress.Text });
                KeyValues.Add(4, new String[] { "dob", _DOB.Date.ToString("yyyy-MM-dd") });
                KeyValues.Add(5, new String[] { "password", GD.Password });
                KeyValues.Add(6, new String[] { "username", _Username.Text });
                if (!String.IsNullOrEmpty(GD.Email)) KeyValues.Add(7, new String[] { "email", GD.Email });
                KeyValues.Add(8, new String[] { "blood_group", _BloodGroup.SelectedItem.ToString() });
                //KeyValues.Add(9, new String[] { "bio_data", _BioData.Text });
                KeyValues.Add(9, new String[] { "medic_category", MedicCategoryID.ToString() });
                KeyValues.Add(10, new String[] { "is_medic", "1" });
                IsCompleted = true;

            }
            catch
            {
                await DisplayAlert("Alert", "Please Fill All Fields", "Okay");
            }
            if (IsCompleted)
            {
                try
                {
                    FormUrlEncodedContent Data = Utilities.PostDataEncoder(KeyValues);
                    //get the result of the signup 0 = status, 1 = message
                    var Result = await SignUpController.SignUp(Data);
                    if (Convert.ToBoolean(Result[0]))
                    {
                        await Utilities.CreateAlertDialog("Alert", Result[1].ToString(), "Ok", delegate
                        {
                            Navigation.PushAsync(new LoginPage());
                            Navigation.RemovePage(this);
                        });
                    }
                    else
                    {
                        await DisplayAlert("Alert", Result[1].ToString(), "Okay");
                    }
                }
                catch (System.Net.WebException WebEx)
                {
                    await DisplayAlert("Alert", "Unable to connect to the server, Please check the internet connection. ", "Okay");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alert", "MPPDP Error, Please Contact Admin", "Okay");
                }
            }
        }

    }
}