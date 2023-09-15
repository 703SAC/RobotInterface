using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RobotInterface.Driver
{
    public class AutostoreInterfaceDriver
    {
        private string autostoreInterfaceUrl = "http://192.168.0.233/AsInterfaceHttp/AutoStoreHttpInterface.aspx";
        
        public AutostoreInterfaceDriver(string interfaceType, string serverIP = "192.168.0.233")
        {
            if (interfaceType.Contains("bin"))
            {
                autostoreInterfaceUrl = "http://" + serverIP + "/AsInterfaceHttp/BinInterface.aspx";

            }
            else if (interfaceType.Contains("task"))
            {
                autostoreInterfaceUrl = "http://" + serverIP + "/AsInterfaceHttp/AutoStoreHttpInterface.aspx";
            }
        }

        public string sendAndReceiveWithAutostore(string xmlString)
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


        public string createXmlGetPortStatus()
        {
            string xmlString = "";
            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xdoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            xdoc.AppendChild(xmlDeclaration);

            XmlNode root = xdoc.CreateElement("methodcall");
            xdoc.AppendChild(root);

            XmlNode methodName = xdoc.CreateElement("name");
            methodName.InnerText = "getportstatus";
            root.AppendChild(methodName);

            XmlNode parameters = xdoc.CreateElement("params");
            root.AppendChild(parameters);

            XmlNode port_id = xdoc.CreateElement("port_id");
            port_id.InnerText = "1";
            parameters.AppendChild(port_id);

            xmlString = xdoc.OuterXml;

            Console.WriteLine(xmlString);

            return xmlString;
        }

        public string createXmlGetBinInfo(string method)
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
            minBinId.InnerText = "100000";
            XmlNode maxBinId = xdoc.CreateElement("max_bin_id");
            maxBinId.InnerText = "110000";

            parameters.AppendChild(minBinId);
            parameters.AppendChild(maxBinId);

            root.AppendChild(parameters);

            xdoc.Save(@"C:\xmlTest.xml");

            xmlString = xdoc.OuterXml;



            Console.WriteLine($"xmlString : {xmlString}");

            return xmlString;
        }

        public string createXmlOpenAndCloseBin(string method, string port, string bin)
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
        public string createXmlPrepareBin(string method, string port, string bin)
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
            XmlNode bins = xdoc.CreateElement("bins");



            XmlNode binId = xdoc.CreateElement("bin_id");
            binId.InnerText = bin;

            parameters.AppendChild(bins);
            bins.AppendChild(binId);

            root.AppendChild(parameters);

            xdoc.Save(@"C:\xmlTest.xml");

            xmlString = xdoc.OuterXml;



            Console.WriteLine($"xmlString : {xmlString}");

            return xmlString;
        }
        public string createXmlAppendPortQueue(string method, string port, string bin)
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
            parameters.AppendChild(portId);
            XmlNode bins = xdoc.CreateElement("bins");
            XmlNode binId = xdoc.CreateElement("bin_id");
            binId.InnerText = bin;

            parameters.AppendChild(bins);
            bins.AppendChild(binId);

            root.AppendChild(parameters);

            xdoc.Save(@"C:\xmlTest.xml");

            xmlString = xdoc.OuterXml;



            Console.WriteLine($"xmlString : {xmlString}");

            return xmlString;
        }

        public string createXmlOpenAndClosePort(string method, string port, bool isInsert = false)
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
            if (method.Equals("openport") && isInsert == false)
            {
                XmlNode select = xdoc.CreateElement("select");
                XmlNode category = xdoc.CreateElement("category");
                //select.InnerText = "100015";
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
