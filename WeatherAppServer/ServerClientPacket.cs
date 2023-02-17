using System;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace DataPackets
{
    public class Head
    {
        public IPAddress sourceIP;
        public IPAddress destinationIP;
        public int numberOfPackets;
        public int dataLength;

        public Head()
        {
            sourceIP = IPAddress.Parse("127.0.0.1");
            destinationIP = IPAddress.Parse("255.255.255.255");

            numberOfPackets = 0;
            dataLength = 0;
        }

        public Head(string sourceIP, string destinationIP, int numberOfPackets, int dataLength)
        {
            this.sourceIP = IPAddress.Parse(sourceIP);
            this.destinationIP = IPAddress.Parse(destinationIP);
            this.numberOfPackets = numberOfPackets;
            this.dataLength = dataLength;
        }

        public override string ToString()
        {
            return this.sourceIP.ToString() + "," + this.destinationIP.ToString() + "," + this.numberOfPackets + "," + this.dataLength;
        }

        public string ToPrintable()
        {
            return "Packet:\n" + "Source IP Address: " + this.sourceIP.ToString() + "\nDestination IP Address: " + this.destinationIP.ToString() + "\nNumber of Packets: " + this.numberOfPackets + "\nLength of the Payload: " + this.dataLength + "\n";
        }
    }

    public class WeatherData
    {
        public const double Kelvin = 273;

        public string weatherType;

        public double temperature;
        public double temperatureFeelsLike;

        public double pressure;
        public double humidity;

        public int visibility;

        public double windSpeed;
        public string windDirection;

        public string sunrise;
        public string sunset;
        public int timezoneFromUTC;

        public WeatherData()
        {
            this.weatherType = string.Empty;
            this.temperature = 0.0;
            this.temperatureFeelsLike = 0.0;
            this.pressure = 0.0;
            this.humidity = 0.0;
            this.visibility = 0;
            this.windSpeed = 0.0;
            this.windDirection = string.Empty;
            this.sunrise = string.Empty;
            this.sunset = string.Empty;
            this.timezoneFromUTC = 0;
        }

        public WeatherData(string weatherType, double temperature, double temperatureFeelsLike, double pressure, double humidity, int visibility, double windSpeed, string windDirection, string sunrise, string sunset, int timezoneFromUTC)
        {
            this.weatherType = weatherType;
            this.temperature = temperature;
            this.temperatureFeelsLike = temperatureFeelsLike;
            this.pressure = pressure;
            this.humidity = humidity;
            this.visibility = visibility;
            this.windSpeed = windSpeed;
            this.windDirection = windDirection;
            this.sunrise = sunrise;
            this.sunset = sunset;
            this.timezoneFromUTC = timezoneFromUTC;
        }
        public WeatherData(string weatherType, string temperature, string temperatureFeelsLike, string pressure, string humidity, string visibility, string windSpeed, string windDirection, string sunrise, string sunset, string timezoneFromUTC)
        {
            this.weatherType = weatherType;
            this.temperature = Double.Parse(temperature) - Kelvin;
            this.temperatureFeelsLike = Double.Parse(temperatureFeelsLike) - Kelvin;
            this.pressure = Double.Parse(pressure);
            this.humidity = Double.Parse(humidity);
            this.visibility = int.Parse(visibility);
            this.windSpeed = Double.Parse(windSpeed);

            // Computing the Cardinal Direction by the degrees given
            double windDir = double.Parse(windDirection);
            if ((windDir > 348.75 && windDir <= 360) || (windDir >= 0 && windDir <= 11.25))
                this.windDirection = "North";
            else if (windDir > 11.25 && windDir <= 33.75)
                this.windDirection = "North-North-East";
            else if (windDir > 33.75 && windDir <= 56.25)
                this.windDirection = "North-East";
            else if (windDir > 56.25 && windDir <= 78.75)
                this.windDirection = "East-North-East";
            else if (windDir > 78.75 && windDir <= 101.25)
                this.windDirection = "East";
            else if (windDir > 101.25 && windDir <= 123.75)
                this.windDirection = "East-South-East";
            else if (windDir > 123.75 && windDir <= 146.25)
                this.windDirection = "South-East";
            else if (windDir > 146.25 && windDir <= 168.75)
                this.windDirection = "South-South-East";
            else if (windDir > 168.75 && windDir <= 191.25)
                this.windDirection = "South";
            else if (windDir > 191.25 && windDir <= 213.75)
                this.windDirection = "South-South-West";
            else if (windDir > 213.75 && windDir <= 236.25)
                this.windDirection = "South-West";
            else if (windDir > 236.25 && windDir <= 258.75)
                this.windDirection = "West-South-West";
            else if (windDir > 258.75 && windDir <= 281.25)
                this.windDirection = "West";
            else if (windDir > 281.25 && windDir <= 303.75)
                this.windDirection = "West-North-West";
            else if (windDir > 303.75 && windDir <= 326.25)
                this.windDirection = "North-West";
            else if (windDir > 326.25 && windDir <= 348.75)
                this.windDirection = "North-North-West";
            else
                this.windDirection = "Undefined";

            this.timezoneFromUTC = int.Parse(timezoneFromUTC);

            TimeSpan sunriseTS = TimeSpan.FromSeconds(int.Parse(sunrise) - this.timezoneFromUTC);
            this.sunrise = sunriseTS.ToString(@"hh\:mm");

            TimeSpan sunsetTS = TimeSpan.FromSeconds(int.Parse(sunset) - this.timezoneFromUTC);
            this.sunset = sunsetTS.ToString(@"hh\:mm");
        }

        public override string ToString()
        {
            return this.weatherType + "," + this.temperature + "," + this.temperatureFeelsLike + "," + this.pressure + "," + this.humidity + "," + this.visibility + "," + this.windSpeed + "," + this.windDirection + "," + this.sunrise + "," + this.sunset + "," + this.timezoneFromUTC;
        }

        public string ToPrintable()
        {
            return "Weather Data:\n" + "Weather Type: " + this.weatherType + "\nTemperature: " + this.temperature + "°C\nFeels Like: " + this.temperatureFeelsLike + "°C\nPressure: " + this.pressure + " hPa\nHumidity: " + this.humidity + "%\nVisibility: " + this.visibility + "%\nWind Speed: " + this.windSpeed + " km/h\nWind Direction: " + this.windDirection + "\nSunrise Time: " + this.sunrise + "\nSunset Time: " + this.sunset + "\n";
        }
    }

    public class ServerClientPacket
    {
        public Head head;
        public WeatherData data;

        public ServerClientPacket()
        {
            head = new Head();
            data = new WeatherData();
        }
        public ServerClientPacket(Head head, WeatherData data)
        {
            this.head = head;
            this.data = data;
        }
        public ServerClientPacket(string sourceIP, string destinationIP, int numberOfPackets, int dataLength, string weatherType, string temperature, string temperatureFeelsLike, string pressure, string humidity, string visibility, string windSpeed, string windDirection, string sunrise, string sunset, string timezoneFromUTC)
        {
            this.head = new Head(sourceIP, destinationIP, numberOfPackets, dataLength);
            this.data = new WeatherData(weatherType, temperature, temperatureFeelsLike, pressure, humidity, visibility, windSpeed, windDirection, sunrise, sunset, timezoneFromUTC);
        }
        public ServerClientPacket(string stingPkt)
        {
            string[] splitStringPkt = stingPkt.Split(',');
            this.head = new Head(splitStringPkt[0], splitStringPkt[1], int.Parse(splitStringPkt[2]), int.Parse(splitStringPkt[3]));
            this.data = new WeatherData(splitStringPkt[4], splitStringPkt[5], splitStringPkt[6], splitStringPkt[7], splitStringPkt[8], splitStringPkt[9], splitStringPkt[10], splitStringPkt[11], splitStringPkt[12], splitStringPkt[13], splitStringPkt[14]);
        }

        public string SerializeData()
        {
            return this.head.ToString() + "," + this.data.ToString();
        }

        public string ToPrintable()
        {
            return "Printing the whole Server -> Client Packet: \n" + this.head.ToPrintable() + this.data.ToPrintable();
        }
    }
}