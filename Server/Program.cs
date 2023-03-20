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

using WeatherApplication;
namespace DataPackets;

class Server
{
    public static void Main(string[] args)
    {
        //Thread.Sleep(5000);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 1234);
        server.Bind(localEP);

        byte[] data = new byte[1024];
        EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
        int bytesReceived = server.ReceiveFrom(data, ref remoteEP);

        string message = Encoding.ASCII.GetString(data, 0, bytesReceived);
        //Console.WriteLine("Received message: " + message);
        

        using (WebClient web = new WebClient())
        {
            string APIkey = "b353092d473f54c232544798a31178f3";
            string URL = "https://api.openweathermap.org/data/2.5/weather?q=" +message +"&appid=" + APIkey;
            string url = string.Format(URL);
            string json = web.DownloadString(url);
           
            byte[] clientMsg = Encoding.UTF8.GetBytes(json);

            server.SendTo(clientMsg, remoteEP); //Problem

        }

        

    }

   

    
}

