using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Transport.Client;

namespace Client
{
    class ServerConnection
    {
        public ConsumerService.Client consumerService;
        public ContentCreatorService.Client contentCreatorService;

        public ServerConnection()
        {

            try
            {
                TTransport transport = new TSocketTransport("localhost", 5000);

                TBinaryProtocol protocol = new TBinaryProtocol(transport);

                TMultiplexedProtocol multiplexedProtocolConsumer = new TMultiplexedProtocol(protocol, "ConsumerService");
                consumerService = new ConsumerService.Client(multiplexedProtocolConsumer);

                TMultiplexedProtocol multiplexedProtocolContentCreator = new TMultiplexedProtocol(protocol, "ContentCreatorService");
                contentCreatorService = new ContentCreatorService.Client(multiplexedProtocolContentCreator);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + " in Session class");
            }
        }
    }
}
