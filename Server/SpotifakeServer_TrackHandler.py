import sys
import os
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

    def GetTrackByTitle(self,title):
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

    def GetTrackByAlbumId(self, idAlbum):
        trackList = []
        trackFound =  SqlServerTrackManagement.GetTrackByIdAlbum(self, idAlbum)
        for n in trackFound:
            trackAux = Track()            
            trackAux.idTrack = n.IdTrack
            trackAux.durationSeconds = n.durationSeconds
            trackAux.title = n.title
            trackAux.trackNumber = n.trackNumber
            #trackAux.storagePath = n.storagePath
            #trackAux.gender = n.IdGenre
            trackList.append(trackAux)
        return trackList

    def GetTrackByPlaylistId(self,idPlaylist):
        trackList = []
        trackFound =  SqlServerTrackManagement.GetTrackByPlaylistId(self, idPlaylist)
        for n in trackFound:
            trackAux = Track()            
            trackAux.idTrack = n.IdTrack
            trackAux.durationSeconds = n.durationSeconds

            trackAux.title = n.title
            trackAux.trackNumber = n.trackNumber
            #trackAux.storagePath = n.storagePath
            #trackAux.gender = n.IdGenre
            trackList.append(trackAux)
        return trackList


    def GetTrackByLibraryId(self, idLibrary):
        trackList = []
        trackFound =  SqlServerTrackManagement.GetTrackByIdLibrary(self, idLibrary)
        for n in trackFound:
            trackAux = Track()            
            trackAux.idTrack = n.IdTrack
            trackAux.durationSeconds = n.durationSeconds
            trackAux.title = n.title
            trackAux.trackNumber = n.trackNumber
            trackAux.storagePath = n.storagePath
            trackAux.gender = n.IdGenre
            trackList.append(trackAux)
        return trackList

    def GetTrackByQuery(self, query):
        trackList = []
        trackFound =  SqlServerTrackManagement.GetTrackByQuery(self, query)
        if trackFound != 0:
                for n in trackFound:
                    track = Track(n.IdTrack, n.trackNumber, n.durationSeconds, n.storagePath, n.title)
                    track.title = n.title
                    track.stageName = n.ContentCreatorName
                    trackList.append(track)
                return trackList        
        return False

    def AddTrackToAlbum(self, idAlbum, newTrack, idContentCreator):
        idNewTrack = SqlServerTrackManagement.AddTrackToAlbum(self, idAlbum, newTrack)
        return idNewTrack

    def AddFeaturingTrack(self, idNewTrack, idContentCreator):
        SqlServerTrackManagement.AddFeaturingTrack(self, idNewTrack, idContentCreator)
        return idNewTrack

    def AddTrackToMedia(self, fileName, audio):
        file = open("../Media/Tracks/"+fileName+".mp3", 'wb')
        file.write(audio)
        file.close()
        return True

    def DeleteAlbumTrack(self,idAlbum, trackNumber):
        trackFound = SqlServerTrackManagement.DeleteAlbumTrack(self, idAlbum, trackNumber)
        return trackFound.idTrack

    def UpdateAlbumTrackTitle(self, idAlbum, trackNumber, newAlbumTrackTitle):
        trackFound = SqlServerTrackManagement.UpdateAlbumTrackTitle(self, idAlbum, trackNumber, newAlbumTrackTitle)
        return trackFound

    def AddTrackToLibrary(self, idLibrary, newTrack):
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

    def DeleteLibraryTrack(self,idLibrary, IdTrack):
        trackFound = SqlServerTrackManagement.DeleteLibraryTrack(self, idLibrary, idTrack)
        return trackFound.idTrack

    def AddTrackToPlaylist(self, idPlaylist, newTrack):
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

    def DeletePlaylistTrack(self,idPlaylist, IdTrack):
        trackFound = SqlServerTrackManagement.DeletePlaylistTrack(self, idPlaylist, idTrack)
        return trackFound.idTrack