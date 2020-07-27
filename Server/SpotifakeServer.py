import sys
sys.path.append("gen-py")
from SpotifakeServer_ConsumerHandler import SpotifakeServerConsumerHandler
from SpotifakeServer_ContentCreatorHandler import SpotifakeServerContentCreatorHandler
from SpotifakeServer_TrackHandler import SpotifakeServerTrackHandler
from SpotifakeServer_AlbumHandler import SpotifakeServerAlbumHandler
from SpotifakeServer_PlaylistHandler import SpotifakeServerPlaylistHandler
from SpotifakeServer_LibraryHandler import SpotifakeServerLibraryHandler
from thrift.transport import TSocket
from thrift.server import TServer
from SpotifakeServices import ConsumerService
from SpotifakeServices import ContentCreatorService
from SpotifakeServices import TrackService
from SpotifakeServices import AlbumService
from SpotifakeServices import PlaylistService
from SpotifakeServices import LibraryService
from SpotifakeServices.ttypes import *
from Server.TMultiplexedProcessor import TMultiplexedProcessor

if __name__ == "__main__":

    processor: TMultiplexedProcessor = TMultiplexedProcessor()

    processor.registerProcessor("ConsumerService", ConsumerService.Processor(SpotifakeServerConsumerHandler()))
    processor.registerProcessor("ContentCreatorService", ContentCreatorService.Processor(SpotifakeServerContentCreatorHandler()))
    processor.registerProcessor("AlbumService", AlbumService.Processor(SpotifakeServerAlbumHandler()))
    processor.registerProcessor("TrackService", TrackService.Processor(SpotifakeServerTrackHandler()))
    processor.registerProcessor("PlaylistService", PlaylistService.Processor(SpotifakeServerPlaylistHandler()))
    processor.registerProcessor("LibraryService", LibraryService.Processor(SpotifakeServerLibraryHandler()))

    serverTransport = TSocket.TServerSocket(host="localhost", port=5000)
    server = TServer.TSimpleServer(processor, serverTransport)

    print("Starting service...")

    server.serve()
    
    print("Spotifake Service started.")