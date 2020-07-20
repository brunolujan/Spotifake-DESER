import sys
import thriftpy
sys.path.append("../")
sys.path.append("gen-py")
from SpotifakeServices import PlaylistService
from SpotifakeServices.ttypes import *
from SpotifakeManagement.ttypes import *
from SQLConnection.sqlServer_playlist import SqlServerPlaylistManagement

class SpotifakeServerPlaylistHandler(PlaylistService.Iface):
    connection: SqlServerPlaylistManagement = SqlServerPlaylistManagement()
    spotifakeManagement_thrift = thriftpy.load('../Thrift/SpotifakeManagement.thrift', module_name='spotifakeManagement_thrift')
    spotifakeServices_thrift = thriftpy.load('../Thrift/SpotifakeServices.thrift', module_name='spotifakeServices_thrift')
    Date = spotifakeManagement_thrift.Date
    Playlist = spotifakeManagement_thrift.Track

    def __init__(self):
        pass

    def GetPlaylistByTitle(self,title):
        playlistAux = Playlist()
        playlistFound = SqlServerAlbumManagement.GetPlaylistByTitle(self,title)
        playlistAux = Playlist()
        playlistAux.idPlylist = playlistFound.IdPlaylist
        playlistAux.name = playlistFound.name
        playlistAux.description = playlistFound.description
        date = Date()
        date.day = playlistFound.creationDate.day
        date.month = playlistFound.creationDate.month
        date.year = playlistFound.creationDate.year
        playlistAux.creationDate = date
        playlistAux.coverPath = playlistFound.coverPath
        return playlistFound
