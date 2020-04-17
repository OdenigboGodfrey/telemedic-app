using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Telemedic.Model;
using Telemedic.Templates.Enums;
using Telemedic.Templates;

namespace Telemedic.Controller
{
    static class MedicalRecordController
    {
        public static async Task GetMedicalRecords(RecordType UserRecordType, StackLayout ParentStack)
        {

            Dictionary<String, String> ResultingData = new Dictionary<String, String>();

            Dictionary<int, String[]> Data = new Dictionary<int, string[]>
            {
                { 0 , new String[]{ "user_id", Utilities.ID.ToString() } },
            };

            String URL = "";
            switch (UserRecordType)
            {
                case RecordType.Personal:
                    URL = "user/medical-records/search";
                    break;
                case RecordType.Drug:
                    URL = "user/prescribtions/search";
                    break;
                case RecordType.Lab:
                    URL = "user/lab-results/search";
                    break;
                case RecordType.Medical:
                    URL = "user/medical-records/search";
                    break;
            }

            try
            {
                int NoOfChangesMade = 0;

                String ResultJson = await GeneralModel.PostAsync(Utilities.PostDataEncoder(Data), URL);
                

                var DecodedJson = JObject.Parse(ResultJson);

                if ((int)DecodedJson["records"] > 0)
                {
                    foreach (var Child in DecodedJson["results"])
                    {
                        ///StoredContent returns as e.g "{Personal/Medical/Drug/Lab}{Title [length=17]:Title goes here}{Content [lenght=42]:Content goes here}"
                        String StoredContent = Child["description"].ToString();


                        String CurrentRecordType = StoredContent.Substring((StoredContent.IndexOf('{') + 1), ((StoredContent.IndexOf('}') - 1)));

                        ///if record isnt personal when user clicks "Personal Record" skip
                        if (UserRecordType == RecordType.Personal)
                        {
                            if (CurrentRecordType != Enum.GetName(typeof(RecordType), UserRecordType))
                            {
                                continue;
                            }
                        }
                        else if (UserRecordType == RecordType.Medical)
                        {
                            if (CurrentRecordType != Enum.GetName(typeof(RecordType), UserRecordType))
                            {
                                continue;
                            }
                        }
                        


                        ///strip out the record type
                        StoredContent = StoredContent.Replace("{" + CurrentRecordType + "}", "");

                        ///{Title [length=17]:Title goes here}


                        ///Title = "Title [length=17]:Title goes here"
                        String Title = StoredContent.Substring(StoredContent.IndexOf('{') + 1, ((StoredContent.IndexOf('}') - 1)));

                        ///get the length of the title e.g "Title goes here" == 17 i.e [length=17]
                        int TitleLength = Convert.ToInt32(Title.Substring(Title.IndexOf('[') + 1, ((Title.IndexOf(']')) - (Title.IndexOf('[') + 1))).Replace("length=", ""));

                        ///Title = Title goes here
                        Title = StoredContent.Substring((StoredContent.IndexOf(':') + 1), TitleLength);

                        ///strip out title  
                        StoredContent = StoredContent.Replace("{Title [length=" + TitleLength + "]:" + Title + "}", "");

                        String Content = StoredContent.Substring(StoredContent.IndexOf('{') + 1, ((StoredContent.IndexOf('}') - 1)));

                        int ContentLength = Convert.ToInt32(Content.Substring(Content.IndexOf('[') + 1, ((Content.IndexOf(']')) - (Content.IndexOf('[') + 1))).Replace("length=", ""));

                        Content = StoredContent.Substring((StoredContent.IndexOf(':') + 1), ContentLength);

                        NoOfChangesMade++;

                        Device.BeginInvokeOnMainThread(() => 
                        {
                            ParentStack.Children.Add(RecordTemplate.RecordTemplate01((int)Child["medic"]["id"], Title, Content, false));
                        });
                    }

                    if (NoOfChangesMade == 0)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            ParentStack.Children.Add(new Label { Text = "No Records in this section currently.", Style = App.Current.Resources["_EmptyLabelTemplate"] as Style });
                        });
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() => 
                    {
                        ParentStack.Children.Add(new Label { Text = "No Records in this section currently.", Style = App.Current.Resources["_EmptyLabelTemplate"] as Style });
                    });
                }
                
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.StackTrace, "Ok");
            }
        }

        public static async Task<bool> PostMedicalRecords(String Record, String Title, int UserID, RecordType UserRecordType)
        {
            ///for entity id, if personal currently using a default medic id
            Dictionary<int, String[]> Data = new Dictionary<int, string[]>
            {
                { 0 , new String[]{ "user_id", (UserRecordType == RecordType.Personal)? Utilities.ID.ToString() : UserID.ToString() } },
                { 1 , new String[]{ "entity_id",(UserRecordType == RecordType.Personal) ? "1" : Utilities.MedicID.ToString() } },
                { 2 , new String[]{ "description", "{"+ Enum.GetName(typeof(RecordType), UserRecordType) +"}{Title [length="+ Title.Length +"]:"+ Title +"}{Content [length="+ Record.Length +"]:" + Record + "}" } },
            };

            try
            {
                String URL = "";
                switch (UserRecordType)
                {
                    case RecordType.Personal:
                        URL = "user/medical-records/new";
                        break;
                    case RecordType.Drug:
                        URL = "user/prescribtions/new";
                        break;
                    case RecordType.Lab:
                        URL = "user/lab-results/new";
                        break;
                    case RecordType.Medical:
                        URL = "user/medical-records/new";
                        break;
                }

                String ResultJson = await GeneralModel.PostAsync(Utilities.PostDataEncoder(Data), URL);

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
