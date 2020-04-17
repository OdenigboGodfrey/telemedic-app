using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Telemedic.Model;
using Telemedic.Templates;
using Telemedic.Templates.Enums;
using Telemedic.Model.Local;

using Xamarin.Forms;
using Plugin.Connectivity;

namespace Telemedic.Controller
{
    static class ChatController
    {
        public static bool ListenForNewConversation = true, ListenForChatListUpdate = true;
        private static int LastServerChatId = 0;


        public static async Task<bool> SendChat(int ReceiverID, String Message)
        {
            JObject DecodedJson = null;
            DateTime Now = DateTime.Now;
            String ResponseJson = "";

            Dictionary<int, String[]> Data = new Dictionary<int, string[]>
            {
                { 0 , new String[]{ "sender", Utilities.ID.ToString() } },
                { 1 , new String[]{ "receiver", ReceiverID.ToString() } },
                { 2 , new String[]{ "message", Message } },
                { 3 , new String[]{ "time",  Now.ToString("yyyy-MM-dd hh:mm:ss") } },
            };

            try
            {
                ResponseJson = await ChatModel.Chat(Utilities.PostDataEncoder(Data), "chat/");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }


            if (CrossConnectivity.IsSupported && CrossConnectivity.Current.IsConnected)
            {
                DecodedJson = JObject.Parse(ResponseJson);
            }
            else
            {
                DecodedJson = JObject.Parse(@"{ status : false }");

                //store the message in local db for upload when there is internet connection
                //not implemented yet
                await ChatHandler.InsertMessage(Message, Now.ToString("yyyy-MM-dd hh:mm:ss"), true, ReceiverID, MessageType.Text, false, LastServerChatId, Utilities.ID + "-" + ReceiverID);
            }


            return Convert.ToBoolean(DecodedJson["status"]);
        }

