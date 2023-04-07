using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DataPacket
{
    public class Head
    {
        private char destination;
        private char source;
        private int numberOfPackets;
        private int dataLength;
        private char err;
        private char fin;

        public Head()
        {
            this.destination = 'c';
            this.source = 's';

            this.numberOfPackets = 0;
            this.dataLength = 0;

            this.err = '0';
            this.fin = '0';
        }

        public Head(char destination, char source, int numberOfPackets, int dataLength)
        {
            this.destination = destination;
            this.source = source;
            this.numberOfPackets = numberOfPackets;
            this.dataLength = dataLength;

            this.err = '0';
            this.fin = '0';
        }

        public Head(byte[] str)
        {
            string[] bufferSplit = (Encoding.ASCII.GetString(str)).Split(",");

            int i = 0;

            this.destination = (bufferSplit[i].ToCharArray())[0];
            this.source = (bufferSplit[++i].ToCharArray())[0];

            this.numberOfPackets = int.Parse(bufferSplit[++i]);
            this.dataLength = int.Parse(bufferSplit[++i]);

            this.err = (bufferSplit[++i].ToCharArray())[0];
            this.fin = (bufferSplit[++i].ToCharArray())[0];
        }

        /*unsafe public Head(char* ptr) 
        {
            fixed (char* destPtr = &destination)
            {
                Buffer.MemoryCopy(ptr, destPtr, Marshal.SizeOf(destination), Marshal.SizeOf(destination));
            }
            fixed (char* srcPtr = &source)
            {
                Buffer.MemoryCopy(ptr + Marshal.SizeOf(destination), srcPtr, Marshal.SizeOf(source), Marshal.SizeOf(source));
            }
            fixed (int* numPktPtr = &numberOfPackets)
            {
                Buffer.MemoryCopy(ptr + Marshal.SizeOf(destination) + Marshal.SizeOf(source), numPktPtr, Marshal.SizeOf(numberOfPackets), Marshal.SizeOf(numberOfPackets));
            }
            fixed (int* dataLengthPkt = &dataLength)
            {
                Buffer.MemoryCopy(ptr + Marshal.SizeOf(destination) + Marshal.SizeOf(source) + Marshal.SizeOf(numberOfPackets), dataLengthPkt, Marshal.SizeOf(dataLength), Marshal.SizeOf(dataLength));
            }
            fixed (char* errPtr = &err)
            {
                Buffer.MemoryCopy(ptr + Marshal.SizeOf(destination) + Marshal.SizeOf(source) + Marshal.SizeOf(numberOfPackets) + Marshal.SizeOf(dataLength), errPtr, Marshal.SizeOf(err), Marshal.SizeOf(err));
            }
            fixed (char* finPtr = &fin)
            {
                Buffer.MemoryCopy(ptr + Marshal.SizeOf(destination) + Marshal.SizeOf(source) + Marshal.SizeOf(numberOfPackets) + Marshal.SizeOf(dataLength) + Marshal.SizeOf(err), finPtr, Marshal.SizeOf(fin), Marshal.SizeOf(fin));
            }
        }*/

        public void setDataLength(int length)
        {
            this.dataLength = length;
        }

        public void SetErr(bool error)
        {
            if (error)
            {
                this.err = '1';
            }
            else
            {
                this.err = '0';
            }
        }

        public void SetFin(bool finish)
        {
            if (finish)
            {
                this.fin = '1';
            }
            else
            {
                this.fin = '0';
            }
        }

        public int SizeOf()
        {
            return Marshal.SizeOf(destination) + Marshal.SizeOf(source) + Marshal.SizeOf(numberOfPackets) + Marshal.SizeOf(dataLength) + Marshal.SizeOf(err) + Marshal.SizeOf(fin);
        }

        public override string ToString()
        {
            return this.destination + "," + this.source + "," + this.numberOfPackets + "," + this.dataLength + "," + this.err + "," + this.fin;
        }

        public string ToPrintable()
        {
            string dest;
            string src;
            switch (this.destination)
            {
                case 'c': dest = "Client"; break;
                case 's': dest = "Server"; break;
                default: dest = "Undefined"; break;
            }
            switch (this.source)
            {
                case 'c': src = "Client"; break;
                case 's': src = "Server"; break;
                default: src = "Undefined"; break;
            }
            return "Packet:\n" + "Destination: " + dest + "\nSource: " + src + "\nNumber of Packets: " + this.numberOfPackets + "\nLength of the Payload: " + this.dataLength + "\nFlags Err/Fin: " + this.err + "/" + this.fin + "\n";
        }
    }

    public class ServerClientPacket
    {
        const int HEAD_MEMBERS = 6;

        private Head head;
        private WeatherData data;

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

        
        public ServerClientPacket(char source, char destination, int numberOfPackets, int dataLength, string description, double temp, double feels_like, double temp_min, double temp_max, double pressure, double humidity, double speed, double deg, int sunrise, int sunset, int timezoneFromUTC)
        {
            this.head = new Head(source, destination, numberOfPackets, dataLength);
            this.data = new WeatherData(description, temp, feels_like, temp_min, temp_max, pressure, humidity, speed, deg, sunrise, sunset, timezoneFromUTC);
        }


        public ServerClientPacket(string apiResponse)
        {
            this.head = new();
            this.data = new(apiResponse);
        }


        public ServerClientPacket(byte[] str)
        {
            head = new Head(str);
            data = new WeatherData(str, HEAD_MEMBERS);
        }

        /*unsafe public ServerClientPacket(char* buff)
        {
            head = new(buff);

            data = new(buff, head.SizeOf());
        }*/

        public void SetErr(bool error)
        {
            head.SetErr(error);
        }

        public void SetFin(bool finish)
        {
            head.SetFin(finish);
        }

        unsafe public byte[] SerializeData()
        {
            return Encoding.ASCII.GetBytes(head.ToString() + "," + data.ToString());
        }

        public string ToPrintable()
        {
            return "Printing the whole Server -> Client Packet: \n" + this.head.ToPrintable() + this.data.ToPrintable();
        }
    }
}
