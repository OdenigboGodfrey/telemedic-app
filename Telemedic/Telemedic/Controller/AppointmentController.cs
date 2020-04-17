using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Telemedic.Templates;
using Telemedic.Templates.Enums;
using Telemedic.Model;

using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Telemedic.Controller
{
    class AppointmentController
    {
        public static async Task SearchMedicalPractioner(String Name, MedicalPractitionerType MPType,StackLayout Stack)
        {
            Device.BeginInvokeOnMainThread(delegate 
            {
                Stack.Children.Clear();
            });

            Dictionary<int, String[]> Data = new Dictionary<int, string[]>
            {
                { 0 , new String[]{ "name", Name } },
            };

            String ResponseJson = await GeneralModel.PostAsync(Utilities.PostDataEncoder(Data), (MPType == MedicalPractitionerType.Doctor) ? "user/search/medics" : "user/search/institutions");

            var DecodedJson = JObject.Parse(ResponseJson);

            await Utilities.RunTask(async delegate
            {
                foreach (var Medic in DecodedJson["medics"])
                {
                    String MedicBioData = (MPType == MedicalPractitionerType.Doctor) ? await MedicalPractitionerController.GetBioDetails((int)Medic["medic"]["id"]) : "No Bio Data Provided";
                    MedicBioData = (String.IsNullOrEmpty(MedicBioData) ? "No Bio Data provided." : MedicBioData);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Stack.Children.Add(MedPractListTemplate.ListTemplate02((int)Medic["medic"]["id"], MPType, Medic["name"].ToString(), MedicBioData.Substring(0, 25), new Random().Next(1, 6), Utilities.Source("doc_anim.jpg", typeof(AppointmentController)), 4.1, Medic["address"].ToString(), Convert.ToDouble(Medic["medic"]["charge"]), String.Empty));
                    });
                }
            });

            //no result
            Device.BeginInvokeOnMainThread(delegate
            {
                if (Stack.Children.Count == 0)
                {
                    Stack.Children.Add(new Label { Text = "Search returned no result. Please verify supplied name.", Style = App.Current.Resources["_EmptyLabelTemplate"] as Style });
                }
                
            });
        }

        public static async Task ListMedicalPractioner(StackLayout Stack, MedicalPractitionerType MPType)
        {
            Device.BeginInvokeOnMainThread(delegate { Stack.Children.Clear(); });

            //posting empty values as the PostAsync requires a FormUrlEncodedContent
            Dictionary<int, String[]> Data = new Dictionary<int, string[]>
            {
                { 0 , new String[]{ "n/a", "n/a" } },
            };

            String ResponseJson = await GeneralModel.PostAsync(Utilities.PostDataEncoder(Data),(MPType == MedicalPractitionerType.Doctor) ? "user/search/medics" : "user/search/institutions");
            
            var DecodedJson = JObject.Parse(ResponseJson);

            await Utilities.RunTask(async delegate 
            {
                foreach (var Medic in DecodedJson["medics"])
                {
                    String MedicBioData = (MPType == MedicalPractitionerType.Doctor) ? await MedicalPractitionerController.GetBioDetails((int)Medic["medic"]["id"]) : "No Bio Data Provided";
                    MedicBioData = (String.IsNullOrEmpty(MedicBioData) ? "No Bio Data provided." : MedicBioData);

                    

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ///only add the child if it is of the same specialty type
                        //if ( Enum.GetName(typeof(MedicalPractitionerType),MPType) ==  ((MPType == MedicalPractitionerType.Doctor) ? Enum.GetName(typeof(MedicalPractitionerType), Medic["medic"]["specialty"]) : Enum.GetName(typeof(MedicalPractitionerType), Medic["specialty"]))) Stack.Children.Add(MedPractListTemplate.ListTemplate02((MPType == MedicalPractitionerType.Doctor) ? (int)Medic["medic"]["id"] : (int)Medic["id"], MPType, Medic["name"].ToString(), (MedicBioData.Length >= 25) ? MedicBioData.Substring(0, 25) : MedicBioData, new Random().Next(1, 6), Utilities.Source("doc_anim.jpg", typeof(AppointmentController)), 4.1, Medic["address"].ToString(), (MPType == MedicalPractitionerType.Doctor) ? Convert.ToDouble(Medic["medic"]["charge"]) : 0, String.Empty));
                        if (MPType == MedicalPractitionerType.Doctor)
                        {
                            Stack.Children.Add(MedPractListTemplate.ListTemplate02((MPType == MedicalPractitionerType.Doctor) ? (int)Medic["medic"]["id"] : (int)Medic["id"], MPType, Medic["name"].ToString(), (MedicBioData.Length >= 25) ? MedicBioData.Substring(0, 25) : MedicBioData, new Random().Next(1, 6), Utilities.Source("doc_anim.jpg", typeof(AppointmentController)), 4.1, Medic["address"].ToString(), (MPType == MedicalPractitionerType.Doctor) ? Convert.ToDouble(Medic["medic"]["charge"]) : 0, String.Empty));
                        }
                        else
                        {
                            if (Enum.GetName(typeof(MedicalPractitionerType), MPType).ToLower() ==  Medic["category"].ToString().ToLower())
                            {
                                Stack.Children.Add(MedPractListTemplate.ListTemplate02( (int)Medic["id"], MPType, Medic["name"].ToString(), (MedicBioData.Length >= 25) ? MedicBioData.Substring(0, 25) : MedicBioData, new Random().Next(1, 6), Utilities.Source("doc_anim.jpg", typeof(AppointmentController)), 4.1, Medic["address"].ToString(), 0, String.Empty));
                            }
                        }
                        
                    });
                }
            });

            
            
        }

        public static async Task<bool> BookAppointment(int UserID,String DateBooked,TimeSpan TimeBooked, MedicalPractitionerType AppointmentType,int MedPRactID, String Description,String PhoneNo)
        {
            String Time12Hours = "",Meridiem = "";
            int hours = Convert.ToInt32(TimeBooked.ToString("hh"));
            if (hours > 12)
            {
                Time12Hours += (hours - 12);
                Meridiem += "PM";
            }
            else if (hours == 0)
            {
                Time12Hours += "12";
                Meridiem += "AM";
            }
            else if (hours == 12)
            {
                Time12Hours += "12";
                Meridiem += "PM";
            }
            else
            {
                Time12Hours += hours;
                Meridiem += "AM";
            }

            Time12Hours += ":" + TimeBooked.ToString("mm");
            Time12Hours += " ";
            Time12Hours +=  Meridiem;

            Dictionary<int, String[]> Data = new Dictionary<int, string[]>
            {
                { 0 , new String[]{ "user_id", Utilities.ID.ToString() } },
                { 1 , new String[]{ "entity_id", MedPRactID.ToString() } },
                { 2 , new String[]{ "appointment_type", (AppointmentType == MedicalPractitionerType.Doctor) ? "p" : "i" } },
                { 3 , new String[]{ "time", Time12Hours } },
                { 4 , new String[]{ "phone_number", PhoneNo } },
                { 5 , new String[]{ "description", Description } },
                { 6 , new String[]{ "date", DateBooked } },
            };

            try
            {
                String ResponseJson = await GeneralModel.PostAsync(Utilities.PostDataEncoder(Data), "user/appointments/new");
                var DecodedJson = JObject.Parse(ResponseJson);
                if (!(bool)DecodedJson["status"])
                {
                    await App.Current.MainPage.DisplayAlert("Alert", DecodedJson["message"].ToString(), "ok");
                }
                return Convert.ToBoolean(DecodedJson["status"]);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "ok");
                return false;
            }
        }

        public static async Task WaitingList(StackLayout Stack)
        {
            Device.BeginInvokeOnMainThread(delegate { Stack.Children.Clear(); });
            try
            {
                String ResponseJson = await AppointmentModel.WaitingList("user/appointments/?id=" + Utilities.MedicID);
                var DecodedJson = JObject.Parse(ResponseJson);

                if ((int)DecodedJson["records"] > 0)
                {
                    await Utilities.RunTask(delegate
                    {
                        foreach (var appointment in DecodedJson["appointments"])
                        {
                            Status CurrentStatus = Status.Approved;
                            switch ((int)appointment["status"])
                            {
                                case -1:
                                    CurrentStatus = Status.Declined;
                                    break;
                                case 0:
                                    CurrentStatus = Status.Waiting;
                                    break;
                            };

                            Device.BeginInvokeOnMainThread(delegate
                            {
                                Stack.Children.Add(WaitingRoomTemplate.WaitingTemplate02((int)appointment["user_id"], (int)appointment["id"], appointment["name"].ToString(), CurrentStatus, appointment["time"].ToString(), appointment["description"].ToString(), Utilities.Source("doc_anim.jpg", typeof(AppointmentController)), appointment["phone_number"].ToString()));
                            });
                        }
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Stack.Children.Add(new Label { Text = "No Patients in the waiting room.", Style = App.Current.Resources["_EmptyLabelTemplate"] as Style });
                    });
                }
                
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "ok");
            }
        }

        public static async Task<int> AppointmentStatus(int RequestStatus,int AppointmentID,int UserID)
        {
            String ResponseJson = "";

            Dictionary<int, String[]> Data = new Dictionary<int, string[]>
            {
                { 0 , new String[]{ "id", AppointmentID.ToString() } },
                { 1 , new String[]{ "status", RequestStatus.ToString () } },
            };

            if (RequestStatus == -1) Data.Add(2, new String[] { "remark", "Appointment Rejected." });

            try
            {
                if (RequestStatus == 0)
                {
                    await GeneralModel.PostAsync(Utilities.PostDataEncoder(Data), "user/appointments/");
                    return 0;
                }
                else
                {
                    ResponseJson = await GeneralModel.PostAsync(Utilities.PostDataEncoder(Data), "user/appointments/");

                    var DecodedJson = JObject.Parse(ResponseJson);
                    
                    //status message
                    if ((bool)DecodedJson["status"] && (int)DecodedJson["appointment_status"] == 1)
                    {
                        bool InitMessage = await ChatController.SendChat(UserID, "{Appointment Accepted, Default Message}");

                        if (InitMessage) return 1;
                        return 0;
                    }
                    else if ((bool)DecodedJson["status"] && (int)DecodedJson["appointment_status"] != 1)
                    {
                        return (int)DecodedJson["appointment_status"];
                    }
                    else
                    {
                        return -2;
                    }
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "ok");
                return -1;
            }

        }
    }
}
