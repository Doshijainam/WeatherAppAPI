using DataPackets;

namespace WeatherAppTests
{
    [TestClass]
    public class DataPacketsTests
    {
        // Testing the Default Constructor for Server -> Client Data Packet
        [TestMethod]
        public void ServerClientPacketDefConstrTestHead()
        {
            Head head = new Head();

            ServerClientPacket scp = new ServerClientPacket();

            Assert.AreEqual(head.ToString(), scp.head.ToString());
        }
        [TestMethod]
        public void ServerClientPacketDefConstrTestData()
        {
            WeatherData data = new WeatherData();

            ServerClientPacket scp = new ServerClientPacket();

            Assert.AreEqual(data.ToString(), scp.data.ToString());
        }
        // Testing the Custom Constructor #1 for Server -> Client Data Packet with empty fields
        [TestMethod]
        public void ServerClientPacketCustConstr1TestHeadE()
        {
            Head head = new Head();
            WeatherData data = new WeatherData();

            ServerClientPacket scp = new ServerClientPacket(head, data);

            Assert.AreEqual(head.ToString(), scp.head.ToString());
        }
        [TestMethod]
        public void ServerClientPacketCustConstr1TestDataE()
        {
            Head head = new Head();
            WeatherData data = new WeatherData();

            ServerClientPacket scp = new ServerClientPacket(head, data);

            Assert.AreEqual(data.ToString(), scp.data.ToString());
        }

        // Testing the Custom Constructor #1 for Server -> Client Data Packet with non-empty fields
        [TestMethod]
        public void ServerClientPacketCustConstr1TestHeadF()
        {
            Head head = new Head("10.0.0.1", "10.0.0.27", 15, 155);
            WeatherData data = new WeatherData("Cloudy", 12.5, 11.25, 75, 78, 100, 22, "155", "1594353335", "1594412149", 3600);

            ServerClientPacket scp = new ServerClientPacket(head, data);

            Assert.AreEqual(head.ToString(), scp.head.ToString());
        }
        [TestMethod]
        public void ServerClientPacketCustConstr1TestDataF()
        {
            Head head = new Head("10.0.0.1", "10.0.0.27", 15, 155);
            WeatherData data = new WeatherData("Cloudy", 12.5, 11.25, 75, 78, 100, 22, "155", "1594353335", "1594412149", 3600);

            ServerClientPacket scp = new ServerClientPacket(head, data);

            Assert.AreEqual(data.ToString(), scp.data.ToString());
        }

        // Testing the Custom Constructor #2 for Server -> Client Data Packet with non-empty fields
        [TestMethod]
        public void ServerClientPacketCustConstr2TestHeadF()
        {
            Head head = new Head("10.0.0.1", "10.0.0.27", 15, 155);
            WeatherData data = new WeatherData("Cloudy", 12.5, 11.25, 75, 78, 100, 22, "155", "1594353335", "1594412149", 3600);

            ServerClientPacket scp = new ServerClientPacket("10.0.0.1", "10.0.0.27", 15, 155, "Cloudy", "12.5", "11.25", "75", "78", "100", "22", "155", "1594353335", "1594412149", "3600");

            Assert.AreEqual(head.ToString(), scp.head.ToString());
        }
        [TestMethod]
        public void ServerClientPacketCustConstr2TestDataF()
        {
            Head head = new Head("10.0.0.1", "10.0.0.27", 15, 155);
            WeatherData data = new WeatherData("Cloudy", 12.5, 11.25, 75, 78, 100, 22, "155", "1594353335", "1594412149", 3600);

            ServerClientPacket scp = new ServerClientPacket("10.0.0.1", "10.0.0.27", 15, 155, "Cloudy", "285.5", "284.25", "75", "78", "100", "22", "155", "1594353335", "1594412149", "3600");

            Assert.AreEqual("Cloudy,12.5,11.25,75,78,100,22,South-South-East,02:55,19:15,3600", scp.data.ToString());
        }

        // Testing the Custom Constructor #3 for Server -> Client Data Packet with non-empty fields
        [TestMethod]
        public void ServerClientPacketCustConstr3TestHeadF()
        {
            Head head = new Head("10.0.0.1", "10.0.0.27", 15, 155);
            WeatherData data = new WeatherData("Cloudy", 12.5, 11.25, 75, 78, 100, 22, "155", "1594353335", "1594412149", 3600);

            ServerClientPacket scp = new ServerClientPacket("10.0.0.1,10.0.0.27,15,155,Cloudy,285.5,284.25,75,78,100,22,155,1594353335,1594412149,3600");

            Assert.AreEqual(head.ToString(), scp.head.ToString());
        }
        [TestMethod]
        public void ServerClientPacketCustConstr3TestDataF()
        {
            Head head = new Head("10.0.0.1", "10.0.0.27", 15, 155);
            WeatherData data = new WeatherData("Cloudy", 12.5, 11.25, 75, 78, 100, 22, "155", "1594353335", "1594412149", 3600);

            ServerClientPacket scp = new ServerClientPacket("10.0.0.1,10.0.0.27,15,155,Cloudy,285.5,284.25,75,78,100,22,155,1594353335,1594412149,3600");

            Assert.AreEqual("Cloudy,12.5,11.25,75,78,100,22,South-South-East,02:55,19:15,3600", scp.data.ToString());
        }

        [TestMethod]
        public void ServerClientPacketSerializeDataTest()
        {
            ServerClientPacket scp = new ServerClientPacket("10.0.0.1,10.0.0.27,15,155,Cloudy,285.5,284.25,75,78,100,22,155,1594353335,1594412149,3600");

            Assert.AreEqual("10.0.0.1,10.0.0.27,15,155,Cloudy,12.5,11.25,75,78,100,22,South-South-East,02:55,19:15,3600", scp.SerializeData());
        }

        [TestMethod]
        public void ServerClientPackettoPrintableTest()
        {
            ServerClientPacket scp = new ServerClientPacket("10.0.0.1,10.0.0.27,15,155,Cloudy,285.5,284.25,75,78,100,22,155,1594353335,1594412149,3600");

            string expected = "Printing the whole Server -> Client Packet: \nPacket:\nSource IP Address: 10.0.0.1\nDestination IP Address: 10.0.0.27\nNumber of Packets: 15\nLength of the Payload: 155\nWeather Data:\nWeather Type: Cloudy\nTemperature: 12.5°C\nFeels Like: 11.25°C\nPressure: 75 hPa\nHumidity: 78%\nVisibility: 100%\nWind Speed: 22 km/h\nWind Direction: South-South-East\nSunrise Time: 02:55\nSunset Time: 19:15\n";

            Assert.AreEqual(expected, scp.ToPrintable());
        }
    }
}