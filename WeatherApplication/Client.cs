using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherApplication
{
    internal static class Client
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress ip = IPAddress.Parse("192.168.1.255");
            IPEndPoint ep = new IPEndPoint(ip, 11000);

            // TODO : Get the user choice of the city here
            /*string uInput;
             * 
             * TODO : Then put it into the packet and send to the server once serialized
            s.SendTo(uInput, ep);*/

            // TODO : Receive the server response and decode it into the DataPacket class

            // TODO : Print stuff to the form
        }
    }
}
