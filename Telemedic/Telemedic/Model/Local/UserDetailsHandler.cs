using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Telemedic.Model.Local
{
    static class UserDetailsHandler
    {
        public static async Task SaveUserDetails(UserDetailsModel UserDetails)
        {
            await LocalDB.DbCon.InsertAsync(UserDetails);
        }

        public static async Task<UserDetailsModel> GetUserDetails()
        {
            var Result = await LocalDB.DbCon.QueryAsync<UserDetailsModel>(@"SELECT * FROM USERDETAILSMODEL WHERE USERID = " + Utilities.ID + " LIMIT 1");
            return Result[0];
        }
    }
}
