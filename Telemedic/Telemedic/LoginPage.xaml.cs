using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Telemedic.Model.Local;
using Plugin.Connectivity;
using Telemedic.Controller;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace Telemedic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Utilities.IsLoggedIn)
            {
                Navigation.PushAsync(new HomePage(Utilities.ID, Utilities.IsMedic));
                Navigation.RemovePage(this);
            }
        }

        private async void HandleLogin(object sender, EventArgs e)
        {
            await HandleLoginAction();
            //await MedicalRecordController.GetMedicalRecords(Templates.Enums.RecordType.Personal, new StackLayout());
        }

        private async Task HandleLoginAction()
        {
            bool isUserEmpty = string.IsNullOrEmpty(nUser.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(nPassword.Text);

            if (isUserEmpty || isPasswordEmpty)
            {
                if (isUserEmpty) nUser.Focus(); nUser.PlaceholderColor = Color.Red;
                if (isPasswordEmpty) nPassword.Focus(); nPassword.PlaceholderColor = Color.Red;
                await DisplayAlert("Alert", "Pleae fill the RED fields", "Ok");
            }
            else
            {
                ///no internet access
                ///local sign in
                if (!CrossConnectivity.Current.IsConnected)
                {
                    String Hashed = "";
                    using (var sha = SHA512.Create())
                    {
                        using (var md5 = MD5.Create())
                        {
                            Hashed = Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(nPassword.Text))))));
                        }
                    }

                    ///verification
                    var UserInfo = await LoginHandler.LoginInfo(nUser.Text, Hashed);
                    if (UserInfo.Count > 0)
                    {
                        if (UserInfo[0].MedicID != 0) Utilities.MedicID = UserInfo[0].MedicID;

                        await Navigation.PushAsync(new HomePage(UserInfo[0].UserID, (UserInfo[0].MedicID != 0)));

                        Navigation.RemovePage(this);
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Please create an account when you are connected before you can use this application offline.", "Ok");
                    }

                }
                else
                {
                    Dictionary<int, String[]> KeyValues = new Dictionary<int, string[]>();
                    KeyValues.Add(0, new String[] { "password", nPassword.Text });
                    KeyValues.Add(1, new String[] { "user", nUser.Text });
                    FormUrlEncodedContent Data = Utilities.PostDataEncoder(KeyValues);

                    String ResponseJson = "";
                    try
                    {
                        ResponseJson = await Model.LoginModel.Login(Data);
                        var DecodedJson = JObject.Parse(ResponseJson.ToString());
                        
                        if (Convert.ToBoolean(DecodedJson["status"]))
                        {
                            ///request needed permission of not granted
                            bool StoragePermissionGranted = await Utilities.CheckPermission(Permission.Storage, Utilities.ApplicationName + " would need access to device storage.");

                            Utilities.IsLoggedIn = true;
                            Utilities.IsMedic = Convert.ToBoolean(DecodedJson["info"]["is_medic"]);
                            Utilities.ID = (int)DecodedJson["info"]["id"];

                            if (Utilities.IsMedic) Utilities.MedicID = (int)DecodedJson["info"]["medic_id"];



                            ///save info to local db

                            using (var sha = SHA512.Create())
                            {
                                using (var md5 = MD5.Create())
                                {
                                    String Hashed = Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(nPassword.Text))))));

                                    var UserInfo = await LoginHandler.LoginInfo(nUser.Text, Hashed);
                                    if (UserInfo.Count == 0)
                                    {
                                        await LoginHandler.InsertNewLoginInfo(nUser.Text, Hashed, (int)DecodedJson["info"]["id"], (Utilities.IsMedic) ? (int)DecodedJson["info"]["medic_id"] : 0, DecodedJson["info"]["name"].ToString());
                                        var UserDetails = await SettingsController.GetSettings();
                                        await UserDetailsHandler.SaveUserDetails(new UserDetailsModel { Address = UserDetails["address"], DOB = UserDetails["dob"], Email = UserDetails["email"], FullName = UserDetails["name"], PhoneNo = UserDetails["phone"], UserID = Utilities.ID });
                                    }
                                }
                            }



                            await Navigation.PushAsync(new HomePage((int)DecodedJson["info"]["id"], Utilities.IsMedic));

                            //await ChatHandler.GetLastUniqueConversation();

                            Navigation.RemovePage(this);
                        }
                        else
                        {
                            await DisplayAlert("Alert", DecodedJson["message"].ToString(), "Okay");
                        }
                    }
                    catch (System.Net.WebException WebEx)
                    {
                        await DisplayAlert("Alert", WebEx.Message + "\n" + Utilities.BaseAddress + "\n" + ResponseJson, "Okay");
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Alert", ex.StackTrace + "\n" + Utilities.BaseAddress + "\n" + ResponseJson, "Okay");
                    }
                }
            }
        }

        private async void HandleSignup(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }

        private void ForgotPassword_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPasswordPage());
            //Navigation.PushAsync(new ChatPage(0,Utilities.IsMedic));
        }
    }
}