//using DataPackets;

//namespace WeatherAppTests
//{
//    [TestClass]
//    public class DataPacketsTests
//    {
//        // Testing the Default Constructor for Server -> Client Data Packet
//        [TestMethod]
//        public void ServerClientPacketDefConstrTestHead()
//        {
//            Head head = new Head();

//            ServerClientPacket scp = new ServerClientPacket();

//            Assert.AreEqual(head.ToString(), scp.head.ToString());
//        }
//        [TestMethod]
//        public void ServerClientPacketDefConstrTestData()
//        {
//            WeatherData data = new WeatherData();

//            ServerClientPacket scp = new ServerClientPacket();

//            Assert.AreEqual(data.ToString(), scp.data.ToString());
//        }
//        // Testing the Custom Constructor #1 for Server -> Client Data Packet with empty fields
//        [TestMethod]
//        public void ServerClientPacketCustConstr1TestHeadE()
//        {
//            Head head = new Head();
//            WeatherData data = new WeatherData();

//            ServerClientPacket scp = new ServerClientPacket(head, data);

//            Assert.AreEqual(head.ToString(), scp.head.ToString());
//        }
//        [TestMethod]
//        public void ServerClientPacketCustConstr1TestDataE()
//        {
//            Head head = new Head();
//            WeatherData data = new WeatherData();

//            ServerClientPacket scp = new ServerClientPacket(head, data);

//            Assert.AreEqual(data.ToString(), scp.data.ToString());
//        }

//        // Testing the Custom Constructor #1 for Server -> Client Data Packet with non-empty fields
//        [TestMethod]
//        public void ServerClientPacketCustConstr1TestHeadF()
//        {
//            Head head = new Head("10.0.0.1", "10.0.0.27", 15, 155);
//            WeatherData data = new WeatherData("Cloudy", 12.5, 11.25, 75, 78, 100, 22, "155", "1594353335", "1594412149", 3600);

//            ServerClientPacket scp = new ServerClientPacket(head, data);

//            Assert.AreEqual(head.ToString(), scp.head.ToString());
//        }
//        [TestMethod]
//        public void ServerClientPacketCustConstr1TestDataF()
//        {
//            Head head = new Head("10.0.0.1", "10.0.0.27", 15, 155);
//            WeatherData data = new WeatherData("Cloudy", 12.5, 11.25, 75, 78, 100, 22, "155", "1594353335", "1594412149", 3600);

//            ServerClientPacket scp = new ServerClientPacket(head, data);

//            Assert.AreEqual(data.ToString(), scp.data.ToString());
//        }

//        // Testing the Custom Constructor #2 for Server -> Client Data Packet with non-empty fields
//        [TestMethod]
//        public void ServerClientPacketCustConstr2TestHeadF()
//        {
//            Head head = new Head("10.0.0.1", "10.0.0.27", 15, 155);
//            WeatherData data = new WeatherData("Cloudy", 12.5, 11.25, 75, 78, 100, 22, "155", "1594353335", "1594412149", 3600);

//            ServerClientPacket scp = new ServerClientPacket("10.0.0.1", "10.0.0.27", 15, 155, "Cloudy", "12.5", "11.25", "75", "78", "100", "22", "155", "1594353335", "1594412149", "3600");

//            Assert.AreEqual(head.ToString(), scp.head.ToString());
//        }
//        [TestMethod]
//        public void ServerClientPacketCustConstr2TestDataF()
//        {
//            Head head = new Head("10.0.0.1", "10.0.0.27", 15, 155);
//            WeatherData data = new WeatherData("Cloudy", 12.5, 11.25, 75, 78, 100, 22, "155", "1594353335", "1594412149", 3600);

//            ServerClientPacket scp = new ServerClientPacket("10.0.0.1", "10.0.0.27", 15, 155, "Cloudy", "285.5", "284.25", "75", "78", "100", "22", "155", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("Cloudy,12.5,11.25,75,78,100,22,South-South-East,02:55,19:15,3600", scp.data.ToString());
//        }

