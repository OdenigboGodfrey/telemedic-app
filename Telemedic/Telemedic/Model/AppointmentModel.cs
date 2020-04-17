using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Telemedic.Model
{
    static class AppointmentModel
    {
        async public static Task<String> WaitingList( String WebURI)
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri(Utilities.BaseAddress);

            var res = await Client.GetAsync(Client.BaseAddress + WebURI);
            return res.Content.ReadAsStringAsync().Result;
        }
    }
}
