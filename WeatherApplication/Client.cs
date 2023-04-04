using System;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.Text;
using DataPacket;

namespace WeatherApplication
{
    internal static class Client
    {

        public const int listenPort = 22000;

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

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
