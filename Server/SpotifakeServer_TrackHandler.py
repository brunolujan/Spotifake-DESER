import sys
import thriftpy
sys.path.append("../")
sys.path.append("gen-py")
from SpotifakeServices import TrackService
from SpotifakeServices.ttypes import *
from SpotifakeManagement.ttypes import *
from SQLConnection.sqlServer_track import SqlServerTrackManagement

class SpotifakeServerTrackHandler(TrackService.Iface):
    connection: SqlServerTrackManagement = SqlServerTrackManagement()
    spotifakeManagement_thrift = thriftpy.load('../Thrift/SpotifakeManagement.thrift', module_name='spotifakeManagement_thrift')
    spotifakeServices_thrift = thriftpy.load('../Thrift/SpotifakeServices.thrift', module_name='spotifakeServices_thrift')
    Date = spotifakeManagement_thrift.Date
    Track = spotifakeManagement_thrift.Track

    def __init__(self):
        pass

    def GetTrackByTitle(self,title): #Nuevo
        trackAux = Track()
        trackFound = SqlServerTrackManagement.GetTrackByTitle(self,title)
        trackAux = Track()
        trackAux.idTrack = trackFound.IdTrack
        trackAux.durationSeconds = trackFound.durationSeconds
        trackAux.title = trackFound.title
        trackAux.trackNumber = trackFound.trackNumber
        trackAux.storagePath = trackFound.storagePath
        trackAux.gender = trackFound.IdGenre
        return trackFound

    def GetTrackByAlbumId(self, idAlbum): #Nuevo
        trackList = []
        trackFound =  SqlServerTrackManagement.GetTrackByAlbumId(self, idAlbum)
        for n in trackFound:
            trackAux = Track()            
            trackAux.idTrack = n.IdTrack
            trackAux.durationSeconds = n.durationSeconds
            trackAux.title = n.title
            trackAux.trackNumber = n.trackNumber
            trackAux.storagePath = n.storagePath
            trackAux.gender = n.IdGenre
            trackList.append(trackAux)
        print(trackList)
        return trackList

    def AddTrackToAlbum(self, idAlbum, newTrack): #Nuevo
        idNewTrack = SqlServerTrackManagement.AddTrackToAlbum(self, idAlbum, newTrack)
        SqlServerTrackManagement.AddFeaturingTrack(self, idNewTrack, idContentCreator)
        return newTrack

    def DeleteAlbumTrack(self,idAlbum, trackNumber): #Nuevo
        trackFound = SqlServerTrackManagement.DeleteAlbumTrack(self, idAlbum, trackNumber)
        return trackFound.idTrack

    def UpdateAlbumTrackTitle(self, idAlbum, trackNumber, newAlbumTrackTitle): #Nuevo
        trackFound = SqlServerTrackManagement.UpdateAlbumTrackTitle(self, idAlbum, trackNumber, newAlbumTrackTitle)
        return trackFound

    def AddTrackToLibrary(self, idLibrary, newTrack): #Nuevo
        trackAux = Track()
        trackFound = SqlServerTrackManagement.AddTrackToLibrary(self, idLibrary, newTrack)
        trackAux = Track()
        trackAux.idTrack = trackFound.IdTrack
        trackAux.durationSeconds = trackFound.durationSeconds
        trackAux.title = trackFound.title
        trackAux.trackNumber = trackFound.trackNumber
        trackAux.storagePath = trackFound.storagePath
        trackAux.gender = trackFound.IdGenre
        return trackFound

    def DeleteLibraryTrack(self,idLibrary, IdTrack): #Nuevo
        trackFound = SqlServerTrackManagement.DeleteLibraryTrack(self, idLibrary, idTrack)
        return trackFound.idTrack

    def AddTrackToPlaylist(self, idPlaylist, newTrack): #Nuevo
        trackAux = Track()
        trackFound = SqlServerTrackManagement.AddTrackToPlaylist(self, idPlaylist, newTrack)
        trackAux = Track()
        trackAux.idTrack = trackFound.IdTrack
        trackAux.durationSeconds = trackFound.durationSeconds
        trackAux.title = trackFound.title
        trackAux.trackNumber = trackFound.trackNumber
        trackAux.storagePath = trackFound.storagePath
        trackAux.gender = trackFound.IdGenre
        return trackFound

    def DeletePlaylistTrack(self,idPlaylist, IdTrack): #Nuevo
        trackFound = SqlServerTrackManagement.DeletePlaylistTrack(self, idPlaylist, idTrack)
        return trackFound.idTrack