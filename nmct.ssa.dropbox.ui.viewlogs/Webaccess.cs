using Newtonsoft.Json;
using nmct.ssa.dropbox.common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;

namespace nmct.ssa.dropbox.ui.viewlogs
{
    public class Webaccess
    {
        private static string URL = "http://localhost:63829/";

        public static TokenResponse GetToken(string username, string password)
        {
            var client = new OAuth2Client(new Uri(URL + "token"));
            return client.RequestResourceOwnerPasswordAsync(username, password).Result;
        }

        public static async Task<ObservableCollection<FileLog>> GetLogs(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
                client.GetAsync(URL + "api/log");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ObservableCollection<FileLog>>(json);
                }
                else return null;
            }
        }
    }
}
