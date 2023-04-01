using System.Text;
using System.Net;
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

        static void Main(string[] args)
        {
            // Reusing Jainam's Server Settings from here:

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint localEP = new IPEndPoint(IPAddress.Any, listenPort);
            server.Bind(localEP);

            EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

            // To here <-

            bool run = true;
            while (run)
            {
                byte[] cityBuff = new byte[1024];
                server.ReceiveFrom(cityBuff, ref remoteEP);
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

                server.SendTo(packet.SerializeData(), remoteEP);
            }
        }
    }
}