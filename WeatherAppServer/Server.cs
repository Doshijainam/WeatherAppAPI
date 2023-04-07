// See https://aka.ms/new-console-template for more information

using System.Net.Sockets;
using System.Net.Http.Headers;
using DataPacket;

namespace WeatherAppServer
{
    internal static class Server
    {
        private const string API_URL = "https://api.openweathermap.org/data/2.5/weather";
        private const string API_KEY = "&appid=b353092d473f54c232544798a31178f3";
        private const int listenPort = 22000;


class SimpleClient {

            bool run = true;
            while (run)
            {
                byte[] cityBuff = server.Receive(ref receiveEP);
                string city = Encoding.ASCII.GetString(cityBuff);

                string apiRequest = API_URL;
                string apiQuery = "?q=" + city + API_KEY;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiRequest);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(apiQuery).Result;

                ServerClientPacket packet = new();

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse;
                    apiResponse = response.Content.ReadAsStringAsync().Result;

                    packet = new(apiResponse);
                }
                else
                {
                    packet.SetErr(true);
                }

                server.Send(packet.SerializeData(), packet.SerializeData().Length);
            }
        }
    }
}