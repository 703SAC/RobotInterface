using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotInterface.Driver
{
    public class CEventArgs
    {
        public class ConnectionEventArgs : EventArgs
        {
            public string ClientID { get; set; } = "";
            public bool Connected { get; set; }
            public object[] Args { get { return new object[] { ClientID, Connected }; } }
        }
        public class MessageReceivedArgs : EventArgs
        {
            public string ClientID { get; set; } = "";
            public string Message { get; set; } = "";
            public object[] Args { get { return new object[] { ClientID, Message }; } }
        }
        public class InternalExceptionArgs : EventArgs
        {
            public Exception Exception { get; set; } = new Exception();
            public string MethodName { get; set; } = "";
            public string Remark { get; set; } = "";
            public object[] Args { get { return new object[] { Exception, MethodName }; } }
        }
    }
}
