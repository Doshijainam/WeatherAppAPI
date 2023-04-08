using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApplication
{
    [Serializable]
    public class WeatherInfo
    {

        [Serializable]
        public class coord
        {
            public double lon { get; set; }
           public  double lat { get; set; }

        }
        [Serializable]
        public class weather
        {
            public string main { get; set; }
            public string description { get; set; }

            public string icon { get; set; }

        }
        [Serializable]
        public class main
        {

            public double temp { get; set; }
            public double pressure { get; set; }
            public double humidity { get; set; }

        }
        [Serializable]
        public class wind
        {
            public double speed { get; set; }
        }
        [Serializable]
        public class clouds
        {
            public int all { get; set; }
        }
        [Serializable]
        public class sys
        {
            public long sunrise { get; set; }
            public long sunset { get; set; }
        }
        [Serializable]
        public class root
        {
            public coord coord { get; set; }

            public List<weather> weather { get; set; }

            public main main { get; set; }

            public wind wind { get; set; }

            public sys sys { get; set; }


        }
    }
}
