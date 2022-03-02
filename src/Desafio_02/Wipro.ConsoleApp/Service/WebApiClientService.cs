using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wipro.ConsoleApp.Model;

namespace Wipro.ConsoleApp.Service
{
    public class WebApiClientService
    {
        public MoedaDTO GetItemFila(string uri)
        {
            MoedaDTO moedaDTO = null;

            try
            {
                var httpClient = new HttpClient();

                HttpResponseMessage response = httpClient.GetAsync($"http://localhost:52630/{uri}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    moedaDTO = JsonConvert.DeserializeObject<MoedaDTO>(responseBody);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return moedaDTO;
        }
    }
}
