using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Transport.Client;

namespace Client {

    public class ServerStreamingConnection {

        public StreamingService.Client streamingService;

        public ServerStreamingConnection() {
            try {
                TTransport transport = new TSocketTransport("localhost", 8000);
                TProtocol protocol = new TBinaryProtocol(transport);
                streamingService = new StreamingService.Client(protocol);
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }
    }
}
