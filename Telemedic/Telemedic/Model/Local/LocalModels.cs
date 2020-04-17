using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLitePCL;

using Telemedic.Templates.Enums;

namespace Telemedic.Model.Local
{

    public class ChatTableModel
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public String Message { get; set; }
        public bool Sent { get; set; }
        public String MessageDate { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public MessageType MessageType { get; set; }
        public int LastServerChatID { get; set; }
        public bool ServerSent { get; set; } = false;
        public String UniqueID { get; set; }
    }

    public class ReceiverTableModel
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public String ReceiverName { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public String ReceiverDisplayPicture { get; set; }
        public int MedicID { get; set; }
    }

    public class LoginTableModel
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public String LoginInfo { get; set; }
        public String Password { get; set; }
        public int UserID { get; set; }
        public int MedicID { get; set; }
        public String UserName { get; set; }
    }

    public class UserDetailsModel
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public int UserID { get; set; }
        public String DOB { get; set; }
        public String Email { get; set; }
        public String PhoneNo { get; set; }
        public String FullName { get; set; }
        public String Address { get; set; }
    }
}
