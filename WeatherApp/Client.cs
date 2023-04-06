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

        public const int listenPort = 22000;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new ClientGUI());



            // TODO : Get the user choice of the city here
            string uInput = "Toronto";

            // Just sending the CITY NAME HERE
            using (UdpClient sender = new UdpClient(listenPort))
                sender.Send(Encoding.ASCII.GetBytes(uInput), listenPort);

            // Now the receiving part 

            UdpClient receiver = new UdpClient(listenPort);
            IPEndPoint senderEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), listenPort);

            // Initializing the packet with the data we got back from the server.
            ServerClientPacket packet = new ServerClientPacket(receiver.Receive(ref senderEP));

            // Print out the data to form (now just prints to console.
            Console.WriteLine(packet.ToPrintable());

        }
    }
}