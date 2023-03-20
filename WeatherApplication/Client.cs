using System;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

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
                    /*string uInput;
                     * 
                     * TODO : Then put it into the packet and send to the server once serialized
                    s.SendTo(uInput, ep);*/

                    // Receiving a response here as a byte array or a char* pointer
                    byte[] bytes = listener.Receive(ref groupEP);

                    // Process the data here to form a data packet.
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

            

            // TODO : Receive the server response and decode it into the DataPacket class

            // TODO : Print stuff to the form
        }
    }
}
