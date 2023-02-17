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
            return this.sourceIP.ToString() + "," + this.destinationIP.ToString() + this.numberOfPackets + "," + this.dataLength;
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
            this.windDirection = windDirection;
            this.timezoneFromUTC = int.Parse(timezoneFromUTC);

            TimeSpan sunriseTS = TimeSpan.FromSeconds(int.Parse(sunrise) - this.timezoneFromUTC);
            this.sunrise = sunriseTS.ToString(@"hh\:mm");

            TimeSpan sunsetTS = TimeSpan.FromSeconds(int.Parse(sunset) - this.timezoneFromUTC);
            this.sunset = sunsetTS.ToString(@"hh\:mm");
        }

        public override string ToString()
        {
            return weatherType + "," + temperature + "," + temperatureFeelsLike + "," + pressure + "," + humidity + "," + visibility + "," + windSpeed + "," + windDirection + "," + sunrise + "," + sunset + "," + timezoneFromUTC;
        }
    }

    public class ServerClientPacket
    {
        Head head;
        WeatherData data;

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
    }
}