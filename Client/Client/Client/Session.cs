using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{

    public class Session
    {
        public static ServerConnection serverConnection = new ServerConnection();
        public static ServerStreamingConnection streamingServerConnection = new ServerStreamingConnection();
        public static Consumer consumer;
        public static Library library = new Library();
    }
}
