using System;
using System.Net;
using System.Net.Sockets;


namespace WeatherAppServer
{
    internal static class Server
    {
        public static void Main ()
        {
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
        }

        // TODO: Loop with functionality here

        // TODO: Shut down server here
    }
}