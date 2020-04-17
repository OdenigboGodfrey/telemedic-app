using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Telemedic.Model
{
    static class ChatModel
    {
        async public static Task<String> Chat(FormUrlEncodedContent Content,String route)
        {

            var Client = new HttpClient();
            Client.BaseAddress = new Uri(Utilities.BaseAddress);

            var res = await Client.PostAsync(Client.BaseAddress + route, Content);
            return res.Content.ReadAsStringAsync().Result;
        }
    }
}
