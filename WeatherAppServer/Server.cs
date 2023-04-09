using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Http.Headers;

using DataPacket;

namespace WeatherAppServer
{
    internal static class Server
    {
        private const string API_URL = "https://api.openweathermap.org/data/2.5/weather";
        private const string API_KEY = "&appid=b353092d473f54c232544798a31178f3";
        private const int listenPort = 8080;

        static void Main(string[] args)
        {

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 1234);
            server.Bind(localEP);

            EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

            bool run = true;
            while (run)
            {
                // Getting the city name here from the client
                byte[] cityBuff = new byte[1024];
                server.ReceiveFrom(cityBuff, ref remoteEP);
                string city = Encoding.ASCII.GetString(cityBuff);
                city = city.Split("\0")[0];

                // Sending the API rquest and initializing packet with the response.

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