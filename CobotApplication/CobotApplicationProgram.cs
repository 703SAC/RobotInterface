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
            ConnectToRobotDashboard(robotIP, robotDashboardPort);

            InitGetStatusTimer();
        }


        #endregion

        #region [Private Variables]
        private System.Timers.Timer checkingStatusTimer;
        private IPEndPoint robotDashboardEndPoint;
        private Socket robotDashboardSocket;
        private static string autostoreInterfaceUrl = "http://192.168.0.220/AsInterfaceHttp/AutoStoreHttpInterface.aspx";
        #endregion


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
            string xmlGetPostStatus = createXmlGetPortStatus();
            sendAndReceiveWithAutostore(xmlGetPostStatus);
            #endregion
        }
        private void ConnectToRobotDashboard(string robotIP, int robotDashboardPort)
        {
            try
            {
                robotDashboardSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                robotDashboardEndPoint = new IPEndPoint(IPAddress.Parse(robotIP), robotDashboardPort);
                robotDashboardSocket.Connect(robotDashboardEndPoint);
                byte[] receiveBuffer = new byte[1024];
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return returnMessage;
        }
        private string sendAndReceiveWithAutostore(string xmlString)
        {
            string returnMessage = string.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    StringContent stringContent = new StringContent(xmlString, Encoding.UTF8);
                    var response = client.PostAsync(autostoreInterfaceUrl, stringContent).Result;
                    Console.WriteLine("http client post statusCode: " + response.StatusCode + " Content: " + response.Content);
                    StreamReader returnStream = new StreamReader(response.Content.ReadAsStream());
                    returnMessage = returnStream.ReadToEnd();

                    Console.WriteLine(returnMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return returnMessage;
        }
        

        private string createXmlGetPortStatus()
        {
            string xmlString = "";
            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xdoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            xdoc.AppendChild(xmlDeclaration);

            XmlNode root = xdoc.CreateElement("methodcall");
            xdoc.AppendChild(root);

            XmlNode methodName = xdoc.CreateElement("name");
            methodName.InnerText = "getPortStatus";
            root.AppendChild(methodName);

            XmlNode parameters = xdoc.CreateElement("params");
            root.AppendChild(parameters);

            XmlNode port_id = xdoc.CreateElement("port_id");
            port_id.InnerText = "1";
            parameters.AppendChild(port_id);

            xmlString = xdoc.OuterXml;

            return xmlString;
        }

        public string createXmlRequest(string method)
        {
            string xmlString = "";


            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xdoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            xdoc.AppendChild(xmlDeclaration);
            //xdoc.CreateXmlDeclaration("1.0", "UTF-8", "no");

            XmlNode root = xdoc.CreateElement("methodcall");
            xdoc.AppendChild(root);

            XmlNode methodName = xdoc.CreateElement("name");
            methodName.InnerText = method;
            root.AppendChild(methodName);

            XmlNode parameters = xdoc.CreateElement("params");
            XmlNode minBinId = xdoc.CreateElement("min_bin_id");
            minBinId.InnerText = "100002";
            XmlNode maxBinId = xdoc.CreateElement("max_bin_id");
            maxBinId.InnerText = "100032";

            parameters.AppendChild(minBinId);
            parameters.AppendChild(maxBinId);

            root.AppendChild(parameters);

            xdoc.Save(@"C:\xmlTest.xml");

            xmlString = xdoc.OuterXml;



            Console.WriteLine($"xmlString : {xmlString}");

            return xmlString;
        }

        public string createXmlRequest(string method, string port, string bin)
        {
            string xmlString = "";


            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xdoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            xdoc.AppendChild(xmlDeclaration);
            //xdoc.CreateXmlDeclaration("1.0", "UTF-8", "no");

            XmlNode root = xdoc.CreateElement("methodcall");
            xdoc.AppendChild(root);

            XmlNode methodName = xdoc.CreateElement("name");
            methodName.InnerText = method;
            root.AppendChild(methodName);

            XmlNode parameters = xdoc.CreateElement("params");
            XmlNode portId = xdoc.CreateElement("port_id");


            portId.InnerText = port;

            XmlNode binId = xdoc.CreateElement("bin_id");
            binId.InnerText = bin;

            parameters.AppendChild(portId);
            parameters.AppendChild(binId);

            root.AppendChild(parameters);

            xdoc.Save(@"C:\xmlTest.xml");

            xmlString = xdoc.OuterXml;



            Console.WriteLine($"xmlString : {xmlString}");

            return xmlString;
        }

        public string createXmlRequest(string method, string port)
        {
            string xmlString = "";


            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xdoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            xdoc.AppendChild(xmlDeclaration);
            //xdoc.CreateXmlDeclaration("1.0", "UTF-8", "no");

            XmlNode root = xdoc.CreateElement("methodcall");
            xdoc.AppendChild(root);

            XmlNode methodName = xdoc.CreateElement("name");
            methodName.InnerText = method;
            root.AppendChild(methodName);

            XmlNode parameters = xdoc.CreateElement("params");
            if (method.Equals("openport"))
            {
                XmlNode select = xdoc.CreateElement("select");
                XmlNode category = xdoc.CreateElement("category");
                category.InnerText = "1";
                select.AppendChild(category);
                parameters.AppendChild(select);
            }
            XmlNode portId = xdoc.CreateElement("port_id");
            portId.InnerText = port;

            parameters.AppendChild(portId);

            root.AppendChild(parameters);

            xdoc.Save(@"C:\xmlTest.xml");

            xmlString = xdoc.OuterXml;



            Console.WriteLine($"xmlString : {xmlString}");

            return xmlString;
        }

    }
}