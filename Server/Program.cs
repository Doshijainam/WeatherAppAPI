//using System;
//using System.Net.Sockets;
//using System.Net;
//using System.Text;
//using System.Threading;

//using System.IO.MemoryMappedFiles;
//using System.IO.Pipes;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using System.Runtime.Serialization.Formatters.Binary;

//using WeatherApplication;
//namespace DataPackets;

//public class Server
//{
//    public static byte[] ConvertToByteArray(WeatherInfo.root info)
//    {
//        BinaryFormatter bf = new BinaryFormatter();
//        using (MemoryStream ms = new MemoryStream())
//        {
//            bf.Serialize(ms, info); 
//            return ms.ToArray();    

//        }
//    }
//    public static void Main(string[] args)
//    {
//        //Thread.Sleep(5000);
//        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//        IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 1234);
//        server.Bind(localEP);

//        byte[] data = new byte[1024];
//        EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);



//            int bytesReceived = 0;
//            bytesReceived = server.ReceiveFrom(data, ref remoteEP);


//            string message = Encoding.ASCII.GetString(data, 0, bytesReceived);
//            //Console.WriteLine("Received message: " + message);


//            using (WebClient web = new WebClient())
//            {
//                string APIkey = "b353092d473f54c232544798a31178f3";
//                string URL = "https://api.openweathermap.org/data/2.5/weather?q=" + message + "&appid=" + APIkey;
//                string url = string.Format(URL);
//                string json = web.DownloadString(url);
//            WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

//            byte[] clientMsg = ConvertToByteArray(Info);

//            //byte[] clientMsg = Encoding.UTF8.GetBytes(json);

//            server.SendTo(clientMsg, remoteEP); 


//        }
//        }




//    }





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
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace WeatherApplication
{
    public class Server
    {
       
        // Convert WeatherInfo.root object to byte array using BinaryFormatter
        public static byte[] ConvertToByteArray(WeatherInfo.root info)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, info);
                return ms.ToArray();
            }
        }

        public static void Main(string[] args)
        {
            // Create a UDP server socket and bind it to a local endpoint
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 1234);
            server.Bind(localEP);

            // Keep the server open for new clients until an exit code is received
            bool keepRunning = true;
            while (keepRunning)
            {
                byte[] data = new byte[10000];
                EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

                // Receive data from a client
                int bytesReceived = 0;
                bytesReceived = server.ReceiveFrom(data, ref remoteEP);

                // Convert received byte array to a string message
                string message = Encoding.ASCII.GetString(data, 0, bytesReceived);

                // Check for exit code from client
                if (message == "exit")
                {
                    keepRunning = false;
                    continue;
                }

                // Use OpenWeatherMap API to get weather information for the requested city
                using (WebClient web = new WebClient())
                {
                    string APIkey = "b353092d473f54c232544798a31178f3";
                    string URL = "https://api.openweathermap.org/data/2.5/weather?q=" + message + "&appid=" + APIkey;
                    string url = string.Format(URL);
                    string json = web.DownloadString(url);

                    // Deserialize JSON string to WeatherInfo.root object
                    WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                    // Convert WeatherInfo.root object to byte array using BinaryFormatter
                    byte[] clientMsg = ConvertToByteArray(Info);
                    
                    // Send the weather information byte array back to the client
                    server.SendTo(clientMsg, remoteEP);
                }
            }

            // Close the server socket
            server.Close();
        }
    }
}