//        // Testing the Custom Constructor #3 for Server -> Client Data Packet with non-empty fields
//        [TestMethod]
//        public void ServerClientPacketCustConstr3TestHeadF()
//        {
//            Head head = new Head("10.0.0.1", "10.0.0.27", 15, 155);
//            WeatherData data = new WeatherData("Cloudy", 12.5, 11.25, 75, 78, 100, 22, "155", "1594353335", "1594412149", 3600);

//            ServerClientPacket scp = new ServerClientPacket("10.0.0.1,10.0.0.27,15,155,Cloudy,285.5,284.25,75,78,100,22,155,1594353335,1594412149,3600");

//            Assert.AreEqual(head.ToString(), scp.head.ToString());
//        }
//        [TestMethod]
//        public void ServerClientPacketCustConstr3TestDataF()
//        {
//            Head head = new Head("10.0.0.1", "10.0.0.27", 15, 155);
//            WeatherData data = new WeatherData("Cloudy", 12.5, 11.25, 75, 78, 100, 22, "155", "1594353335", "1594412149", 3600);

//            ServerClientPacket scp = new ServerClientPacket("10.0.0.1,10.0.0.27,15,155,Cloudy,285.5,284.25,75,78,100,22,155,1594353335,1594412149,3600");

//            Assert.AreEqual("Cloudy,12.5,11.25,75,78,100,22,South-South-East,02:55,19:15,3600", scp.data.ToString());
//        }

//        [TestMethod]
//        public void ServerClientPacketSerializeDataTest()
//        {
//            ServerClientPacket scp = new ServerClientPacket("10.0.0.1,10.0.0.27,15,155,Cloudy,285.5,284.25,75,78,100,22,155,1594353335,1594412149,3600");

//            Assert.AreEqual("10.0.0.1,10.0.0.27,15,155,Cloudy,12.5,11.25,75,78,100,22,South-South-East,02:55,19:15,3600", scp.SerializeData());
//        }

//        [TestMethod]
//        public void ServerClientPackettoPrintableTest()
//        {
//            ServerClientPacket scp = new ServerClientPacket("10.0.0.1,10.0.0.27,15,155,Cloudy,285.5,284.25,75,78,100,22,155,1594353335,1594412149,3600");

//            string expected = "Printing the whole Server -> Client Packet: \nPacket:\nSource IP Address: 10.0.0.1\nDestination IP Address: 10.0.0.27\nNumber of Packets: 15\nLength of the Payload: 155\nWeather Data:\nWeather Type: Cloudy\nTemperature: 12.5�C\nFeels Like: 11.25�C\nPressure: 75 hPa\nHumidity: 78%\nVisibility: 100%\nWind Speed: 22 km/h\nWind Direction: South-South-East\nSunrise Time: 02:55\nSunset Time: 19:15\n";

//            Assert.AreEqual(expected, scp.ToPrintable());
//        }

//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionN1()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "355", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("North", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionN2()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "9.5", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("North", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionNNE()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "15.5", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("North-North-East", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionNE()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "45.5", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("North-East", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionENE()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "72.35", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("East-North-East", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionE()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "90", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("East", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionESE()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "105", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("East-South-East", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionSE()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "135.95", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("South-East", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionSSE()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "160", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("South-South-East", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionS()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "180", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("South", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionSSW()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "195", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("South-South-West", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionSW()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "220", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("South-West", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionWSW()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "245", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("West-South-West", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionW()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "270", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("West", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionWNW()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "282", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("West-North-West", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionNW()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "315", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("North-West", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionNNW()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "345", "1594353335", "1594412149", "3600");

//            Assert.AreEqual("North-North-West", data.windDirection);
//        }
//        [TestMethod]
//        public void ServerClientPacketCardinalDirectionUnd()
//        {
//            WeatherData data = new WeatherData("Windy", "274.5", "270", "55", "59", "100", "25", "-5", "1594353335", "1594412149", "3600");

            Assert.AreEqual("Undefined", data.windDirection);
        }
    }

    [TestClass]
    public class ServerTests
    {
        [TestMethod]
        public void ServerTest1 ()
        {

        }
    }

    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void ClientTest1()
        {

        }
    }


    [TestClass]
    public class APITests
    {
        [TestMethod]
        public void APITest1()
        {

        }
    }
}