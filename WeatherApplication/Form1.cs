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
namespace WeatherApplication
{
    public partial class Form1 : Form
    {
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
            getWeather(); 


        }
        void getWeather()
        {
            using(WebClient web = new WebClient())
            {

                string URL = "https://api.openweathermap.org/data/2.5/weather?q=" + textBox1.Text + "&appid=" + APIkey;
                string url = string.Format(URL);
                var json  = web.DownloadString(url);
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);
                labConditions.Text = Info.weather[0].main;
                labSunset.Text = Info.sys.sunset.ToString();
                labSun.Text = Info.sys.sunrise.ToString();
                labelWindSpeed.Text = Info.wind.speed.ToString();
                labPressure.Text = Info.main.pressure.ToString();
                labelWeather.Text =  (-273 + Info.main.temp).ToString(); 


                labelDetails.Text = Info.weather[0].description; 

                
              

            }
        }
    }
}
