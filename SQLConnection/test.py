import sys
sys.path.append("../")
from SQLConnection.sqlServer_consumer import SqlServerConsumerManagement
from SQLConnection.sqlServer_contentCreator import SqlServerContentCreatorManagement
from SQLConnection.sqlServer_album import SqlServerAlbumManagement
from SQLConnection.sqlServer_track import SqlServerTrackManagement
from SQLConnection.sqlServer_playlist import SqlServerPlaylistManagement


#CONSUMER MANAGEMENT
query: SqlServerConsumerManagement = SqlServerConsumerManagement()

#query.GetConsumerById(2)

query.GetConsumerByEmailPassword("bechecita@bechecita.bechecita","bechecita")

#query.DeleteConsumer("bruno1529@live.com.mx")

#query.UpdateConsumerName("majohdezmol@gmail.com", "majojojo", "Majo", "Molinos")

#query.UpdateConsumerPassword("majohdezmol@gmail.com", "12345", "majojojo")



#CONTENT CREATOR MANAGEMENT
#query: SqlServerContentCreatorManagement = SqlServerContentCreatorManagement()

#query.GetContentCreatorById(5)

#query.DeleteContentCreator("winehouse@gmail.com")

#query.UpdateContentCreatorName("coldplay@gmail.com", "coldplay_22", "Chris", "Martin")

#query.UpdateContentCreatorPassword("coldplay@gmail.com", "coldplay_11", "coldplay_22")

#query.UpdateContentCreatorStageName("coldplay@gmail.com", "coldplay_22", "COLDPLAY")

#query.UpdateContentCreatorDescription("coldplay@gmail.com","coldplay_22", "My description has been updated")

#query.DeleteLibraryContentCreator(1,1)



#ALBUM MANAGEMENT
#query: SqlServerAlbumManagement = SqlServerAlbumManagement()

#query.GetAlbumByTitle("Parachutes")

#query.DeleteAlbum(2)

#query.UpdateAlbumTitle(1, "Head Full Of Dreams")

#query.DeleteLibraryAlbum(1,1)


#TRACK MANAGEMENT
#query: SqlServerTrackManagement = SqlServerTrackManagement()

#query.DeleteAlbumTrack(1, 2)

#query.UpdateAlbumTrackTitle(1,2, "Everglow")

#query.GetTrackByTitle("Everglow")

#query.DeleteLibraryTrack(1,2)

#query.DeletePlaylistTrack(1,2)



#PLAYLIST MANAGEMENT
#query: SqlServerPlaylistManagement = SqlServerPlaylistManagement()

#query.GetPlaylistByTitle("DESER")

#query.UpdatePlaylistTitle(1,"EEE")

#query.UpdatePlaylistDescription(1,"Ya me canséeeee")

#query.DeleteLibraryPlaylist(1,1)

