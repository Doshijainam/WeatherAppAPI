using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

public struct weather
{
    // Weather condition description (like: Clouds, Broken Clouds, etc.)
    public string description { get; set; }
};
public struct main_info
{
    // Weather Data including:
    // //Temperature (temp)
    public double temp { get; set; }
    // Temperature Feels Like (feels_like)
    public double feels_like { get; set; }
    // Minimal Temperature for the Day (temp_min)
    public double temp_min { get; set; }
    // Maximum Temperature for the Day (temp_max)
    public double temp_max { get; set; }
    // Pressure (pressure)
    public double pressure { get; set; }
    // Humidity (humidity)
    public double humidity { get; set; }
};
public struct wind
{
    // Wind Speed in m/s
    public double speed { get; set; }
    // Wind Direction in degrees from 0 to 360
    public double deg { get; set; }
};
public struct sys
{
    // Sunrise time in seconds
    public int sunrise { get; set; }
    // Sunset time in seconds
    public int sunset { get; set; }
    // Time zone difference from UTC in seconds
    public int timezoneFromUTC { get; set; }
};

namespace DataPacket
{
    public class WeatherData
    {
        public const double Kelvin = 273;

        public weather Weather;
        public main_info Info;
        public wind Wind;
        public sys SystemData;

        // A container to store the human-readable wind direction
        public string windDirection { get; set; }
        // A container to store sunrise & sunset time in a HH:MM format
        public string sunrise { get; set; }
        public string sunset { get; set; }

        // A defauld constructor to set WeatherData class to safe state
        public WeatherData()
        {
            Weather.description = string.Empty;

            Info.temp = 0;
            Info.feels_like = 0;
            Info.temp_min = 0;
            Info.temp_max = 0;
            Info.pressure = 0;
            Info.humidity = 0;

            Wind.speed = 0;
            Wind.deg = 0;
            this.windDirection = "Undefined";

            SystemData.sunrise = 0;
            SystemData.sunset = 0;
            SystemData.timezoneFromUTC = 0;
            this.sunrise = "00:00";
            this.sunset = "00:00";
        }

        // A custom constructor for WeatherData to initialize it woth some contents
        public WeatherData(string description, double temp, double feels_like, double temp_min, double temp_max, double pressure, double humidity, double speed, double deg, int sunrise, int sunset, int timezoneFromUTC)
        {
            Weather.description = description;

            Info.temp = temp;
            Info.feels_like = feels_like;
            Info.temp_min = temp_min;
            Info.temp_max = temp_max;
            Info.pressure = pressure;
            Info.humidity = humidity;

            Wind.speed = speed;
            Wind.deg = deg;

            this.windDirection = DefineWindDirection();

            SystemData.sunrise = sunrise;
            SystemData.sunset = sunset;
            SystemData.timezoneFromUTC = timezoneFromUTC;

            TimeSpan sunriseTS = TimeSpan.FromSeconds(SystemData.sunrise + timezoneFromUTC);
            this.sunrise = sunriseTS.ToString(@"hh\:mm");

            TimeSpan sunsetTS = TimeSpan.FromSeconds(SystemData.sunset + timezoneFromUTC);
            this.sunset = sunsetTS.ToString(@"hh\:mm");
        }

        // TODO: Add a custom constructor here to parse the API Response string into a WeatherData class instance
        public WeatherData(string apiResponse)
        {
            /* Example of a string contained inside apiResponse:
             * {
                    "coord": {
                        "lon": 10.99,
                        "lat": 44.34
                    },
                    "weather": [
                    {
                        "id": 501,
                        "main": "Rain",
                        "description": "moderate rain",
                        "icon": "10d"
                    }
                    ],
                    "base": "stations",
                    "main": {
                        "temp": 298.48,
                        "feels_like": 298.74,
                        "temp_min": 297.56,
                        "temp_max": 300.05,
                        "pressure": 1015,
                        "humidity": 64,
                        "sea_level": 1015,
                        "grnd_level": 933
                    },
                    "visibility": 10000,
                    "wind": {
                        "speed": 0.62,
                        "deg": 349,
                        "gust": 1.18
                    },
                    "rain": {
                        "1h": 3.16
                    },
                    "clouds": {
                        "all": 100
                    },
                    "dt": 1661870592,
                    "sys": {
                        "type": 2,
                        "id": 2075663,
                        "country": "IT",
                        "sunrise": 1661834187,
                        "sunset": 1661882248
                    },
                    "timezone": 7200,
                    "id": 3163858,
                    "name": "Zocca",
                    "cod": 200
                }
            */

            string[] delimeters = { "{\"", "\":{\"", "\":", ",\"", "},\"", "\":[{\"", "\":\"", "\",\"" };

            string[] words = apiResponse.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);

