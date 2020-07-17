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
    Date = spotifakeManagement_thrift.Date
    Album = spotifakeManagement_thrift.Album

    def __init__(self):
        pass

    def GetAlbumsByContentCreatorId(self, idContentCreator):
        albumList = []
        albumFound =  SqlServerAlbumManagement.GetAlbumsByContentCreatorId(self, idContentCreator)
        for n in albumFound:
            albumAux = Album()            
            albumAux.idAlbum = n.IdAlbum
            albumAux.title = n.title
            albumAux.coverPath = n.coverPath
            date = Date()
            date.day = n.releaseDate.day
            date.month = n.releaseDate.month
            date.year = n.releaseDate.year
            albumAux.releaseDate = date;
            albumAux.gender = n.IdGenre
            albumAux.isSingle = n.type
            albumList.append(albumAux)
        print(albumList)
        return albumList

    def GetSinglesByContentCreatorId(self, idContentCreator):
        albumList = []
        albumFound =  SqlServerAlbumManagement.GetSinglesByContentCreatorId(self, idContentCreator)
        for n in albumFound:
            albumAux = Album()
            albumAux.idAlbum = n.IdAlbum
            albumAux.title = n.title
            albumAux.coverPath = n.coverPath
            date = Date()
            date.day = n.releaseDate.day
            date.month = n.releaseDate.month
            date.year = n.releaseDate.year
            albumAux.releaseDate = date
            albumAux.gender = n.IdGenre
            albumAux.isSingle = n.type
            albumList.append(albumAux)
            print(albumList)
        return albumList

    def AddAlbum(self, newAlbum):
        albumFound =  SqlServerAlbumManagement.AddAlbum(self, newAlbum)
        return newAlbum