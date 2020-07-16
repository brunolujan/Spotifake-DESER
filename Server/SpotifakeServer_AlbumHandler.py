import sys
import thriftpy
sys.path.append("../")
sys.path.append("gen-py")
from SpotifakeServices import AlbumService
from SpotifakeServices.ttypes import *
from SpotifakeManagement.ttypes import *
from SQLConnection.sqlServer_album import SqlServerAlbumManagement

class SpotifakeServerAlbumHandler(AlbumService.Iface):

    connection: SqlServerAlbumManagement = SqlServerAlbumManagement()
    spotifakeManagement_thrift = thriftpy.load('../Thrift/SpotifakeManagement.thrift', module_name='spotifakeManagement_thrift')
    spotifakeServices_thrift = thriftpy.load('../Thrift/SpotifakeServices.thrift', module_name='spotifakeServices_thrift')
    Album = spotifakeManagement_thrift.Album

    def __init__(self):
        pass

    def GetAlbumsByContentCreatorId(self, idContentCreator):
        albumList = []
        albumTuple = SqlServerAlbumManagement.GetAlbumsByContentCreatorId(self, idContentCreator)
        for n in len(albumTuple):
            albumAux = Album()
            pass
        return albumList