            string description = "";
            double temp = 0;
            double feels_like = 0;
            double temp_min = 0;
            double temp_max = 0;
            double pressure = 0;
            double humidity = 0;
            double speed = 0;
            double deg = 0;
            int sunrise = 0;
            int sunset = 0;
            int timezoneFromUTC = 0;

            for (int i = 0; i < words.Length; i++)
            {
                switch (words[i])
                {
                    case "description": description = words[i + 1]; break;
                    case "temp": temp = double.Parse(words[i + 1]) - Kelvin; break;
                    case "feels_like": feels_like = double.Parse(words[i + 1]) - Kelvin; break;
                    case "temp_min": temp_min = double.Parse(words[i + 1]) - Kelvin; break;
                    case "temp_max": temp_max = double.Parse(words[i + 1]) - Kelvin; break;
                    case "pressure": pressure = double.Parse(words[i + 1]); break;
                    case "humidity": humidity = double.Parse(words[i + 1]); break;
                    case "speed": speed = double.Parse(words[i + 1]); break;
                    case "deg": deg = double.Parse(words[i + 1]); break;
                    case "sunrise": sunrise = int.Parse(words[i + 1]); break;
                    case "sunset": sunset = int.Parse(words[i + 1]); break;
                    case "timezone": timezoneFromUTC = int.Parse(words[i + 1]); break;
                    default: break;
                }
            }

            Weather.description = description;

            Info.temp = temp;
            Info.feels_like = feels_like;
            Info.temp_min = temp_min;
            Info.temp_max = temp_max;
            Info.pressure = pressure;
            Info.humidity = humidity;

            Wind.speed = speed;
            Wind.deg = deg;

            this.windDirection = DefineWindDirection();

            SystemData.sunrise = sunrise;
            SystemData.sunset = sunset;
            SystemData.timezoneFromUTC = timezoneFromUTC;

            TimeSpan sunriseTS = TimeSpan.FromSeconds(SystemData.sunrise + timezoneFromUTC);
            this.sunrise = sunriseTS.ToString(@"hh\:mm");

            TimeSpan sunsetTS = TimeSpan.FromSeconds(SystemData.sunset + timezoneFromUTC);
            this.sunset = sunsetTS.ToString(@"hh\:mm");
        }

        /* unsafe public WeatherData(char* buff, int headLength)
        {
            int len = sizeof(buff);
            int stringLen = len - headLength - Marshal.SizeOf(main_info) - Marshal.SizeOf(wind) - Marshal.SizeOf(sys);
            fixed (char* desc = &weather)
            {
                Buffer.MemoryCopy(buff + headLength, desc, Marshal.SizeOf(stringLen), Marshal.SizeOf(stringLen));
            }
            fixed (char* main = &main_info)
            {
                Buffer.MemoryCopy(buff + headLength + stringLen, main, Marshal.SizeOf(main_info), Marshal.SizeOf(main_info));
            }
            fixed (char* w = &wind)
            {
                Buffer.MemoryCopy(buff + headLength + stringLen + Marshal.SizeOf(main_info), w, Marshal.SizeOf(wind), Marshal.SizeOf(wind));
            }
            fixed (char* s = &sys)
            {
                Buffer.MemoryCopy(buff + headLength + stringLen + Marshal.SizeOf(main_info) + Marshal.SizeOf(sys), s, Marshal.SizeOf(sys), Marshal.SizeOf(sys));
            }

            this.windDirection = DefineWindDirection();

            TimeSpan sunriseTS = TimeSpan.FromSeconds(SystemData.sunrise - timezoneFromUTC);
            this.sunrise = sunriseTS.ToString(@"hh\:mm");

            TimeSpan sunsetTS = TimeSpan.FromSeconds(SystemData.sunset - timezoneFromUTC);
            this.sunset = sunsetTS.ToString(@"hh\:mm");
        } */

