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
            // TODO: Need to loop this functionality to loop the server and allow to accept new connections later on.

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress destination = IPAddress.Parse("192.168.1.255");

            string city = "";
            // Take in the city from the form here

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

            // Need to serialize the data in a real way, now just sending a string converted to an array of bytes 
            IPEndPoint recvAddr = new IPEndPoint(destination, listenPort);

            // Sending a serialized data here in form of a pointer. 
            // server.SendTo(packet.SerializeData(), recvAddr);
        }
    }
}