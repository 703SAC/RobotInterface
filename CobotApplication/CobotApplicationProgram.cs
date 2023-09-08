using RobotInterface.Driver;
using System.Collections;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;

namespace CobotApplication
{
    public class CobotApplicationProgram
    {

        #region [Constructor]
        // [Singleton] 생성자를 외부에서 호출할 수 없도록 private으로 지정
        private CobotApplicationProgram()
        {            
        }
        public CobotApplicationProgram(string robotIP = "192.168.0.4", int robotDashboardPort = 29999)
        {
            //ConnectToRobotDashboard(robotIP, robotDashboardPort);
            autoresetEvent = new AutoResetEvent(false);
            //InitGetStatusTimer();
        }




        #endregion

        #region [Private Variables]
        private System.Timers.Timer checkingStatusTimer;
        private IPEndPoint robotDashboardEndPoint;
        private Socket robotDashboardSocket;
        private SocketServerWithoutHeader robotSocketServer;
        private IPEndPoint robotSocketEndPoint;
        private AutoResetEvent autoresetEvent;
        private AutostoreInterfaceDriver autostoreInterfaceDriver;
        #endregion

        #region [Public Methods]
        public void RunProgram()
        {
            robotSocketServer = new SocketServerWithoutHeader("192.168.0.9", 6005);
            robotSocketServer.OnExceptionThrown += RobotSocketServer_OnExceptionThrown;
            robotSocketServer.OnClientConnectionChanged += RobotSocketServer_OnClientConnectionChanged;
            robotSocketServer.OnReceivedMessage += RobotSocketServer_OnReceivedMessage;
            //robotSocketServer.StartListening();
            autostoreInterfaceDriver = new AutostoreInterfaceDriver();

            while (true)
            {
                Console.WriteLine("명령어 숫자 입력 \n 1: Open Port \n 2: Open Bin \n 3: UR Robot Run \n 4: Close Bin \n 5: Get Bin Info \n 6: Close Port \n 7: Get Bin State \n 8: Get Port Status \n 0: quit");
                int command = Convert.ToInt32(Console.ReadLine());
                switch (command)
                {
                    default:
                        Console.WriteLine("잘못 입력되었습니다. 다시 입력하세요");
                        break;
                    case 0:
                        robotSocketServer.StopListening();
                        return;
                    case 1:
                        string openPortXml = autostoreInterfaceDriver.createXmlOpenAndClosePort("openport", "1");
                        autostoreInterfaceDriver.sendAndReceiveWithAutostore(openPortXml);
                        CheckAutostoreStatus();
                        break;
                    case 2:
                        Console.WriteLine("현재 포트 상태를 나타냅니다. ");
                        CheckAutostoreStatus();
                        Console.WriteLine("Open할 Bin Number를 입력하세요. 0을 입력하면 뒤로 돌아갑니다. ");
                        string openBinId = Console.ReadLine();
                        if (openBinId.Equals("0"))
                            break;
                        string openBinXml = autostoreInterfaceDriver.createXmlOpenAndCloseBin("openbin", "1", openBinId);
                        autostoreInterfaceDriver.sendAndReceiveWithAutostore(openBinXml);
                        break;
                    case 3:
                        sendAndReceiveWithRobotDashboard("stop");
                        sendAndReceiveWithRobotDashboard("load 0901dhcp.urp");

                        Console.WriteLine("UR Robot Loaded Program 을 실행합니다.");
                        sendAndReceiveWithRobotDashboard("play");
                        autoresetEvent.WaitOne();
                        //robotSocketServer.StopListening();
                        break;
                    case 4:
                        Console.WriteLine("현재 포트 상태를 나타냅니다. ");
                        CheckAutostoreStatus();
                        Console.WriteLine("Close할 Bin Number를 입력하세요. 0을 입력하면 뒤로 돌아갑니다. ");
                        string closeBinId = Console.ReadLine();
                        if (closeBinId.Equals("0"))
                            break;
                        string closeBinXml = autostoreInterfaceDriver.createXmlOpenAndCloseBin("closebin", "1", closeBinId);
                        autostoreInterfaceDriver.sendAndReceiveWithAutostore(closeBinXml);
                        break;
                    case 5:
                        Console.WriteLine("현재 모든 bin 정보를 나타냅니다. ");
                        string getBinInfo = autostoreInterfaceDriver.createXmlGetBinInfo("getbininfo");
                        autostoreInterfaceDriver.sendAndReceiveWithAutostore(getBinInfo);
                        break;
                    case 6:
                        string closePortXml = autostoreInterfaceDriver.createXmlOpenAndClosePort("closeport", "1");
                        autostoreInterfaceDriver.sendAndReceiveWithAutostore(closePortXml);
                        CheckAutostoreStatus();
                        break;
                    case 7:
                        string getBinStateXml = autostoreInterfaceDriver.createXmlGetBinInfo("getbinstate");
                        autostoreInterfaceDriver.sendAndReceiveWithAutostore(getBinStateXml);
                        break;
                    case 8:
                        CheckAutostoreStatus();
                        break;
                }

            }
        }