        public WeatherData(byte[] buffer, int headSize)
        {
            string[] bufferParsed = (Encoding.ASCII.GetString(buffer)).Split(",");

            Weather.description = bufferParsed[headSize];

            Info.temp = Double.Parse(bufferParsed[++headSize]);
            Info.feels_like = Double.Parse(bufferParsed[++headSize]);
            Info.temp_min = Double.Parse(bufferParsed[++headSize]);
            Info.temp_max = Double.Parse(bufferParsed[++headSize]);
            Info.pressure = Double.Parse(bufferParsed[++headSize]);
            Info.humidity = Double.Parse(bufferParsed[++headSize]);

            Wind.speed = Double.Parse(bufferParsed[++headSize]);
            Wind.deg = Double.Parse(bufferParsed[++headSize]);

            this.windDirection = DefineWindDirection();

            this.sunrise = bufferParsed[++headSize];
            this.sunset = bufferParsed[++headSize];
        }

        public string DefineWindDirection()
        {
            string windDir;
            if ((Wind.deg > 348.75 && Wind.deg <= 360) || (Wind.deg >= 0 && Wind.deg <= 11.25))
                windDir = "North";
            else if (Wind.deg > 11.25 && Wind.deg <= 33.75)
                windDir = "North-North-East";
            else if (Wind.deg > 33.75 && Wind.deg <= 56.25)
                windDir = "North-East";
            else if (Wind.deg > 56.25 && Wind.deg <= 78.75)
                windDir = "East-North-East";
            else if (Wind.deg > 78.75 && Wind.deg <= 101.25)
                windDir = "East";
            else if (Wind.deg > 101.25 && Wind.deg <= 123.75)
                windDir = "East-South-East";
            else if (Wind.deg > 123.75 && Wind.deg <= 146.25)
                windDir = "South-East";
            else if (Wind.deg > 146.25 && Wind.deg <= 168.75)
                windDir = "South-South-East";
            else if (Wind.deg > 168.75 && Wind.deg <= 191.25)
                windDir = "South";
            else if (Wind.deg > 191.25 && Wind.deg <= 213.75)
                windDir = "South-South-West";
            else if (Wind.deg > 213.75 && Wind.deg <= 236.25)
                windDir = "South-West";
            else if (Wind.deg > 236.25 && Wind.deg <= 258.75)
                windDir = "West-South-West";
            else if (Wind.deg > 258.75 && Wind.deg <= 281.25)
                windDir = "West";
            else if (Wind.deg > 281.25 && Wind.deg <= 303.75)
                windDir = "West-North-West";
            else if (Wind.deg > 303.75 && Wind.deg <= 326.25)
                windDir = "North-West";
            else if (Wind.deg > 326.25 && Wind.deg <= 348.75)
                windDir = "North-North-West";
            else
                windDir = "Undefined";

            return windDir;
        }

        public override string ToString()
        {
            return Weather.description + "," + Info.temp + "," + Info.feels_like + "," + Info.temp_min + "," + Info.temp_max + "," + Info.pressure + "," + Info.humidity + "," + Wind.speed + "," + Wind.deg + "," + this.sunrise + "," + this.sunset;
        }

        public string ToPrintable()
        {
            return "Weather Data:\n" + "Weather Type: " + Weather.description + "\nTemperature: " + Info.temp + "°C\nFeels Like: " + Info.feels_like + "°C\nMinimum Temperature: " + Info.temp_min + "°C\nMaximum Temperature: " + Info.temp_max + "°C\nPressure: " + Info.pressure + " hPa\nHumidity: " + Info.humidity + "%\nWind Speed: " + Wind.speed + " m/s\nWind Direction: " + this.windDirection + "\nSunrise Time: " + this.sunrise + "\nSunset Time: " + this.sunset + "\n";
        }
    }
}