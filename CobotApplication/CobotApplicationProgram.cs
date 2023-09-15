using RobotInterface.Driver;
using System.Collections;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Sockets;
using System.Security;
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
        public CobotApplicationProgram(string robotIP = "192.168.0.243", int robotDashboardPort = 29999)
        {
            ConnectToRobotDashboard(robotIP, robotDashboardPort);
            autoresetEventForRobotSocket = new AutoResetEvent(false);
            autoresetEventForTimer = new AutoResetEvent(false);
            //InitGetStatusTimer();
        }




        #endregion

        #region [Private Variables]
        private System.Timers.Timer checkingStatusTimer;
        private IPEndPoint robotDashboardEndPoint;
        private Socket robotDashboardSocket;
        private static SocketServerWithoutHeader? robotSocketServer;
        private IPEndPoint robotSocketEndPoint;
        private AutoResetEvent autoresetEventForRobotSocket;
        private AutoResetEvent autoresetEventForTimer;
        private AutostoreInterfaceDriver autostoreTaskInterfaceDriver;
        private AutostoreInterfaceDriver autostoreBinInterfaceDriver;
        private Dictionary<string, int> pickingInfos = new Dictionary<string, int>();
        private static Queue<int[]> pickingPlans = new Queue<int[]>();

        #endregion

        #region [Public Methods]
        public void RunConsoleProgram()
        {
            robotSocketServer = new SocketServerWithoutHeader("192.168.0.240", 6005);
            robotSocketServer.OnExceptionThrown += RobotSocketServer_OnExceptionThrown;
            robotSocketServer.OnClientConnectionChanged += RobotSocketServer_OnClientConnectionChanged;
            robotSocketServer.OnReceivedMessage += RobotSocketServer_OnReceivedMessageConsole;
            robotSocketServer.StartListening();
            autostoreTaskInterfaceDriver = new AutostoreInterfaceDriver("task");
            //autostoreBinInterfaceDriver = new AutostoreInterfaceDriver("bin");
            while (true)
            {
                Console.WriteLine("명령어 숫자 입력 \n 1: Open Port \n 2: Open Bin \n 3: UR Robot Run \n 4: Close Bin \n 5: Get Bin Info \n 6: Close Port \n 7: Get Bin State \n 8: Get Port Status \n 9: Open Port For Insertion \n 10: Insert Bin \n 0: Quit");
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
                        string openPortXml = autostoreTaskInterfaceDriver.createXmlOpenAndClosePort("openport", "1");
                        autostoreTaskInterfaceDriver.sendAndReceiveWithAutostore(openPortXml);
                        CheckAutostoreStatus();
                        break;
                    case 2:
                        Console.WriteLine("현재 포트 상태를 나타냅니다. ");
                        CheckAutostoreStatus();
                        Console.WriteLine("Open할 Bin Number를 입력하세요. 0을 입력하면 뒤로 돌아갑니다. ");
                        string openBinId = Console.ReadLine();
                        if (openBinId.Equals("0"))
                            break;
                        string openBinXml = autostoreTaskInterfaceDriver.createXmlOpenAndCloseBin("openbin", "1", openBinId);
                        autostoreTaskInterfaceDriver.sendAndReceiveWithAutostore(openBinXml);
                        break;
                    case 3:
                        RunURScript("Test_Senario_2_230912_with_communication.urp");
                        break;
                    case 4:
                        Console.WriteLine("현재 포트 상태를 나타냅니다. ");
                        CheckAutostoreStatus();
                        Console.WriteLine("Close할 Bin Number를 입력하세요. 0을 입력하면 뒤로 돌아갑니다. ");
                        string closeBinId = Console.ReadLine();
                        if (closeBinId.Equals("0"))
                            break;
                        string closeBinXml = autostoreTaskInterfaceDriver.createXmlOpenAndCloseBin("closebin", "1", closeBinId);
                        autostoreTaskInterfaceDriver.sendAndReceiveWithAutostore(closeBinXml);
                        break;
                    case 5:
                        Console.WriteLine("현재 모든 bin 정보를 나타냅니다. ");
                        string getBinInfo = autostoreTaskInterfaceDriver.createXmlGetBinInfo("getbininfo");
                        autostoreTaskInterfaceDriver.sendAndReceiveWithAutostore(getBinInfo);
                        break;
                    case 6:
                        string closePortXml = autostoreTaskInterfaceDriver.createXmlOpenAndClosePort("closeport", "1");
                        autostoreTaskInterfaceDriver.sendAndReceiveWithAutostore(closePortXml);
                        CheckAutostoreStatus();
                        break;
                    case 7:
                        string getBinStateXml = autostoreTaskInterfaceDriver.createXmlGetBinInfo("getbinstate");
                        autostoreTaskInterfaceDriver.sendAndReceiveWithAutostore(getBinStateXml);
                        break;
                    case 8:
                        CheckAutostoreStatus();
                        break;
                    //case 9:
                    //    Console.WriteLine("Port Queue에 대기시킬 Bin Number를 입력하세요. ");
                    //    string prepareBinID = Console.ReadLine();
                    //    string prepareBinXml = autostoreBinInterfaceDriver.createXmlPrepareBin("preparebin", "1", prepareBinID);
                    //    autostoreBinInterfaceDriver.sendAndReceiveWithAutostore(prepareBinXml);
                    //    break;
                    //case 10:
                    //    Console.WriteLine("Port Queue에 Append 시킬 Bin Number를 입력하세요");
                    //    string appendBinId = Console.ReadLine();
                    //    string appendPortQueue = autostoreBinInterfaceDriver.createXmlAppendPortQueue("appendportqueue", "1", appendBinId);
                    //    autostoreBinInterfaceDriver.sendAndReceiveWithAutostore(appendPortQueue);
                    //    break;
                    case 9:
                        Console.WriteLine("Open Port For Insertion");
                        string OpenPortForInsertXml = autostoreTaskInterfaceDriver.createXmlOpenAndClosePort("openport", "1", true);
                        autostoreTaskInterfaceDriver.sendAndReceiveWithAutostore(OpenPortForInsertXml);
                        break;
                    case 10:
                        Console.WriteLine("Insert Bin Number를 입력하세요");
                        string insertBinId = Console.ReadLine();
                        string InsertBinXml = autostoreTaskInterfaceDriver.createXmlOpenAndCloseBin("insertbin", "1", insertBinId);
                        autostoreTaskInterfaceDriver.sendAndReceiveWithAutostore(InsertBinXml);
                        break;
                }

            }
        }

        private void RunURScript(string scriptName)
        {
            int targetCount, target, product, pickingCount = 0;
            while (true)
            {
                Console.WriteLine("Target 수를 입력하세요 1 or 2");
                if(int.TryParse(Console.ReadLine(), out targetCount) == true)
                {
                    break;
                }
                Console.WriteLine("입력이 잘못 되었습니다. 다시 입력해 주세요.");
            }


            if (targetCount == 1)
            {
                while (true)
                {
                    Console.WriteLine("Target 종류를 입력하세요 1 or 2");
                    if (int.TryParse(Console.ReadLine(), out target) == false)
                    {
                        Console.WriteLine("입력이 잘못 되었습니다. 다시 입력해 주세요.");
                        continue;
                    }
                    Console.WriteLine("물건의 유형을 입력해 주세요. \n1: 직사각형(Box) \n2: 원통(Can) \n3: Mixed 1 \n4: Mixed 2 ");
                    if (int.TryParse(Console.ReadLine(), out product) == false)
                    {
                        Console.WriteLine("입력이 잘못 되었습니다. 다시 입력해 주세요.");
                        continue;
                    }
                    Console.WriteLine("Picking 개수를 입력하세요");
                    if (int.TryParse(Console.ReadLine(), out pickingCount) == false)
                    {
                        Console.WriteLine("입력이 잘못 되었습니다. 다시 입력해 주세요.");
                        continue;
                    }
                    int[] sets = new int[3];
                    sets[0] = target;
                    sets[1] = product;
                    sets[2] = pickingCount;

                    pickingPlans.Enqueue(sets);
                    break;
                }
            }
            else if(targetCount >= 2)
            {
                for(int targetNumber = 0; targetNumber < targetCount; targetNumber++)
                {
                    while (true)
                    {
                        Console.WriteLine("Target " + (targetNumber + 1) + " : 물건의 유형을 입력해 주세요. \n1: 직사각형(Box) \n2: 원통(Can) \n3: Mixed 1 \n4: Mixed 2 ");
                        if (int.TryParse(Console.ReadLine(), out product) == false)
                        {
                            Console.WriteLine("입력이 잘못 되었습니다. 다시 입력해 주세요.");
                            continue;
                        }
                        Console.WriteLine("Target " + (targetNumber + 1) + " : Picking 개수를 입력하세요");
                        if (int.TryParse(Console.ReadLine(), out pickingCount) == false)
                        {
                            Console.WriteLine("입력이 잘못 되었습니다. 다시 입력해 주세요.");
                            continue;
                        }
                        break;
                    }
                    int[] sets = new int[3];
                    sets[0] = targetNumber + 1;
                    sets[1] = product;
                    sets[2] = pickingCount;

                    pickingPlans.Enqueue(sets);
                }
            }
            try
            {
                for (int targetNumber = 0; targetNumber < targetCount; targetNumber++)
                {
                    sendAndReceiveWithRobotDashboard("stop");
                    sendAndReceiveWithRobotDashboard("load " + scriptName);

                    Console.WriteLine("UR Robot Loaded Program 을 실행합니다.");
                    sendAndReceiveWithRobotDashboard("play");
                    autoresetEventForRobotSocket.WaitOne();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        public void RunWinformProgram()
        {
            Task.Run(() =>
            {
                robotSocketServer = new SocketServerWithoutHeader("192.168.0.240", 6005);
                robotSocketServer.OnExceptionThrown += RobotSocketServer_OnExceptionThrown;
                robotSocketServer.OnClientConnectionChanged += RobotSocketServer_OnClientConnectionChanged;
                robotSocketServer.OnReceivedMessage += RobotSocketServer_OnReceivedMessageWinform;
                robotSocketServer.StartListening();
            });
            
            autostoreTaskInterfaceDriver = new AutostoreInterfaceDriver("task");
            
        }
        public void OnClick_btn_URRun(Dictionary<string, int> pickingInfosInput)
        {
            pickingInfos = pickingInfosInput;

            sendAndReceiveWithRobotDashboard("stop");
            sendAndReceiveWithRobotDashboard("load Test_Senario_1_230912_with_communication.urp");

            Console.WriteLine("UR Robot Loaded Program 을 실행합니다.");
            sendAndReceiveWithRobotDashboard("play");
            autoresetEventForRobotSocket.WaitOne();
        }

        private void RobotSocketServer_OnReceivedMessageWinform(object? sender, CEventArgs.MessageReceivedArgs e)
        {
            if (e.Message.Contains("shape"))
            {
                int shape = pickingInfos["Product"];
                robotSocketServer.SendMessage(e.ClientID, shape);

            }
            else if (e.Message.Contains("target"))
            {
                int target = pickingInfos["target"];
                robotSocketServer.SendMessage(e.ClientID, target);
            }
            else if (e.Message.Contains("number"))
            {
                Console.WriteLine("꺼낼 물건 개수를 입력해 주세요.");
                int numberOfItem = pickingInfos["PickCount"];

                robotSocketServer.SendMessage(e.ClientID, numberOfItem);
            }
            else if (e.Message.Contains("complete"))
            {
                autoresetEventForRobotSocket.Set();
            }
        }

        private void RobotSocketServer_OnReceivedMessageConsole(object? sender, CEventArgs.MessageReceivedArgs e)
        {
            
            if (e.Message.Contains("shape"))
            {
                int shape = 0;
                
                while (true)
                {
                    Console.WriteLine("물건의 유형을 입력해 주세요. \n1: 직사각형(Box) \n2: 원통(Can) \n3: Mixed 1 \n4: Mixed 2 ");
                    shape = Convert.ToInt32(Console.ReadLine());
                    switch (shape)
                    {
                        case 1: case 2: case 3: case 4:
                            robotSocketServer.SendMessage(e.ClientID, shape);
                            return;
                        default:
                            Console.WriteLine("입력이 잘못되었습니다 : " + shape);
                            break;
                    }

                }
            }
            else if (e.Message.Contains("target"))
            {
                int target = 0;
                while (true)
                {
                    Console.WriteLine("target을 입력해 주세요. 1 or 2");
                    target = Convert.ToInt32(Console.ReadLine());
                    switch (target)
                    {
                        case 1: case 2 :
                            robotSocketServer.SendMessage(e.ClientID, target);
                            return;
                        default:
                            Console.WriteLine("입력이 잘못되었습니다 : " + target);
                            break;
                    }
                }

            }
            else if (e.Message.Contains("number"))
            {
                Console.WriteLine("꺼낼 물건 개수를 입력해 주세요.");
                int numberOfItem = Convert.ToInt32(Console.ReadLine());

                robotSocketServer.SendMessage(e.ClientID, numberOfItem);
            }
            else if (e.Message.Contains("complete"))
            {
                autoresetEventForRobotSocket.Set();
            }
            else if (e.Message.Contains("pickinfo"))
            {
                int[] sets = pickingPlans.Dequeue();
                robotSocketServer.SendMessage(e.ClientID, sets[0]);
                robotSocketServer.SendMessage(e.ClientID, sets[1]);
                robotSocketServer.SendMessage(e.ClientID, sets[2]);
            }
            else if (e.Message.Contains("failrerequest"))
            {
                Console.WriteLine("전송에 실패하였습니다.");
            }
            else if (e.Message.Contains("error"))
            {
                Console.WriteLine("UR robot item dropped!");
                //autoresetEventForRobotSocket.Set();
                //pickingPlans.Clear();
                //throw new Exception("UR robot item dropped!");
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
            sendAndReceiveWithRobotDashboard("programState\n");
            sendAndReceiveWithRobotDashboard("robotmode\n");
            #endregion

            #region [Autostore Status Check]
            string xmlGetPostStatus = autostoreTaskInterfaceDriver.createXmlGetPortStatus();
            autostoreTaskInterfaceDriver.sendAndReceiveWithAutostore(xmlGetPostStatus);
            #endregion
        }

        private void CheckAutostoreStatus()
        {
            string xmlGetPostStatus = autostoreTaskInterfaceDriver.createXmlGetPortStatus();
            autostoreTaskInterfaceDriver.sendAndReceiveWithAutostore(xmlGetPostStatus);
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
                    if (returnMessage.Contains("Fail"))
                    {
                        throw new Exception("robot 명령 실패");
                    }
                }
                else
                {
                    Console.WriteLine("robot 통신 연결을 확인해주세요.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return returnMessage;
        }

        #endregion
    }
}