using System;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace HttpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string taskInterfaceUrl = "http://192.168.0.220/AsInterfaceHttp/AutoStoreHttpInterface.aspx";

            HttpClient client = new HttpClient();
            //var res = client.GetAsync("").Result;
            //Console.WriteLine("Response : " + res.StatusCode);

            
            while (true)
            {
                Console.WriteLine("명령어를 입력하세요. 1. getPortStatus 2. OpenBin 3. CloseBin 4. OpenPort 5. ClosePort 6.GetBinInfo  0. 종료 ");
                int command = Convert.ToInt32(Console.ReadLine());
                string stringXml = "";
                string binNumber = "";
                switch (command)
                {
                    case 1:
                        XmlDocument requestXml = new XmlDocument();

                        requestXml.Load("C:\\Users\\getPortStatus.xml");
                        stringXml = requestXml.OuterXml;
                        break;
                    case 2:
                        Console.Write("open할 bin number를 입력하세요. ");
                        binNumber = Console.ReadLine();
                        stringXml = createXmlRequest("openbin", "1", binNumber);
                        break;
                    case 3:
                        Console.Write("close할 bin number를 입력하세요. ");
                        binNumber = Console.ReadLine();
                        stringXml = createXmlRequest("closebin", "1", binNumber);                        
                        break;
                    case 4:
                        stringXml = createXmlRequest("openport", "1");
                        break;
                    case 5:
                        stringXml = createXmlRequest("closeport", "1");
                        break;
                    case 6:
                        stringXml = createXmlRequest("getbininfo");
                        break;
                    case 0:
                        break;
                }
                if(command.Equals(0))
                {
                    break;
                }

                StringContent stringContent = new StringContent(stringXml, Encoding.UTF8);
                var response = client.PostAsync(taskInterfaceUrl, stringContent).Result;
                Console.WriteLine("statusCode: " + response.StatusCode + " Content: " + response.Content);

                

                StreamReader returnStream = new StreamReader(response.Content.ReadAsStream());

                Console.WriteLine(returnStream.ReadToEnd());

            }
            
        }

        public static string createXmlRequest(string method)
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

        public static string createXmlRequest(string method, string port, string bin)
        {
            string xmlString = "";


            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xdoc.CreateXmlDeclaration("1.0","UTF-8","yes");
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

        public static string createXmlRequest(string method, string port)
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
