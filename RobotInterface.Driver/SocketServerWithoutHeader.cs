using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace RobotInterface.Driver
{
    public class SocketServerWithoutHeader
    {
        #region [Events]
        public event EventHandler<CEventArgs.ConnectionEventArgs>? OnClientConnectionChanged;
        public event EventHandler<CEventArgs.MessageReceivedArgs>? OnReceivedMessage;
        public event EventHandler<CEventArgs.InternalExceptionArgs>? OnExceptionThrown;
        #endregion

        #region [Static Variables]
        public List<string> Clients { get => _clients.Keys.ToList(); }
        Dictionary<string, TcpClient> _clients;
        TcpListener _listener;
        bool _listening = false;
        static int HeaderSize = 2;
        #endregion

        #region [Constructor]
        public SocketServerWithoutHeader(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _clients = new Dictionary<string, TcpClient>();
        }
        public SocketServerWithoutHeader(string ip, int port)
        {
            _listener = new TcpListener(IPAddress.Parse(ip), port);
            _clients = new Dictionary<string, TcpClient>();

        }
        #endregion

        #region [Public Methods]
        public void StartListening()
        {
            Task.Run(() =>
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
            });
        }
        public void StopListening()
        {
            try
            {
                _listening = false;
                _listener.Stop();
                foreach(var c in _clients.Values)
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
                byte[] arrayBytesRequest = new byte[1024];

                while (client.Client.Connected)
                {
                    int nRead = stream.Read(arrayBytesRequest, 0, arrayBytesRequest.Length);

                    string clientID = client.Client.RemoteEndPoint.ToString();

                    /*
                    // Read Header = Size of Message (2bytes)
                    byte[] headerBuffer = new byte[HeaderSize];
                    int readHeaderSize = stream.Read(headerBuffer, 0, HeaderSize);

                    if (readHeaderSize < 1)
                    {
                        Console.WriteLine("Client closed the connection.");
                        _clients.Remove(clientID);
                        stream.Close();
                    }
                    else if (readHeaderSize < HeaderSize)
                    {
                        stream.Read(headerBuffer, readHeaderSize, HeaderSize - readHeaderSize);
                    }

                    // Read Message (header size bytes)
                    short dataSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(headerBuffer));
                    Debug.WriteLine($"clientID : {clientID} dataSize : {dataSize}");

                    byte[] dataBuffer = new byte[dataSize];

                    int receivedDataSize = 0;

                    while(receivedDataSize < dataSize)
                    {
                        int nRead = stream.Read(dataBuffer, receivedDataSize, dataSize - receivedDataSize);
                        receivedDataSize += nRead;
                    }
                    string receivedMessage = Encoding.UTF8.GetString(dataBuffer);

                    CEventArgs.MessageReceivedArgs args = new CEventArgs.MessageReceivedArgs();
                    args.ClientID = clientID;
                    args.Message = receivedMessage;
                    HandleReceivedMessage(clientID, args);
                    */

                    
                    if(nRead > 0)
                    {
                        string sMsgRequest = Encoding.ASCII.GetString(arrayBytesRequest);
                        // Available은 데이터 Read 할 수 있는 byte 크기를 말한다.
                        Debug.WriteLine("nRead : " + nRead + " client.Available : " + client.Available + " arrayBytesRequest.Length : " + arrayBytesRequest.Length);

                        CEventArgs.MessageReceivedArgs args = new CEventArgs.MessageReceivedArgs();
                        args.ClientID = clientID;
                        args.Message = sMsgRequest;
                        HandleReceivedMessage(clientID, args);

                    }
                    
                }

            }
            catch( Exception ex )
            {
                HandleException(ex, "SocketServer.ReceiveMessage()");
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                foreach (var client in _clients)
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            byte[] dataBuffer = Encoding.ASCII.GetBytes(message);
                            //byte[] dataWithHeaderBuffer = new byte[HeaderSize + dataBuffer.Length];
                            //byte[] dataSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)dataBuffer.Length));
                           
                            //Array.Copy(dataSize, 0, dataWithHeaderBuffer, 0, dataSize.Length);
                            //Array.Copy(dataBuffer, 0, dataWithHeaderBuffer, HeaderSize, dataBuffer.Length);

                            client.Value.Client.SendBufferSize = dataBuffer.Length;
                            client.Value.Client.Send(dataBuffer);
                        }
                        catch (Exception)
                        {

                        }
                    });
                }
            }
            catch(Exception ex)
            {
                HandleException(ex, $"SocketServer.SendMessage({message})");
            }
        }
        public void SendMessage(int message)
        {
            try
            {
                foreach (var client in _clients)
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            //byte[] dataBuffer = BitConverter.GetBytes(message);
                            byte[] dataBuffer = BitConverter.GetBytes(IPAddress.NetworkToHostOrder(message));
                            //byte[] dataWithHeaderBuffer = new byte[HeaderSize + dataBuffer.Length];
                            //byte[] dataSize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)dataBuffer.Length));

                            //Array.Copy(dataSize, 0, dataWithHeaderBuffer, 0, dataSize.Length);
                            //Array.Copy(dataBuffer, 0, dataWithHeaderBuffer, HeaderSize, dataBuffer.Length);

                            client.Value.Client.SendBufferSize = dataBuffer.Length;
                            client.Value.Client.Send(dataBuffer);
                        }
                        catch (Exception)
                        {

                        }
                    });
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, $"SocketServer.SendMessage({message})");
            }
        }

        public void SendMessage(string clientID, string message)
        {
            try
            {
                if (_clients.ContainsKey(clientID))
                {
                    Console.WriteLine(BitConverter.GetBytes(1));
                    _clients[clientID].Client.Send(Encoding.ASCII.GetBytes(message));

                }
            }
            catch(Exception ex)
            {
                HandleException(ex, $"SendMessage({clientID}, {message})");
            }
        }
        public void SendMessage(string clientID, int message)
        {
            try
            {
                if (_clients.ContainsKey(clientID))
                {
                    byte[] dataBuffer = BitConverter.GetBytes(IPAddress.NetworkToHostOrder(message));
                    _clients[clientID].Client.Send(dataBuffer);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, $"SendMessage({clientID}, {message})");
            }
        }
        public void SendMessage(string clientID, byte[] buffer)
        {
            try
            {
                if (_clients.ContainsKey(clientID))
                {
                    _clients[clientID].Client.Send(buffer);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, $"SendMessage({clientID}, {buffer})");
            }
        }
        #endregion

        #region [EventMethods]
        private void HandleClientStatus(string clientId, CEventArgs.ConnectionEventArgs args)
        {
            EventHandler<CEventArgs.ConnectionEventArgs>? handler = OnClientConnectionChanged;
            handler?.Invoke(clientId, args);
        }
        private void HandleReceivedMessage(string clientId, CEventArgs.MessageReceivedArgs args)
        {
            EventHandler<CEventArgs.MessageReceivedArgs>? handler = OnReceivedMessage;
            handler?.Invoke(clientId, args);
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
            EventHandler<CEventArgs.InternalExceptionArgs>? handler = OnExceptionThrown;
            handler?.Invoke(methodName, args);
        }
        #endregion
    }
}