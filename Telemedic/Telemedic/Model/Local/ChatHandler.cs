using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Telemedic.Templates.Enums;

namespace Telemedic.Model.Local
{
    static class ChatHandler
    {
        public static async Task InsertMessage(String Message,String MessageTime,bool Sent,int ReceiverID,MessageType UserMessageType, bool ServerSent, int LastServerChatID, String UniqueID)
        {
            await LocalDB.DbCon.InsertAsync(new ChatTableModel { Message = Message, MessageDate = MessageTime, Sent = Sent, ReceiverID = ReceiverID, MessageType = UserMessageType, SenderID = Utilities.ID, ServerSent = ServerSent, LastServerChatID = LastServerChatID, UniqueID = UniqueID });
        }

        public static async Task<Dictionary<int,List<Object>>> GetConversation(int ReceiverID)
        {
            Dictionary<int, List<Object>> Messages = new Dictionary<int, List<object>>();

            var Result = await LocalDB.DbCon.QueryAsync<ChatTableModel>(@"SELECT * FROM CHATTABLEMODEL WHERE RECEIVERID = " + ReceiverID + " OR SENDERID = " + ReceiverID);

            foreach (var Content in Result )
            {
                Messages.Add(Content.ID, new List<object>());
                Messages[Content.ID].Add(Content.Message);// 0 message
                Messages[Content.ID].Add(Content.MessageDate);//1 messagedate
                Messages[Content.ID].Add(Content.MessageType);//2 messagetype
                Messages[Content.ID].Add(Content.ReceiverID);//3 receiverid
                Messages[Content.ID].Add(Content.Sent);//4 sent
                Messages[Content.ID].Add(Content.SenderID);//5 senderID
                Messages[Content.ID].Add(Content.LastServerChatID);//6 last server conversation ID
            }

            return Messages;
        }

        public static async Task<List<Object>> GetLastConversation(int ReceiverID)
        {
            //Dictionary<int, List<Object>> MessagesInfo = new Dictionary<int, List<object>>();
            List<Object> _Messages = new List<object>();

            var Result = await LocalDB.DbCon.QueryAsync<ChatTableModel>(@"SELECT * FROM CHATTABLEMODEL WHERE RECEIVERID = " + ReceiverID + " ORDER BY ID DESC LIMIT 1");

            foreach (var Content in Result)
            {
                _Messages.Add(Content.ID);//0 local id
                _Messages.Add(Content.Message);//1 message
                _Messages.Add(Content.MessageDate);//2 message date
                _Messages.Add(Content.MessageType);//3 messagetype
                _Messages.Add(Content.ReceiverID);//4 receiverid
                _Messages.Add(Content.Sent);//5 sent
                _Messages.Add(Content.SenderID);//6 senderid
                _Messages.Add(Content.LastServerChatID);//7 LastServerChatID
            }

            return _Messages;
        }

        public static async Task<List<Object>> GetLastUniqueConversation(int ReceiverID)
        {
            List<Object> MessagesInfo = new List<object>();

            var Result = await LocalDB.DbCon.QueryAsync<ChatTableModel>(@"SELECT * FROM CHATTABLEMODEL WHERE UNIQUEID = '" + Utilities.ID + "-" + ReceiverID  + "' OR UNIQUEID = '"+ ReceiverID + "-" + Utilities.ID + "'  ORDER BY LastServerChatID DESC LIMIT 1");

            foreach (var Content in Result)
            {
                MessagesInfo.Add(Content.ID);//0 local id
                MessagesInfo.Add(Content.Message);//1 message
                MessagesInfo.Add(Content.MessageDate);//2 message date
                MessagesInfo.Add(Content.MessageType);//3 messagetype
                MessagesInfo.Add(Content.ReceiverID);//4 receiverid
                MessagesInfo.Add(Content.Sent);//5 sent
                MessagesInfo.Add(Content.SenderID);//6 senderid
                MessagesInfo.Add(Content.LastServerChatID);//7 LastServerChatID
            }

            //await App.Current.MainPage.DisplayAlert("Alert", "" + Result.Count, "Ok");
            return MessagesInfo;
        }

        public static async Task<int> MyChatCounter()
        {
            var Result = await LocalDB.DbCon.QueryAsync<ChatTableModel>("Select * from ChatTableModel WHERE SENDERID = " + Utilities.ID + " OR RECEIVERID = " + Utilities.ID);

            //foreach (var xx in Result)
            //{
            //    System.Diagnostics.Debug.WriteLine("Result [" + xx.ID + " " + xx.LastServerChatID + " " + xx.Message + " " + xx.MessageDate + " " + xx.MessageType + " " + xx.ReceiverID + " " + xx.SenderID + " " + xx.Sent + " " + xx.ServerSent + " " + xx.UniqueID + " " + "]");
            //}
            

            return Result.Count;
        }
    }
}
