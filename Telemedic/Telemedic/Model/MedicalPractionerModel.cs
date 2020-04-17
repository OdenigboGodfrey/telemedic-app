using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Telemedic.Model
{
    static class MedicalPractionerModel
    {
        private static String WebURI = "user/get/medic-categories";

        async public static Task<String> MedicalPractitioners()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri(Utilities.BaseAddress);

            var res = await Client.PostAsync(Client.BaseAddress + WebURI, Utilities.PostDataEncoder(
                new Dictionary<int, string[]>
                {
                    { 0, new String[] { "name", "medic-categories" } }
                }));
            return res.Content.ReadAsStringAsync().Result;
        }

        //async public static Task<Dictionary<int,>> SearchMedicalPractioners(FormUrlEncodedContent Data)
        //{
        //    WebURI = "user/search/";
        //    var Client = new HttpClient();
        //    Client.BaseAddress = new Uri(Utilities.BaseAddress);

        //    var res = await Client.PostAsync(Client.BaseAddress + WebURI, Data);
        //    return res.Content.ReadAsStringAsync().Result;
        //}
    }
}
