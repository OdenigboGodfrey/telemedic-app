using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Telemedic.Model
{
    static class GeneralModel
    {
        async public static Task<String> PostAsync(FormUrlEncodedContent Content,String WebURI)
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri(Utilities.BaseAddress);

            var res = await Client.PostAsync(Client.BaseAddress + WebURI, Content);
            return res.Content.ReadAsStringAsync().Result;
        }

        async public static Task<String> GetAsync(String WebURI)
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri(Utilities.BaseAddress);

            var res = await Client.GetAsync(Client.BaseAddress + WebURI);

            return res.Content.ReadAsStringAsync().Result;
        }
    }
}
