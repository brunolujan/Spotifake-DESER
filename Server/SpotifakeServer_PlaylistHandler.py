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
        playlistFound = SqlServerPlaylistManagement.GetPlaylistByTitle(self,title)
        playlistAux.idPlaylist = playlistFound.IdPlaylist
        playlistAux.name = playlistFound.name
        playlistAux.description = playlistFound.description
        date = Date()
        date.day = playlistFound.creationDate.day
        date.month = playlistFound.creationDate.month
        date.year = playlistFound.creationDate.year
        playlistAux.creationDate = date
        playlistAux.coverPath = playlistFound.coverPath
        return playlistFound

    def GetPlaylistByLibraryId(self, idLibrary):
        playlistList = []
        playlistFound = SqlServerPlaylistManagement.GetPlaylistByLibraryId(self, idLibrary)
        for n in playlistFoundFound:
            playlistAux.idPlaylist = n.IdPlaylist
            date = Date()
            date.day = n.creationDate.day
            date.month = n.creationDate.month
            date.year = n.creationDate.year
            playlistAux.creationDate = date
            playlistAux.name = n.title
            playlistAux.description = n.description
            playlistAux.coverPath = n.coverPath
            playlistList.append(playlistAux)
        return playlistList

    def AddPlaylistToLibrary(idLibrary, newPlaylist):
        playlistAux = Playlist()
        playlistFound = SqlServerPlaylistManagement.AddPLaylistToLibrary(self, idLibrary, newPlaylist)
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

    def DeleteLibraryPlaylist(self,idLibrary, idPlylist):
        playlistFound = SqlServerPlaylistManagement.DeleteLibraryPlaylist(self, idLibrary, idPlylist)
        return playlistFound.idPlylist

    def UpdatePLaylistTitle(self, idPLaylist, newPlaylistTitle):
        playlistFound = SqlServerPlaylistManagement.UpdatePlaylistTitle(self, idPLaylist, newPlaylistTitle)
        return playlistFound

    def UpdatePlaylistCover(self, idAlbum, newImageStoragePath):
        playlistFound = SqlServerPlaylistManagement.UpdatePlaylistCover(self, idPlylist, newImageStoragePath)
        return playlistFound

    def UpdatePlaylistDescription(self, idPLaylist, newDescription):
        playlistFound = SqlServerPlaylistManagement.UpdatePlaylistDescription(self, idPlylist, newDescription)
        return playlistFound