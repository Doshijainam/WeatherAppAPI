using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
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


namespace WeatherApplication
{
    public partial class Form1 : Form
    {
        private NamedPipeServerStream serverStream;

        public Form1()
        {
            InitializeComponent();
            serverStream = new NamedPipeServerStream("mypipe");
            serverStream.WaitForConnection();
        }


        public void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void button1_Click(object sender, EventArgs e)
        {
            sendCityNameToClient();
            
            getResponse();
            textBox1.Text = "";

        }

        public void sendCityNameToClient()
        {
            byte[] buffer = Encoding.ASCII.GetBytes(textBox1.Text);
            serverStream.Write(buffer, 0, buffer.Length);
        }


        public void getResponse()
        {
            using (var pipe = new NamedPipeServerStream("my-pipe"))
            {
                pipe.WaitForConnection();
                using (var reader = new StreamReader(pipe))
                {
                    
                    string clientDataPacketResponse = reader.ReadLine();
                    WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(clientDataPacketResponse);

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if(sender.Equals(Exit))
            {
                Application.Exit();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
