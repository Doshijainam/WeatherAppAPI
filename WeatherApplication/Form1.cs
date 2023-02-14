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
            string URL = "https://api.openweathermap.org/data/2.5/weather?q=" + textBox1.Text + "&appid=" + APIkey;
            string url = string.Format(URL);



        }
    }
}
