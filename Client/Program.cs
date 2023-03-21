using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Threading;
using Newtonsoft.Json;

class Client
{
    public static void Main(string[] args)
    {
        

        //Thread.Sleep(5000);
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
        IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Any, 0);

        using (NamedPipeClientStream clientStream = new NamedPipeClientStream(".", "mypipe", PipeDirection.In))
        {
            clientStream.Connect();

            byte[] buffer = new byte[1024];
                int bytesRead = 0;
                bytesRead = clientStream.Read(buffer, 0, buffer.Length);


                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                byte[] serverMessage = Encoding.ASCII.GetBytes(message);
                client.SendTo(serverMessage, remoteEP);
            
            

            byte[] serverResponse = new byte[1024];
            EndPoint ep = new IPEndPoint(IPAddress.Any, 0);

            int bytesReceived = client.ReceiveFrom(buffer, ref ep);
             string json = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

             var obj = JsonConvert.DeserializeObject(json);

             var sendBack = JsonConvert.SerializeObject(obj);

             using (var pipe = new NamedPipeClientStream("my-pipe"))
             {
                 pipe.Connect();
                 using (var writer = new StreamWriter(pipe))
                 {
                     writer.WriteLine(json);
                 }
             }

            



        }

    }

    
}