using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Telemedic.Model
{
    static class SignUpModel
    {
        private static String WebURI = "user/signup/";


        async public static Task<String> SignUp(FormUrlEncodedContent Content)
        {
            WebURI = "user/signup/";

            var Client = new HttpClient();
            Client.BaseAddress = new Uri(Utilities.BaseAddress);

            var res = await Client.PostAsync(Client.BaseAddress + WebURI, Content);
            return res.Content.ReadAsStringAsync().Result;
        }

        async public static Task<String> CheckUniqueFields(FormUrlEncodedContent Content,String FieldName)
        {
            switch (FieldName)
            {
                case "phone-no":
                    WebURI = "user/check/phone";
                    break;
                case "e-mail":
                    WebURI = "user/check/email";
                    break;
                case "user":
                    WebURI = "user/check/user";
                    break;
            }

            
            var Client = new HttpClient();
            Client.BaseAddress = new Uri(Utilities.BaseAddress);

            var res = await Client.PostAsync(Client.BaseAddress + WebURI, Content);
            return res.Content.ReadAsStringAsync().Result;
        }
    }
}
