using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Telemedic.Model
{
    static class TransactionModel
    {
        //user/transaction-history/

        private static String WebURI = "user/fund/";

        async public static Task<String> FundAccount(FormUrlEncodedContent Content)
        {
            WebURI = "user/fund/";

            var Client = new HttpClient();
            Client.BaseAddress = new Uri(Utilities.BaseAddress);

            var res = await Client.PostAsync(Client.BaseAddress + WebURI, Content);
            return res.Content.ReadAsStringAsync().Result;
        }

        async public static Task<String> TransactionHistory(FormUrlEncodedContent Content)
        {
            WebURI = "user/transaction-history/";

            var Client = new HttpClient();
            Client.BaseAddress = new Uri(Utilities.BaseAddress);

            var res = await Client.PostAsync(Client.BaseAddress + WebURI, Content);
            return res.Content.ReadAsStringAsync().Result;
        }
    }
}
