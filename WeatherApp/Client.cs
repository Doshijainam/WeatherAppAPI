using System.Net.Sockets;
using System.Net;
using System;
using System.Windows.Forms;
using System.Text;
using DataPacket;
using System.IO.Pipes;

namespace WeatherApp
{
    internal static class Program
    {

        public const int listenPort = 8080;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        //[STAThread]
        public static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            /* ApplicationConfiguration.Initialize();
             Application.Run(new ClientGUI());*/

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            //IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Any, 0);

            //using (NamedPipeClientStream clientStream = new NamedPipeClientStream(".", "mypipe", PipeDirection.In))
            //{
             //   clientStream.Connect();

                byte[] buffer = new byte[1024];
            //    clientStream.Read(buffer, 0, buffer.Length);

                // TODO : Get the user choice of the city here
                string uInput = "Toronto";

                // Just sending the CITY NAME HERE
                client.SendTo(Encoding.ASCII.GetBytes(uInput), remoteEP);

                // Now the receiving part 

                EndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                client.ReceiveFrom(buffer, ref ep);

                // Initializing the packet with the data we got back from the server.
                ServerClientPacket packet = new ServerClientPacket(buffer);

                // Print out the data to form (now just prints to console.
                Console.WriteLine(packet.ToPrintable());
                Console.WriteLine("Please hit any key to exit.");
                Console.ReadLine();
            //}
        }
    }
}