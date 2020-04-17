using System;
using System.Collections.Generic;
using System.Text;

namespace Telemedic.Templates.Enums
{
    /****/
    public enum TransactionType
    {
        Credit = 0,
        Debit = 1
    }

    /****/
    public enum HistoryType
    {
        Doctor = 0,
        Hospital = 1,
        Laboratory = 2,
        Pharmacy = 3
    }

    /****/
    public enum NotificationType
    {
        Chat = 0,
        Reminder = 1,
        Appointment = 2
    }

    /****/
    public enum Status
    {
        Approved = 0,
        Waiting = 1,
        Declined = 2
    }

    /****/
    public enum RecordType
    {
        Personal = 0,
        Medical = 1,
        Lab = 2,
        Drug = 3
    }

    /****/
    public enum MedicalPractitionerType
    {
        Doctor = 0,
        Laboratory = 1,
        Hospital = 2,
        Pharmacy = 3
    }

    /****/
    public enum ChatSectionType
    {
        Chat = 0,
        ChatList = 1,
    }

    /** used with the local Database for message type**/
    public enum MessageType
    {
        Text = 0,
        Image = 1,
        Audio = 2,
        Video = 3,
    }
}
