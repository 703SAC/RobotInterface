﻿using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClientExecute
{
    internal class SocketClientTest
    {
        static void Main(string[] args)
        {
            Thread.Sleep(3000);

            #region [Multiple Client Test]
            

            for (int j = 1; j <= 10; j++)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
                Task.Run(() => connectAndSendMessageSocketClient(socket, endPoint, "TestMessage" + j));
            }
            Console.WriteLine("for문 마감");
            Console.ReadLine();


            #endregion


            #region [Single Client Test]
            

            //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);

            //socket.Connect(endPoint);

            //byte[] buffer = new byte[1024];

            //buffer = Encoding.UTF8.GetBytes("Hello World!");

            //socket.Send(buffer);

            //Console.ReadLine();
            
            #endregion
        }

        public static void connectAndSendMessageSocketClient(Socket socket, IPEndPoint endPoint, string message)
        {
            socket.Connect(endPoint);
            
            byte[] buffer = Encoding.UTF8.GetBytes(message);

            byte[] withHeaderBuffer = new byte[2 + buffer.Length];
            byte[] dataSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)buffer.Length));
            //Console.WriteLine("dataSize : " + (short)buffer.Length);
            Array.Copy(dataSize, 0, withHeaderBuffer, 0, dataSize.Length);
            Array.Copy(buffer, 0, withHeaderBuffer, 2, buffer.Length);
            for (int i = 0; i < 20000; i++)
            {
                //Console.WriteLine(Encoding.UTF8.GetString(withHeaderBuffer));
                socket.Send(withHeaderBuffer);
            }
            Console.WriteLine("" + message + " 의 메세지 2만 번 Send 완료");
        }
    }
}