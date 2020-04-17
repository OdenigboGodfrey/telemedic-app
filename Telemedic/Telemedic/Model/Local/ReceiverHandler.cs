using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Telemedic.Model.Local
{
    static class ReceiverHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ReceiverID"></param>
        /// <param name="DisplayPicture"></param>
        /// <returns></returns>
        public static async Task<int> NewReceiver(String Name,int ReceiverID,String DisplayPicture,int MedicID)
        {
            return await LocalDB.DbCon.InsertAsync(new ReceiverTableModel { ReceiverID = ReceiverID, ReceiverName = Name, ReceiverDisplayPicture = DisplayPicture, MedicID = MedicID, SenderID = Utilities.ID });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<int>> GetReceiversById()
        {
            List<int> ReceiversList = new List<int>();

            var Result = await LocalDB.DbCon.QueryAsync<ReceiverTableModel>(@"SELECT * FROM ReceiverTableModel WHERE SENDERID = "+Utilities.ID);

            foreach (var Content in Result)
            {
                ReceiversList.Add(Content.ReceiverID);
            }

            return ReceiversList;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<ReceiverTableModel>> GetReceiverInfoById(int ReceiverID)
        {
            List<int> ReceiversList = new List<int>();

            var Result = await LocalDB.DbCon.QueryAsync<ReceiverTableModel>(@"SELECT * FROM ReceiverTableModel WHERE RECEIVERID = " + ReceiverID);

            return Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<Dictionary<int, List<Object>>> GetReceiversInfo()
        {
            Dictionary<int, List<Object>> Data = new Dictionary<int, List<object>>();

            var Result = await LocalDB.DbCon.QueryAsync<ReceiverTableModel>(@"SELECT * FROM ReceiverTableModel WHERE SENDERID = " + Utilities.ID + " OR RECEIVERID = " + Utilities.ID);

            foreach (var Content in Result)
            {
                Data.Add(Content.ID, new List<object>());
                Data[Content.ID].Add(Content.MedicID);//0 medic id
                Data[Content.ID].Add(Content.ReceiverDisplayPicture);//1 DP
                Data[Content.ID].Add(Content.ReceiverID);//2 Receiver ID
                Data[Content.ID].Add(Content.ReceiverName);//3 Receiver Name
                Data[Content.ID].Add(Content.SenderID);//4 senderid
            }

            return Data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReceiverID"></param>
        /// <returns></returns>
        public static async Task<int> CheckReceiver(int ReceiverID)
        {
            var Result = await LocalDB.DbCon.QueryAsync<ReceiverTableModel>(@"SELECT * FROM ReceiverTableModel WHERE SENDERID = "+ Utilities.ID +" AND RECEIVERID = " + ReceiverID + " LIMIT 1");

            return (Result.Count > 0) ? Convert.ToInt32(Result[0].ReceiverID) : 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReceiverID"></param>
        /// <returns></returns>
        public static async Task<int> GetMedicID(int ReceiverID)
        {
            var Result = await LocalDB.DbCon.QueryAsync<ReceiverTableModel>(@"SELECT * FROM ReceiverTableModel WHERE SENDERID = " + Utilities.ID + " AND RECEIVERID = " + ReceiverID + " LIMIT 1");

            return (Result.Count > 0) ? Convert.ToInt32(Result[0].MedicID) : 0;
        }
    }
}
