using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

namespace WeatherApplication
{
    public class WeatherInfo
    {
        

        public class coord
        {
            public double lon { get; set; }
           public  double lat { get; set; }

        }
        public class weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }

            public string icon { get; set; }

        }

        public class main
        {

            public double temp { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }

            public double feelsLike{ get; set; }

            public double tempMin { get; set; }

            public double tempMax { get; set; }

        }

        public class wind
        {
            public double speed { get; set; }

            public int degree { get; set; }
        }

        public class clouds
        {
            public int all { get; set; }
        }

        public class sys
        {
            public long sunrise { get; set; }
            public long sunset { get; set; }

            public string country { get; set; }

            public int type { get; set; }

            public int id { get; set; }
        }

        public class root
        {
            public coord coord { get; set; }

            public List<weather> weather { get; set; }

            public string @base{get;set;}

            public main main { get; set; }

            public int visibility { get; set; }

            public wind wind { get; set; }

            public clouds clouds { get; set; }

            public long dt { get; set; }

            public sys sys { get; set; }

            public int timezone { get; set; }

            public int id { get; set; }

            public string name { get; set; }

            public int cod { get; set; }


        }
    }

 
}
