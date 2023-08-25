using RobotInterface.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace SocketServerExecute
{
    public class SocketServerTest
    {
        static int cnt = 0;
        static void Main(string[] args)
        {
            SocketServer server = new SocketServer(12345);
            server.OnExceptionThrown += Server_OnExceptionThrown;
            server.OnClientConnectionChanged += Server_OnClientConnectionChanged;
            server.OnReceivedMessage += Server_OnReceivedMessage;

            server.StartListening();
            Console.WriteLine("Server Listening...");
            while (true)
            {
                Console.WriteLine("명령어를 입력하세요. 1. StartListening 2. StopListening 3. 메세지 보내기 4. Client 지정하여 메세지 보내기 0. 종료");
                string command = Console.ReadLine();
                string message = string.Empty;
                switch (command)
                {
                    case "1":
                        server.StartListening();
                        break;
                    case "2":
                        server.StopListening();
                        break;
                    case "3":
                        Console.WriteLine("보낼 메세지를 입력하세요");
                        message = Console.ReadLine();
                        server.SendMessage(message);
                        break;
                    case "4":
                        Console.WriteLine("client를 입력하세요");
                        string clientId = Console.ReadLine();
                        Console.WriteLine("보낼 메세지를 입력하세요");
                        message = Console.ReadLine();
                        server.SendMessage(clientId, message);
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("잘못 입력되었습니다. 다시 입력하세요");
                        break;
                }

                if (command.Equals("0")){
                    break;
                }
            }
                
            
        }

        private static void Server_OnReceivedMessage(object? sender, CEventArgs.MessageReceivedArgs e)
        {
            Console.WriteLine(cnt++ + " "  + e.ClientID + " : " + e.Message);
        }

        private static void Server_OnClientConnectionChanged(object? sender, CEventArgs.ConnectionEventArgs e)
        {
            Console.WriteLine(e.ClientID + " : " + e.Connected);
        }

        private static void Server_OnExceptionThrown(object? sender, CEventArgs.InternalExceptionArgs e)
        {
            Console.WriteLine(e.MethodName + " : " + e.Exception);
        }
    }
}
