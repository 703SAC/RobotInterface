using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace RobotInterface.Driver
{
    public class SocketServer
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
        #region [Events]
        public event EventHandler<CEventArgs.ConnectionEventArgs> OnClientConnectionChanged;
        public event EventHandler<CEventArgs.MessageReceivedArgs> OnReceivedMessage;
        public event EventHandler<CEventArgs.InternalExceptionArgs> OnExceptionThrown;
        #endregion

        #region [Static Variables]
        public List<string> Clients { get => _clients.Keys.ToList(); }
        Dictionary<string, TcpClient> _clients;
        TcpListener _listener;
        bool _listening = false;
        #endregion

        #region [Constructor]
        public SocketServer(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _clients = new Dictionary<string, TcpClient>();
        }
        public SocketServer(string ip, int port)
        {
            _listener = new TcpListener(IPAddress.Parse(ip), port);
            _clients = new Dictionary<string, TcpClient>();

        }
        #endregion

        #region [Public Methods]
        public void StartListening()
        {
            try
            {
                _listener.Start();
                _listening = true;
                while (_listening)
                {
                    TcpClient client = _listener.AcceptTcpClient();
                    string clientId = client.Client.RemoteEndPoint.ToString();
                    _clients.Add(clientId, client);

                    CEventArgs.ConnectionEventArgs args = new CEventArgs.ConnectionEventArgs();
                    args.ClientID = clientId;
                    args.Connected = client.Connected;
                    HandleClientStatus(clientId, args);

                    ThreadPool.QueueUserWorkItem(ReceiveMessage, client);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "SocketServer.StartListening()");
            }
        }
        public void StopListening()
        {
            try
            {
                _listening = false;
                _listener.Stop();
                foreach(var c  in _clients.Values)
                {
                    try
                    {
                        c.Close();
                    }
                    catch(Exception ex)
                    {
                        HandleException(ex, "SocketServer.StopListening()");
                    }
                }
                _clients.Clear();
            }
            catch(Exception ex) 
            {
                HandleException(ex, "SocketServer.StopListening()");
            }
        }
        #endregion

        #region [Private Methods]
        private void ReceiveMessage(object state)
        {
            try
            {
                TcpClient client = (TcpClient)state;
                NetworkStream stream = client.GetStream();
                StringBuilder stringBuilder = new StringBuilder();

                while(client.Client.Connected)
                {
                    byte[] arrayBytesRequest = new byte[client.Available];

                    int nRead = stream.Read(arrayBytesRequest, 0, arrayBytesRequest.Length);

                    if(nRead > 0)
                    {
                        string sMsgReqeust = Encoding.ASCII.GetString(arrayBytesRequest);
                        Console.WriteLine("Received message request: " + sMsgReqeust);
                        string sMsgAnswer = string.Empty;

                    }
                }

            }
            catch( Exception ex )
            {
                HandleException(ex, "SocketServer.ReceiveMessage()");
            }
        }
        #endregion

        #region [EventMethods]
        private void HandleClientStatus(string clientId, CEventArgs.ConnectionEventArgs args)
        {
            EventHandler<CEventArgs.ConnectionEventArgs> handler = OnClientConnectionChanged;
            handler.Invoke(clientId, args);
        }
        private void HandleReceivedMessage(string clientId, CEventArgs.MessageReceivedArgs args)
        {
            EventHandler<CEventArgs.MessageReceivedArgs> handler = OnReceivedMessage;
            handler.Invoke(clientId, args);
        }
        private void HandleException(Exception ex, string methodName)
        {
            CEventArgs.InternalExceptionArgs args = new CEventArgs.InternalExceptionArgs
            {
                Exception = ex,
                MethodName = methodName
            };
            ThrowException(methodName, args);
        }
        private void ThrowException(string methodName, CEventArgs.InternalExceptionArgs args)
        {
            EventHandler<CEventArgs.InternalExceptionArgs> handler = OnExceptionThrown;
            handler.Invoke(methodName, args);
        }
        #endregion
    }
}