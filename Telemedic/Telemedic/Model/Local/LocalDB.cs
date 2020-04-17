using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLitePCL;


namespace Telemedic.Model.Local
{
    static class LocalDB
    {
        public static String DBLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Path.VolumeSeparatorChar + "UserPersonalDB.sql");
        public static SQLiteAsyncConnection DbCon;

        public static async Task CreateDB()
        {
            try
            {
                DbCon = new SQLiteAsyncConnection(DBLocation);

                await DbCon.CreateTableAsync<ChatTableModel>();
                await DbCon.CreateTableAsync<ReceiverTableModel>();
                await DbCon.CreateTableAsync<LoginTableModel>();
                await DbCon.CreateTableAsync<UserDetailsModel>();


                //var Listt = await DbCon.QueryAsync<ReceiverTableModel>("Select * from ReceiverTableModel");
                //String ss = "";
                //foreach (var i in Listt)
                //{
                //    ss += "ID " + i.ID + "\n";
                //    ss += "MedicID " + i.MedicID + "\n";
                //    ss += "ReceiverID " + i.ReceiverID + "\n";
                //    ss += "ReceiverName " + i.ReceiverName + "\n";
                //}

                //await App.Current.MainPage.DisplayAlert("Alert", "Receiver " + Listt.Count+"\n "+ss, "Ok");

                //var Listt2 = await DbCon.QueryAsync<ChatTableModel>("Select * from ChatTableModel");

                //await App.Current.MainPage.DisplayAlert("Alert", "Chat " + Listt2.Count, "Ok");

                //var Listt3 = await DbCon.QueryAsync<LoginTableModel>("Select * from LoginTableModel");

                //await App.Current.MainPage.DisplayAlert("Alert", "Login " + Listt3.Count, "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "" + ex.Message, "Ok");
            }

            
        }

    }
}
