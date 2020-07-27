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
        public AlbumService.Client albumService;
        public TrackService.Client trackService;
        public PlaylistService.Client playlistService;
        public LibraryService.Client libraryService;

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

                TMultiplexedProtocol multiplexeProtocolAlbum = new TMultiplexedProtocol(protocol, "AlbumService");
                albumService = new AlbumService.Client(multiplexeProtocolAlbum);

                TMultiplexedProtocol multiplexeProtocolTrack = new TMultiplexedProtocol(protocol, "TrackService");
                trackService = new TrackService.Client(multiplexeProtocolTrack);

                TMultiplexedProtocol multiplexedProtocolPlaylist = new TMultiplexedProtocol(protocol, "PlaylistService");
                playlistService = new PlaylistService.Client(multiplexedProtocolPlaylist);

                TMultiplexedProtocol multiplexeProtocolLibrary = new TMultiplexedProtocol(protocol, "LibraryService");
                libraryService = new LibraryService.Client(multiplexeProtocolLibrary);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + " in Session class");
            }
        }
    }
}
