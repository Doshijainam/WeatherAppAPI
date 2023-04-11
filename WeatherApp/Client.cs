using System.Net.Sockets;
using System.Net;
using System.IO;
using System;
using System.Windows.Forms;
using System.Text;
using DataPacket;
using System.IO.Pipes;

namespace WeatherApp
{
    public static class Program
    {

        public const int listenPort = 8080;
        public static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ClientGUI form = new ClientGUI(); // Create an instance of your Windows Form

            // Attach an event handler to the form's button click event
            form.button1.Click += (sender, e) =>
            {

                // Get the user input from the form's textbox
                string uInput = form.textBox12.Text;
                //string uInput = "Toronto";
                // Send the user input to the server
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
                client.SendTo(Encoding.ASCII.GetBytes(uInput), remoteEP);

                // Receive the response from the server
                byte[] buffer = new byte[1024];
                EndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                client.ReceiveFrom(buffer, ref ep);
                ServerClientPacket packet = new ServerClientPacket(buffer);


                string logMessage = $"{DateTime.Now}: Received packet: {packet.ToPrintable()}";

                using (StreamWriter writer = File.AppendText("C:/Users/JIGNESH PATEL/WeatherAppAPI/WeatherAppAPI/log.txt"))
                {
                    writer.WriteLine(logMessage);
                }

                form.textBox1.Text = packet.data.Weather.description;
                form.textBox2.Text = Math.Round(packet.data.Info.temp).ToString() + " °C";
                form.textBox3.Text = Math.Round(packet.data.Info.feels_like).ToString() + " °C";
                form.textBox4.Text = packet.data.Info.temp_min.ToString() + " °C";
                form.textBox5.Text = packet.data.Info.temp_max.ToString() + " °C";
                form.textBox6.Text = packet.data.Info.pressure.ToString();
                form.textBox7.Text = packet.data.Info.humidity.ToString();
                form.textBox8.Text = packet.data.Wind.speed.ToString() + " Km/h";

                form.textBox9.Text = packet.data.windDirection.ToString();
                form.textBox10.Text = packet.data.sunrise.ToString();
                form.textBox11.Text = packet.data.sunset.ToString();








                // Display the response on the form
                //form.textBox2.Text = packet.ToPrintable();
            };

            // Run the form
            Application.Run(form);
        }


    }

}
