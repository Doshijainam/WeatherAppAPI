using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Formatters.Binary;


namespace WeatherApplication
{
    public partial class Form1 : Form
    {


        private NamedPipeServerStream serverStream;
        public static WeatherInfo.root ConvertToDataPacket(byte[] buffer)
        {
            string json = Encoding.UTF8.GetString(buffer);
            WeatherInfo.root info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);
            return info;
        }




        public Form1()
        {
            InitializeComponent();
        }
        string APIkey = "b353092d473f54c232544798a31178f3";

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (true)
            {
                sendCityNameToClient();
                getResponse();
                textBox1.Text = "";

                // Prompt user for another input or exit
                var dialogResult = MessageBox.Show("Enter another city name?", "Prompt", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    break; // Exit the loop and method
                }
            }
        }


        public void sendCityNameToClient()
        {
            byte[] buffer = Encoding.ASCII.GetBytes(textBox1.Text);
            serverStream.Write(buffer, 0, buffer.Length);
        }
        void getWeather()
        {
            using(WebClient web = new WebClient())
            {
                pipe.WaitForConnection();
                using (var reader = new StreamReader(pipe))
                {
                    string clientDataPacketResponse;
                    while ((clientDataPacketResponse = reader.ReadLine()) != null)
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(clientDataPacketResponse);
                        WeatherInfo.root Info = ConvertToDataPacket(buffer);

                        labConditions.Text = Info.weather[0].main;
                        labSunset.Text = Info.sys.sunset.ToString();
                        labSun.Text = Info.sys.sunrise.ToString();
                        labelWindSpeed.Text = Info.wind.speed.ToString();
                        labPressure.Text = Info.main.pressure.ToString();
                        labelWeather.Text = (-273 + Info.main.temp).ToString();
                        labelDetails.Text = Info.weather[0].description;
                    }
                }
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (sender.Equals(Exit))
            {
                Application.Exit();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}



//using System;
//using System.IO;
//using System.IO.MemoryMappedFiles;
//using System.IO.Pipes;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Newtonsoft.Json;
//using System.Net;
//using Newtonsoft.Json.Linq;
//using System.Runtime.Serialization.Formatters.Binary;


//namespace WeatherApplication
//{
//    public partial class Form1 : Form
//    {
//        private NamedPipeServerStream serverStream;

//        public Form1()
//        {
//            InitializeComponent();
//            serverStream = new NamedPipeServerStream("mypipe");
//            serverStream.WaitForConnection();
//        }

//        public void sendCityNameToClient()
//        {
//            byte[] buffer = Encoding.ASCII.GetBytes(textBox1.Text);
//            serverStream.Write(buffer, 0, buffer.Length);
//        }

//        public void getResponse()
//        {
//            using (var pipe = new NamedPipeClientStream(".", "my-pipe", PipeDirection.In))
//            {
//                pipe.Connect();
//                using (var reader = new StreamReader(pipe))
//                {
//                    string clientDataPacketResponse = reader.ReadLine();
//                    byte[] buffer = Encoding.UTF8.GetBytes(clientDataPacketResponse);
//                    WeatherInfo.root Info = ConvertToDataPacket(buffer);

//                    labConditions.Text = Info.weather[0].main;
//                    labSunset.Text = Info.sys.sunset.ToString();
//                    labSun.Text = Info.sys.sunrise.ToString();
//                    labelWindSpeed.Text = Info.wind.speed.ToString();
//                    labPressure.Text = Info.main.pressure.ToString();
//                    labelWeather.Text = (-273 + Info.main.temp).ToString();
//                    labelDetails.Text = Info.weather[0].description;
//                }
//            }
//        }

//        private NamedPipeClientStream clientStream;
//        public static WeatherInfo.root ConvertToDataPacket(byte[] buffer)
//        {
//            BinaryFormatter binaryFormatter = new BinaryFormatter();
//            using (MemoryStream ms = new MemoryStream(buffer))
//            {
//                WeatherInfo.root info = (WeatherInfo.root)binaryFormatter.Deserialize(ms);
//                return info;
//            }
//        }

//        private void textBox1_TextChanged(object sender, EventArgs e)
//        {
//        }

//        private void Exit_Click(object sender, EventArgs e)
//        {
//            if (sender.Equals(Exit))
//            {
//                Application.Exit();
//            }
//        }

//        private void label2_Click(object sender, EventArgs e)
//        {
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            sendCityNameToClient();
//            getResponse();
//            textBox1.Text = "";
//        }
//    }
//}
