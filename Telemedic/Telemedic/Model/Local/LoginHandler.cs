using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Telemedic.Model.Local
{
    static class LoginHandler
    {
        public static async Task InsertNewLoginInfo(String LoginInfo,String Password, int UserID, int MedicID,String UserName)
        {
            await LocalDB.DbCon.InsertAsync(new LoginTableModel { LoginInfo = LoginInfo, Password = Password, UserID = UserID, MedicID = MedicID, UserName = UserName });
        }

        /// <summary>
        /// checks if the login info exists on the device
        /// </summary>
        /// <param name="LoginInfo"></param>
        /// <param name="Password"></param>
        /// <returns>User ID</returns>
        public static async Task<List<LoginTableModel>> LoginInfo(String LoginInfo, String Password)
        {
            var Result = await LocalDB.DbCon.QueryAsync<LoginTableModel>(@"SELECT * FROM LoginTableModel WHERE LOGININFO = '" + LoginInfo + "' AND PASSWORD = '" + Password + "' LIMIT 1");
            return Result;
        }

        public static async Task<String> GetMyName()
        {
            var Result = await LocalDB.DbCon.QueryAsync<LoginTableModel>(@"SELECT * FROM LoginTableModel WHERE USERID = " + Utilities.ID);

            return Result[0].UserName;
        }
    }
}
