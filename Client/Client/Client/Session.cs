using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Session
    {
        public static ServerConnection serverConnection = new ServerConnection();
        public static StreamingService streamingServerConnection = new StreamingService();
    }
}