        /// <summary>
        /// LoadConversationToStack Loads the users conversation
        /// </summary>
        /// <param name="Stack"> the parent stack</param>
        /// <param name="RecepientID"> the ID of the receiver</param>
        public static async Task LoadConversationToStack(StackLayout Stack, int RecepientID, bool IsLocalLoaded, Grid ChatControlGrid, int LastID = 0)
        {
            int LastMessageID = LastID;

            JObject ConversationDecodedJson = new JObject();
            String ConversationResponseJson = "";

            //if false, then local db is empty else info is loaded from local db
            if (!IsLocalLoaded)
            {
                Device.BeginInvokeOnMainThread(delegate { Stack.Children.Clear(); });

                System.Diagnostics.Debug.WriteLine("IN IF, Old Last Server " + LastServerChatId);

                ConversationResponseJson = await ChatModel.Chat(Utilities.PostDataEncoder(new Dictionary<int, string[]>
                {
                    { 0 , new String[]{ "sender", Utilities.ID.ToString() } },
                    { 1 , new String[]{ "receiver", RecepientID.ToString() } },
                }), "chat/conversations");

                ConversationDecodedJson = JObject.Parse(ConversationResponseJson);

                LastMessageID = (int)ConversationDecodedJson["conversations"].Last["id"];

                var LastMessageDate = DateTime.Parse(ConversationDecodedJson["conversations"].Last["time"].ToString());

                if ((DateTime.Now.Day - LastMessageDate.Day) >= 7)///one week since last chat disabled messaging
                {
                    Device.BeginInvokeOnMainThread(delegate
                    {
                        ChatControlGrid.IsVisible = false;
                    });
                }

                await Utilities.RunTask(async delegate
                {
                    foreach (var conversation in ConversationDecodedJson["conversations"])
                    {
                        await ChatHandler.InsertMessage(conversation["message"].ToString(), conversation["time"].ToString(), (bool)conversation["sent"], RecepientID, MessageType.Text, true, (int)conversation["id"], Utilities.ID.ToString() + "-" + RecepientID);

                        if (conversation["message"].ToString() == "{Appointment Accepted, Default Message}")
                        {
                            var ChildStack = new Grid { HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(10, 2, 10, 2) };

                            ChildStack.Children.Add(new Label
                            {
                                Text = conversation["message"].ToString().Replace(", Default Message", "").Replace("{", "").Replace("}", ""),
                                BackgroundColor = (Color)App.Current.Resources["_MedAppBlack"],
                                TextColor = Color.White,
                                HorizontalTextAlignment = TextAlignment.Center,
                                VerticalTextAlignment = TextAlignment.Center,
                                HeightRequest = 30,
                                HorizontalOptions = LayoutOptions.FillAndExpand
                            });

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Stack.Children.Add(ChildStack);
                                ControlTagger<String>.SetTag(Stack, conversation["id"].ToString());
                            });
                        }
                        else
                        {
                            DateTime MessageTime = DateTime.Parse(conversation["time"].ToString());
                            String MessageTimeString = "";
                            if (MessageTime.Day != DateTime.Now.Day && MessageTime.Month == DateTime.Now.Month)
                            {
                                MessageTimeString = MessageTime.ToString("");
                            }
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Stack.Children.Add(SingleMessageTemplate.CreateMessage01((Convert.ToBoolean(conversation["sent"]) ? 1 : 0), conversation["message"].ToString(), Utilities.GetValidatedTime(MessageTime, ChatSectionType.Chat)));
                                ControlTagger<String>.SetTag(Stack, conversation["id"].ToString());
                            });
                        }
                    }
                });
            }

            ///Update chat in Background
            while (ListenForNewConversation)
            {

                ConversationResponseJson = await ChatModel.Chat(Utilities.PostDataEncoder(new Dictionary<int, string[]>
                {
                    { 0 , new String[]{ "sender", Utilities.ID.ToString() } },
                    { 1 , new String[]{ "receiver", RecepientID.ToString() } },
                }), "chat/conversations");

                ConversationDecodedJson = JObject.Parse(ConversationResponseJson);

                if (LastMessageID < (int)ConversationDecodedJson["conversations"].Last["id"])
                {
                    await Utilities.RunTask(async delegate
                    {
                        foreach (var conversation in ConversationDecodedJson["conversations"])
                        {
                            if ((int)conversation["id"] > LastMessageID)
                            {
                                LastServerChatId = (int)conversation["id"];

                                ///save updates locally
                                await ChatHandler.InsertMessage(conversation["message"].ToString(), conversation["time"].ToString(), (bool)conversation["sent"], RecepientID, MessageType.Text, true, (int)conversation["id"], Utilities.ID + "-" + RecepientID);

                                if (conversation["message"].ToString() == "{Appointment Accepted, Default Message}")
                                {

                                    var ChildStack = new Grid { HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(10, 2, 10, 2) };

                                    ChildStack.Children.Add(new Label
                                    {
                                        Text = conversation["message"].ToString().Replace(", Default Message", "").Replace("{", "").Replace("}", ""),
                                        BackgroundColor = (Color)App.Current.Resources["_MedAppBlack"],
                                        TextColor = Color.White,
                                        HorizontalTextAlignment = TextAlignment.Center,
                                        VerticalTextAlignment = TextAlignment.Center,
                                        HeightRequest = 30,
                                        HorizontalOptions = LayoutOptions.FillAndExpand
                                    });

                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        Stack.Children.Add(ChildStack);
                                        ControlTagger<int>.SetTag(Stack, (int)conversation["id"]);
                                    });
                                }
                                else
                                {
                                    DateTime MessageTime = DateTime.Parse(conversation["time"].ToString());
                                    String MessageTimeString = "";
                                    if (MessageTime.Day != DateTime.Now.Day && MessageTime.Month == DateTime.Now.Month)
                                    {
                                        MessageTimeString = MessageTime.ToString("");
                                    }
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        Stack.Children.Add(SingleMessageTemplate.CreateMessage01((Convert.ToBoolean(conversation["sent"]) ? 1 : 0), conversation["message"].ToString(), Utilities.GetValidatedTime(MessageTime, ChatSectionType.Chat)));
                                        ControlTagger<int>.SetTag(Stack, (int)conversation["id"]);
                                    });
                                }
                            }
                        }
                    });

                    LastMessageID = (int)ConversationDecodedJson["conversations"].Last["id"];
                }
            }
        }

        /// <summary>
        /// ChatsListToStack loads all the users conversation onto the specified stacklayout
        /// </summary>
        /// <param name="Stack"></param>
        /// <param name="Con"></param>
        public static async Task ChatsListToStack(StackLayout Stack, ContentPage Con, bool IsLocalLoaded = false)
        {
            if (!IsLocalLoaded) Device.BeginInvokeOnMainThread(() => { Stack.Children.Clear(); });

            do
            {
                System.Diagnostics.Debug.WriteLine("Stack Child COunt" + Stack.Children.Count);
                try
                {
                    Dictionary<int, String[]> ChatData = new Dictionary<int, string[]>();

                    Dictionary<int, String[]> Data = new Dictionary<int, string[]>
                    {
                        { 0 , new String[]{ "sender", Utilities.ID.ToString() } },
                    };

                    String ChatListResponseJson = await ChatModel.Chat(Utilities.PostDataEncoder(Data), "chat/get-list");

                    var ChatListDecodedJson = JObject.Parse(ChatListResponseJson);

                    foreach (var Contact in ChatListDecodedJson["contacts"])
                    {
                        ChatData.Add((int)Contact["info"]["id"], new string[5]);// key = receivers id
                        ChatData[(int)Contact["info"]["id"]][0] = Contact["info"]["name"].ToString(); //index 0 = name

                        String ConversationResponseJson = await ChatModel.Chat(Utilities.PostDataEncoder(new Dictionary<int, string[]>
                        {
                            { 0 , new String[]{ "sender", Utilities.ID.ToString() } },
                            { 1 , new String[]{ "receiver", Contact["info"]["id"].ToString() } },
                        }), "chat/conversations");

                        //System.Diagnostics.Debug.WriteLine(ConversationResponseJson);

                        var ConversationDecodedJson = JObject.Parse(ConversationResponseJson);

                        //System.Diagnostics.Debug.WriteLine("JSON" + ConversationDecodedJson["conversations"].Last);
                        //System.Diagnostics.Debug.WriteLine("JSON" + ConversationDecodedJson["conversations"].Last["message"]);

                        if (ConversationDecodedJson["conversations"].Last["message"].ToString() == "{Appointment Accepted, Default Message}")
                        {
                            ChatData[(int)Contact["info"]["id"]][1] = ConversationDecodedJson["conversations"].Last["message"].ToString();//index 1 = message

                        }
                        else
                        {
                            ChatData[(int)Contact["info"]["id"]][1] = ((bool)ConversationDecodedJson["conversations"].Last["sent"] ? "You: " : "");
                            ChatData[(int)Contact["info"]["id"]][1] += ConversationDecodedJson["conversations"].Last["message"].ToString();
                        }

                        //System.Diagnostics.Debug.WriteLine("DICT" + ChatData[(int)Contact["info"]["id"]][1]);


                        ChatData[(int)Contact["info"]["id"]][2] = ConversationDecodedJson["conversations"].Last["time"].ToString(); // index 2 = date

                        try
                        {
                            ChatData[(int)Contact["info"]["id"]][3] = Contact["info"]["medic_id"].ToString(); //index 3 = medic id
                        }
                        catch
                        {
                            ///exception thrown and caught as the current receiver doesnt have a medic id since he/she is a normal user(patient)
                            ChatData[(int)Contact["info"]["id"]][3] = "0";
                        }

                        //last conversation id, index 4 = id of the last message for the current convo on the online db
                        ChatData[(int)Contact["info"]["id"]][4] = ConversationDecodedJson["conversations"].Last["id"].ToString(); 

                        ///if local receiver doesnt exist, add to local db
                        int Result = await ReceiverHandler.CheckReceiver((int)Contact["info"]["id"]);
                        if (Result == 0)
                        {
                            await ReceiverHandler.NewReceiver(ChatData[(int)Contact["info"]["id"]][0], (int)Contact["info"]["id"], "", Convert.ToInt32(ChatData[(int)Contact["info"]["id"]][3]));
                        }

                    }

                    await Utilities.RunTask(delegate
                    {
                        if (ChatData.Count > 0)
                        {

                            foreach (int key in ChatData.Keys)
                            {
                                if (Stack.Children.Count >= 4) continue;
                                System.Diagnostics.Debug.WriteLine(" Count " + ChatData.Count + " [0] " + ChatData[key][0] + " [1] " + ChatData[key][1] + " [2] " + ChatData[key][2] + " [3] " + ChatData[key][3] + " [4] " + ChatData[key][4]);

                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    
                                    StackLayout ChildStack;

                                    if (Stack.Children.Count > 0 && Stack.Children[0].GetType() == typeof(Label))
                                    {
                                        ///clear message stack if default 'no chat message is shown'
                                        Stack.Children.Clear();
                                    }

                                    if (Stack.Children.Count > 0)
                                    {
                                        ///IsNewConversation is true by default,  the value is changed if the conversation exists on the chat list stack
                                        bool IsNewConversation = true;

                                        for (int i = 0; i < Stack.Children.Count; i++)
                                        {
                                            var child = Stack.Children[i];

                                            //if the child is already on stack, recreate that child if different values
                                            String[] ChildStackTag = ControlTagger<String>.GetTag(child).Split('-');
                                            

                                            ///receiver exists in stack
                                            if (key == Convert.ToInt32(ChildStackTag[1]))
                                            {
                                                System.Diagnostics.Debug.WriteLine("Is Old Convo :- False");

                                                int NewMessageCount = await GetCountOfNewMessages(key, Convert.ToInt32(ChildStackTag[2]));

                                                System.Diagnostics.Debug.WriteLine("Child in Stack With Tag [" + ChildStackTag[0] + "-" + ChildStackTag[1] + "-" + ChildStackTag[2] + "-" + ChildStackTag[3] + "] Tag [" + Utilities.ID + "-" + key + "-" + ChatData[key][4] + "-" + NewMessageCount + "] Last Server ID " + Convert.ToInt32(ChatData[key][4]));

                                                //receiver
                                                if ((Convert.ToInt32(ChildStackTag[2]) < Convert.ToInt32(ChatData[key][4])))
                                                {

                                                    /// create notification to show mew message
                                                    List<Object> ListForNotification = new List<object>();
                                                    if (ChatData[key][1].Equals("{Appointment Accepted, Default Message}"))
                                                    {
                                                        ListForNotification.Add(ChatData[key][1].Replace(", Default Message", "").Replace("{", "").Replace("}", ""));//index 0 message
                                                    }
                                                    else
                                                    {
                                                        //ListForNotification.Add(ChatData[key][1]);//index 0 message
                                                        ListForNotification.Add("" + NewMessageCount + " new messages from "+ ChatData[key][0]);//index 0 message
                                                    }
                                                    
                                                    ListForNotification.Add(ChatData[key][2]);//index 1 time
                                                    ListForNotification.Add(NotificationType.Chat);//index 2 notificationtype
                                                    ListForNotification.Add(key);// index 3 id


                                                    NotificationPage.NotificationData.Add(NotificationPage.NotificationData.Count, ListForNotification);

                                                    Stack.Children.RemoveAt(i);

                                                    ChildStack = ListAllChatTemplate.CreateNewStackType01(ChatData[key][0], "", ChatData[key][1], Utilities.GetValidatedTime(DateTime.Parse(ChatData[key][2]),ChatSectionType.ChatList), Con, key, NewMessageCount, Convert.ToInt32(ChatData[key][3]));
                                                    //ChildStack = new StackLayout { BackgroundColor = Color.Tomato };
                                                    ControlTagger<String>.SetTag(ChildStack, Utilities.ID + "-" + key + "-" + ChatData[key][4] + "-" + NewMessageCount);

                                                    //Stack.Children.Add(ChildStack);
                                                    Stack.Children.Insert(0, ChildStack);
                                                }
                                                

                                                ///IsNewConversation is false as the current key exists in the chat list stack
                                                IsNewConversation = false;
                                            }

                                        }
                                        System.Diagnostics.Debug.WriteLine("Done, Status :- " + IsNewConversation);
                                        ///If IsNewConversation is true, this key is a new conversaton, create new childstack
                                        if (IsNewConversation)
                                        {
                                            ///{Appointment Accepted, Default Message} is the default sent message, create different view for it
                                            if (ChatData[key][1].Equals("{Appointment Accepted, Default Message}"))
                                            {
                                                ChatData[key][1] = ChatData[key][1].Replace(", Default Message", "").Replace("{", "").Replace("}", "");
                                                ChildStack = ListAllChatTemplate.CreateNewStackType01(ChatData[key][0], "", ChatData[key][1], Utilities.GetValidatedTime(DateTime.Parse(ChatData[key][2]), ChatSectionType.ChatList), Con, key, 0, Convert.ToInt32(ChatData[key][3]));
                                                ControlTagger<String>.SetTag(ChildStack, Utilities.ID + "-" + key + "-" + ChatData[key][4] + "-" + 0);
                                                Stack.Children.Insert(Stack.Children.Count, ChildStack);
                                            }
                                            else
                                            {
                                                ChildStack = ListAllChatTemplate.CreateNewStackType01(ChatData[key][0], "", ChatData[key][1], Utilities.GetValidatedTime(DateTime.Parse(ChatData[key][2]), ChatSectionType.ChatList), Con, key, 0, Convert.ToInt32(ChatData[key][3]));
                                                ControlTagger<String>.SetTag(ChildStack, Utilities.ID + "-" + key + "-" + ChatData[key][4] + "-" + 0);
                                                Stack.Children.Insert(Stack.Children.Count, ChildStack);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ///{Appointment Accepted, Default Message} is the default sent message, create different view for it
                                        if (ChatData[key][1].Equals("{Appointment Accepted, Default Message}"))
                                        {
                                            /// create notification to show the appointment has been accepted
                                            List<Object> ListForNotification = new List<object>();
                                            ListForNotification.Add(ChatData[key][1].Replace(", Default Message", "").Replace("{", "").Replace("}", ""));//index 0 message
                                            ListForNotification.Add(ChatData[key][2]);//index 1 time
                                            ListForNotification.Add(NotificationType.Appointment);//index 2 notificationtype
                                            ListForNotification.Add(key);// index 3 id


                                            NotificationPage.NotificationData.Add(NotificationPage.NotificationData.Count, ListForNotification);

                                            System.Diagnostics.Debug.WriteLine("Adding");

                                            ChatData[key][1] = ChatData[key][1].Replace(", Default Message", "").Replace("{", "").Replace("}", "");
                                            ChildStack = ListAllChatTemplate.CreateNewStackType01(ChatData[key][0], "", ChatData[key][1], Utilities.GetValidatedTime(DateTime.Parse(ChatData[key][2]), ChatSectionType.ChatList), Con, key, 0, Convert.ToInt32(ChatData[key][3]));
                                            ControlTagger<String>.SetTag(ChildStack, Utilities.ID + "-" + key + "-" + ChatData[key][4] + "-" + 0);
                                            Stack.Children.Insert(Stack.Children.Count, ChildStack);
                                        }
                                        else
                                        {
                                            ChildStack = ListAllChatTemplate.CreateNewStackType01(ChatData[key][0], "", ChatData[key][1], Utilities.GetValidatedTime(DateTime.Parse(ChatData[key][2]), ChatSectionType.ChatList), Con, key, 0, Convert.ToInt32(ChatData[key][3]));
                                            ControlTagger<String>.SetTag(ChildStack, Utilities.ID + "-" + key + "-" + ChatData[key][4] + "-" + 0);
                                            Stack.Children.Insert(Stack.Children.Count, ChildStack);
                                        }

                                    }
                                });
                            }
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                if (Stack.Children.Count == 0)
                                {
                                    Stack.Children.Add(new Label { Text = (!Utilities.IsMedic) ? "Chats will appear after booking an appointment." : "Chats will appear after accepting an appointment.", Style = App.Current.Resources["_EmptyLabelTemplate"] as Style });
                                }

                            });
                        }
                    });
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", ex.StackTrace, "Ok");
                }

            } while (ListenForChatListUpdate);

        }

        public static async Task<int> GetCountOfNewMessages(int ReceiverId, int LastServerChatID)
        {
            int Counter = 0;

            String ConversationResponseJson = await ChatModel.Chat(Utilities.PostDataEncoder(new Dictionary<int, string[]>
                {
                    { 0 , new String[]{ "sender", Utilities.ID.ToString() } },
                    { 1 , new String[]{ "receiver", ReceiverId.ToString() } },
                }), "chat/conversations");

            var ConversationDecodedJson = JObject.Parse(ConversationResponseJson);

            foreach (var Child in ConversationDecodedJson["conversations"])
            {
                if ((int)Child["id"] > LastServerChatID)
                {
                    Counter++;
                }
            }

            return Counter;
        }

        /// 
        /// Local Database Controllers
        /// 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Stack"></param>
        /// <param name="RecepientID"></param>
        /// <returns></returns>
        public static async Task<Dictionary<String, String>> LocalConversationToStack(StackLayout Stack, int RecepientID)
        {
            Device.BeginInvokeOnMainThread(delegate { Stack.Children.Clear(); });

            Dictionary<String, String> ReturnedResult = new Dictionary<string, string>();

            int LastServerChatID = 0;
            bool MadeChanges = false;

            var Result = await ChatHandler.GetConversation(RecepientID);

            if (Result.Count > 0)
            {
                await Utilities.RunTask(delegate
                {
                    foreach (KeyValuePair<int, List<Object>> Pair in Result)
                    {
                        LastServerChatID = (int)Pair.Value[6];

                        if (Pair.Value[0].ToString() == "{Appointment Accepted, Default Message}")
                        {
                            var ChildStack = new Grid { HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(10, 2, 10, 2) };

                            ChildStack.Children.Add(new Label
                            {
                                Text = Pair.Value[0].ToString().Replace(", Default Message", "").Replace("{", "").Replace("}", ""),
                                BackgroundColor = (Color)App.Current.Resources["_MedAppBlack"],
                                TextColor = Color.White,
                                HorizontalTextAlignment = TextAlignment.Center,
                                VerticalTextAlignment = TextAlignment.Center,
                                HeightRequest = 30,
                                HorizontalOptions = LayoutOptions.FillAndExpand
                            });

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Stack.Children.Add(ChildStack);
                            });
                        }
                        else
                        {
                            DateTime MessageTime = DateTime.Parse(Pair.Value[1].ToString());
                            String MessageTimeString = "";
                            if (MessageTime.Day != DateTime.Now.Day && MessageTime.Month == DateTime.Now.Month)
                            {
                                MessageTimeString = MessageTime.ToString("");
                            }
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Stack.Children.Add(SingleMessageTemplate.CreateMessage01((Convert.ToBoolean(Pair.Value[4]) ? 1 : 0), Pair.Value[0].ToString(), Utilities.GetValidatedTime(MessageTime, ChatSectionType.Chat)));
                                ControlTagger<String>.SetTag(Stack, Pair.Key.ToString());
                            });

                        }
                    }
                    System.Diagnostics.Debug.WriteLine("Done With Local");
                    System.Diagnostics.Debug.WriteLine("Last Local ID " + LastServerChatID);
                });

                MadeChanges = true;
            }

            ReturnedResult.Add("MadeChanges", MadeChanges.ToString());
            ReturnedResult.Add("LastID", LastServerChatID.ToString());

            return ReturnedResult;
        }

        /// <summary>
        /// ChatsListToStack loads all the users conversation onto the specified stacklayout from the local DB
        /// </summary>
        /// <param name="Stack"></param>
        /// <param name="Con"></param>
        public static async Task<bool> LocalChatsListToStack(StackLayout Stack, ContentPage Con)
        {
            Dictionary<int, String[]> ChatData = new Dictionary<int, string[]>();
            Dictionary<int, List<Object>> ReceiversData = new Dictionary<int, List<Object>>();

            //gather the last ConversationID to be used for sorting the data
            Dictionary<int, int> LastConversationID = new Dictionary<int, int>();


            bool MadeChanges = false;
            
            ///for chatlist get all the people user has conversed with
            ///receiverlist gets the information of all the people the current user has conversed with
            var ReceiversList = await ReceiverHandler.GetReceiversInfo();
            ///check if the current users chats is empty, if it is do not load into stack
            int MyChatCounter = await ChatHandler.MyChatCounter();

            //System.Diagnostics.Debug.WriteLine("MyChatCounter " + MyChatCounter);

            if (ReceiversList.Count > 0 && MyChatCounter > 0)
            {
                System.Diagnostics.Debug.WriteLine(ReceiversList.Count);

                Device.BeginInvokeOnMainThread(() =>
                {
                    Stack.Children.Clear();

                });

                ///1) get all conversations for the chat list all stack
                int ccc = 0;
                try
                {
                    foreach (KeyValuePair<int, List<object>> ReceiverInfo in ReceiversList)
                    {
                        //var LastMessageInfo = await ChatHandler.GetLastConversation((int)ReceiverInfo.Value[2]);
                        int ID = (int)ReceiverInfo.Value[2];
                        ID = (Utilities.ID == ID) ? (int)ReceiverInfo.Value[4] : ID;
                        //System.Diagnostics.Debug.WriteLine("Receiver INfo " + ReceiverInfo.Value.Count + "["+ Utilities.ID + "-" + ID +"]");
                        //System.Diagnostics.Debug.WriteLine("Receiver INfo Dets " + ReceiverInfo.Value[0] + ", " + ReceiverInfo.Value[1] + ", " + ReceiverInfo.Value[2] + ", " + ReceiverInfo.Value[3] + ", " + ReceiverInfo.Value[4] + " ");
                        var LastMessageInfo = await ChatHandler.GetLastUniqueConversation(ID);

                        ccc = LastMessageInfo.Count;

                        if (ReceiversData.Count == 0 || (!ReceiversData.ContainsKey(ID))) ReceiversData.Add(ID, LastMessageInfo);
                        ///add the last server chat id to a different dictionary for sorting
                        //System.Diagnostics.Debug.WriteLine("Last MEssage INfo " + LastMessageInfo.Count);
                        if (LastConversationID.Count == 0 || (!LastConversationID.ContainsKey(ID))) LastConversationID.Add(ID, (int)LastMessageInfo[7]);
                    }


                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "1st \n" + ex.Message + " : " + ccc, "Ok");
                }

                System.Diagnostics.Debug.WriteLine("==========================");

                ///2) sort conversations desc
                List<KeyValuePair<int, int>> SortedList = new List<KeyValuePair<int, int>>();
                try
                {
                    SortedList = new List<KeyValuePair<int, int>>(LastConversationID);

                    SortedList.Sort(delegate (KeyValuePair<int, int> FirstPair, KeyValuePair<int, int> SecondPair)
                    {
                        return FirstPair.Value.CompareTo(SecondPair.Value);
                    });
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "2nd \n" + ex.Message, "Ok");
                }



                ///3) populate stack based on sortedlist
                try
                {
                    foreach (KeyValuePair<int, int> Pair in SortedList)
                    {
                        int ReceiversID = (Utilities.ID == (int)ReceiversData[Pair.Key][4]) ? (int)ReceiversData[Pair.Key][6] : (int)ReceiversData[Pair.Key][4];
                        var MedicID = await ReceiverHandler.GetMedicID(ReceiversID);
                        var ReceiverInfo = await ReceiverHandler.GetReceiverInfoById(ReceiversID);

                        await Utilities.RunTask(delegate
                        {
                            StackLayout ChildStack;

                            //{Appointment Accepted, Default Message} is the default sent message, create different view for it
                            if (ReceiversData[Pair.Key][1].Equals("{Appointment Accepted, Default Message}"))
                            {

                                /// create notification to show the appointment has been accepted
                                List<Object> ListForNotification = new List<object>();
                                ListForNotification.Add(ReceiversData[Pair.Key][1].ToString().Replace(", Default Message", "").Replace("{", "").Replace("}", "") + " - " + ReceiverInfo[0].ReceiverName);//index 0 message
                                ListForNotification.Add(ReceiversData[Pair.Key][2]);//index 1 time
                                ListForNotification.Add(NotificationType.Appointment);//index 2 notificationtype
                                ListForNotification.Add(ReceiversID);// index 3 id


                                NotificationPage.NotificationData.Add(NotificationPage.NotificationData.Count, ListForNotification);


                                ReceiversData[Pair.Key][1] = ReceiversData[Pair.Key][1].ToString().Replace(", Default Message", "").Replace("{", "").Replace("}", "");
                                ChildStack = ListAllChatTemplate.CreateNewStackType01(ReceiverInfo[0].ReceiverName, "", ((bool)ReceiversData[Pair.Key][5]) ? "You: " + ReceiversData[Pair.Key][1].ToString() : ReceiversData[Pair.Key][1].ToString(), Utilities.GetValidatedTime(DateTime.Parse(ReceiversData[Pair.Key][2].ToString()), ChatSectionType.ChatList), Con, ReceiversID, 0, MedicID);

                                ControlTagger<String>.SetTag(ChildStack, Utilities.ID + "-" + Pair.Key + "-" + ReceiversData[Pair.Key][7] + "-0");
                                System.Diagnostics.Debug.WriteLine(Utilities.ID + "-" + Pair.Key + "-" + ReceiversData[Pair.Key][7] + "-0");
                                Device.BeginInvokeOnMainThread(delegate
                                {
                                    Stack.Children.Add(ChildStack);
                                });

                            }
                            else
                            {
                                ChildStack = ListAllChatTemplate.CreateNewStackType01(ReceiverInfo[0].ReceiverName, "", ((bool)ReceiversData[Pair.Key][5]) ? "You: " + ReceiversData[Pair.Key][1].ToString() : ReceiversData[Pair.Key][1].ToString(), Utilities.GetValidatedTime(DateTime.Parse(ReceiversData[Pair.Key][2].ToString()), ChatSectionType.ChatList), Con, ReceiversID, 0, MedicID);

                                System.Diagnostics.Debug.WriteLine(Utilities.ID + "-" + Pair.Key + "-" + ReceiversData[Pair.Key][7] + "-0"); ControlTagger<String>.SetTag(ChildStack, Utilities.ID + "-" + Pair.Key + "-" + ReceiversData[Pair.Key][7] + "-0");

                                Device.BeginInvokeOnMainThread(delegate
                                {
                                    Stack.Children.Add(ChildStack);
                                });
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "3rd \n" + ex.Message, "Ok");
                }

                MadeChanges = true;
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (Stack.Children.Count == 0)
                    {
                        Stack.Children.Add(new Label { Text = (!Utilities.IsMedic) ? "Chats will appear after booking an appointment." : "Chats will appear after accepting an appointment.", Style = App.Current.Resources["_EmptyLabelTemplate"] as Style });
                    }
                });
            }
            return MadeChanges;
        }


    }
}