        private void RobotSocketServer_OnReceivedMessage(object? sender, CEventArgs.MessageReceivedArgs e)
        {
            
            if (e.Message.Contains("shape"))
            {
                int shape = 0;
                
                while (true)
                {
                    Console.WriteLine("물건의 유형을 입력해 주세요. 1: 원통 2: 직사각형 3: 군집 ");
                    ArrayList arrayList = new ArrayList();
                    arrayList.Add(1);
                    arrayList.Add(2);
                    arrayList.Add(3);
                    shape = Convert.ToInt32(Console.ReadLine());

                    if (arrayList.Contains(shape) == true)
                        break;
                    else
                        Console.WriteLine("입력이 잘못되었습니다 : " + shape);
                }
                
                robotSocketServer.SendMessage(e.ClientID, shape);

            }
            else if (e.Message.Contains("target"))
            {
                int target = 0;

                while (true)
                {
                    Console.WriteLine("target을 입력해 주세요. 1 or 2");
                    ArrayList arrayList = new ArrayList();
                    arrayList.Add(1);
                    arrayList.Add(2);
                    target = Convert.ToInt32(Console.ReadLine());

                    if (arrayList.Contains(target) == true)
                        break;
                    else
                        Console.WriteLine("입력이 잘못되었습니다 : " + target);
                }

                robotSocketServer.SendMessage(e.ClientID, target);
            }
            else if (e.Message.Contains("number"))
            {
                Console.WriteLine("꺼낼 물건 개수를 입력해 주세요.");
                int numberOfItem = Convert.ToInt32(Console.ReadLine());

                robotSocketServer.SendMessage(e.ClientID, numberOfItem);
            }
            else if (e.Message.Contains("complete"))
            {
                autoresetEvent.Set();
            }



        }

        private void RobotSocketServer_OnClientConnectionChanged(object? sender, CEventArgs.ConnectionEventArgs e)
        {
            Console.WriteLine(e.ClientID + " : " + e.Connected);
        }

        private void RobotSocketServer_OnExceptionThrown(object? sender, CEventArgs.InternalExceptionArgs e)
        {
            Console.WriteLine("RobotSocketServer_OnExceptionThrown : " + e.MethodName + ", " + e.Exception);
        }

        #endregion

        #region [Pirvate Methods]



        private void InitGetStatusTimer()
        {
            checkingStatusTimer = new System.Timers.Timer();
            checkingStatusTimer.Interval = 1000;
            checkingStatusTimer.Elapsed += new System.Timers.ElapsedEventHandler(CheckStatusTimer_Tick);
            checkingStatusTimer.Start();
        }

        // 비동기로 실행되기 때문에 타이머용 Socket을 따로 생성해야 할 듯?
        private void CheckStatusTimer_Tick(object sender, EventArgs e)
        {
            #region [Robot Status Check]
            sendAndReceiveWithRobotDashboard("get operational mode\n");
            sendAndReceiveWithRobotDashboard("programState\n");
            sendAndReceiveWithRobotDashboard("robotmode\n");
            sendAndReceiveWithRobotDashboard("safetystatus\n");
            #endregion

            #region [Autostore Status Check]
            string xmlGetPostStatus = autostoreInterfaceDriver.createXmlGetPortStatus();
            autostoreInterfaceDriver.sendAndReceiveWithAutostore(xmlGetPostStatus);
            #endregion
        }

        private void CheckAutostoreStatus()
        {
            string xmlGetPostStatus = autostoreInterfaceDriver.createXmlGetPortStatus();
            autostoreInterfaceDriver.sendAndReceiveWithAutostore(xmlGetPostStatus);
        }

        private void ConnectToRobotDashboard(string robotIP, int robotDashboardPort)
        {
            try
            {
                robotDashboardSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                robotDashboardEndPoint = new IPEndPoint(IPAddress.Parse(robotIP), robotDashboardPort);
                robotDashboardSocket.Connect(robotDashboardEndPoint);
                byte[] receiveBuffer = new byte[1024];
                robotDashboardSocket.Receive(receiveBuffer);
                Console.WriteLine(Encoding.ASCII.GetString(receiveBuffer));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
        private string sendAndReceiveWithRobotDashboard(string message)
        {
            string returnMessage = "";

            try
            {
                if (robotDashboardSocket.Connected == false)
                {
                    Console.WriteLine("robot과 통신 연결을 확인해주세요.");
                }

                byte[] sendBuffer = Encoding.ASCII.GetBytes(message + "\n");
                robotDashboardSocket.Send(sendBuffer);
                byte[] receiveBuffer = new byte[1024];
                int byteCountReceive = robotDashboardSocket.Receive(receiveBuffer);
                if (byteCountReceive > 0)
                {
                    returnMessage = Encoding.ASCII.GetString(receiveBuffer);
                    Console.WriteLine(returnMessage);
                }
                else
                {
                    Console.WriteLine("robot 통신 연결을 확인해주세요.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return returnMessage;
        }

        #endregion
    }
}