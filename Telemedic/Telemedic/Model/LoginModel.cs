using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Telemedic.Model
{
    static class LoginModel
    {
        private const String WebURI = "user/login/";

        async public static Task<String> Login(FormUrlEncodedContent Content)
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri(Utilities.BaseAddress);

            var res = await Client.PostAsync(Client.BaseAddress + WebURI, Content);
            return res.Content.ReadAsStringAsync().Result;
        }
    }
}
