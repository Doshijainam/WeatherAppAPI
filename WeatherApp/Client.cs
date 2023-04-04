using System.Net.Sockets;
using System.Net;
using System;
using System.Windows.Forms;
using System.Text;
using DataPacket;

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

            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            try
            {
                while (true)
                {
                    // TODO : Get the user choice of the city here
                    string uInput = "Toronto";


                    // server.SendTo(Encoding.ASCII.GetBytes(uInput), groupEP);

                    ServerClientPacket packet = new ServerClientPacket(listener.Receive(ref groupEP));

                    // Print out the data to form.
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }
    }
}