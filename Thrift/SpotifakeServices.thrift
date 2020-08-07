/*
*   Copyright 2020 Bruno Antonio López Luján & María José Hernández Molinos
*
*   Licensed under the Apache License, Version 2.0 (the "License");
*   you may not use this file except in compliance with the License.
*   You may obtain a copy of the License at
*
*   http://www.apache.org/licenses/LICENSE-2.0
*
*   Unless required by applicable law or agreed to in writing, software
*   distributed under the License is distributed on an "AS IS" BASIS,
*   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*   See the License for the specific language governing permissions and
*   limitations under the License.
*/

/**
 *  Application Programming Interface definition for Spotifake Services.
 *  this parent thrift file is contains all service interfaces. The data models are 
 *  described in respective thrift files.
*/

include "SpotifakeManagement.thrift"

/**
*   This file describes the services 
*   that needs to be passed to the API methods in order to 
*   manage Consumer and Content Creator users and Content. 
**/

service ConsumerService {

    /**
    *   Get Consumer by Id
    *
    *   @param idConsumer
    *       The Consumer Id to be obtained.
    *
    *   @return Consumer
    *       Consumer object  
    **/

    SpotifakeManagement.Consumer GetConsumerById(1: SpotifakeManagement.Id idConsumer) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *   Get Consumer by email
    *
    *   @param email
    *       The Consumer email to be obtained.
    *
    *   @return bool
    *       bool object  
    **/

    bool GetConsumerByEmail(1: string email) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)


        /**
    *   Get Consumer by email and password
    *
    *   @param email
    *       The Consumer email to be obtained.
    *   @param password
    *       The Consumer password to be obtained.

    *   @return Consumer
    *       Consumer object  
    **/

    SpotifakeManagement.Consumer GetConsumerByEmailPassword(1: string email, 2: string password) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *   Register a Consumer.
    *
    *   @param newconsumer
    * 
    *   @return Consumer
    *       Consumer object added
    **/

    SpotifakeManagement.Consumer AddConsumer(1: SpotifakeManagement.Consumer newConsumer) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE)

    /**
    *   Delete a Consumer
    *
    *   @param email
    *       The Consumer email of the Consumer to be deleted.
    *
    *   @return Id
    *       The Consumer Id of the Consumer deleted.
    **/

    SpotifakeManagement.Id DeleteConsumer(1: string email) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE, 
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *    
    *   Update previously registered Consumer name.
    *
    *   @param email
    *       The Consumer Email of the Consumer which require an update name.
    *
    *   @return Consumer
    *       Modified Consumer obejct.
    **/

    SpotifakeManagement.Consumer UpdateConsumerName(1: string email, 2: string currentPassword, 3: string newName, 4: string newLastName) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        4: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *    
    *   Update previously registered Consumer password.
    *
    *   @param email
    *       The Consumer Email of the Consumer which require an update password.
    *
    *   @return Consumer
    *       Modified Consumer obejct.
    **/

    SpotifakeManagement.Consumer UpdateConsumerPassword(1: string email, 2: string currentPassword, 3: string newPassword) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        4: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *    
    *   Update previously registered Consumer image.
    *
    *   @param email
    *       The Consumer Email of the Consumer which require an update image.
    *
    *   @return Consumer
    *       Modified Consumer obejct.
    **/

    bool UpdateConsumerImage(1: string email, 2: string fileName) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        4: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *   Allows the login of a consumer
    *
    *   @param email
    *       The Consumer email
    *
    *   @param password 
    *       The Email password of the consumer
    *
    *   @return Consumer
    *       Consumer object
    **/

    SpotifakeManagement.Consumer LoginConsumer(1: string email, 2: string password) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE,
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Add image file binary
    *
    *   @param binary image
    *       The binary number that will be keep.
    *
    *   @return bool
    *       true or false.
    **/

    bool AddImageToMedia(1:string fileName, 2:binary image) throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)


    /**
    *   Get image file binary
    *
    *   @param binary image
    *       The binary number that will be keep.
    *
    *   @return binary
    *       binary image.
    **/

    binary GetImageToMedia(1:string fileName) throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Delete image file binary
    *
    *   @param fileName
    *       The fileName of file that will be delete.
    *
    *   @return bool
    *       True or False
    **/

    bool DeleteImageToMedia(1:string fileName) throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)
}

service ContentCreatorService {

    /**
    *   Get ContentCreator
    *
    *   @return list<ContentCreator>
    *       ContentCreator list  
    **/

    list<SpotifakeManagement.ContentCreator> GetContentCreators()
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *   Get ContentCreator by Id
    *
    *   @param idContentCreator
    *       The ContentCreator Id to be obtained.
    *
    *   @return ContentCreator
    *       ContentCreator object  
    **/

    SpotifakeManagement.ContentCreator GetContentCreatorById(1: SpotifakeManagement.Id idContentCreator)
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *   Get ContentCreator by Library Id
    *
    *   @param idLibrary
    *       The Library Id to be obtained.
    *
    *   @return ContentCreator list
    *       list<ContentCreator>  
    **/

    list<SpotifakeManagement.ContentCreator> GetContentCreatorByLibraryId(1: SpotifakeManagement.Id idLibrary)
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *   Get ContentCreator by email
    *
    *   @param email
    *       The ContentCreator email to be obtained.
    *
    *   @return bool
    *       bool object  
    **/

    bool GetContentCreatorByEmail(1: string email) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *   Get ContentCreator by email
    *
    *   @param email
    *       The ContentCreator email to be obtained.
    *
    *   @return bool
    *       bool object  
    **/

    bool GetContentCreatorByStageName(1: string email) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *   Register a Content Creator.
    *
    *   @param newContentCreator
    * 
    *   @return ContentCreator
    *       ContentCreator object added
    **/

    SpotifakeManagement.ContentCreator AddContentCreator(1:SpotifakeManagement.ContentCreator newContentCreator) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE)

    /**
    *   Delete a ContentCreator
    *
    *   @param email
    *       The Content Creator email of the Content Creator to be deleted.
    *
    *   @return Id
    *       The Content Creator Id of the Content Creator deleted.
    **/
    
    SpotifakeManagement.Id DeleteContentCreator(1: string email)
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *    
    *   Update previously registered Content Creator name.
    *
    *   @param email
    *       The Content Creator Email of the Consumer which require an update name.
    *
    *   @return ContentCreator
    *       Modified Content Creator obejct.
    **/

    SpotifakeManagement.ContentCreator UpdateContentCreatorName(1: string email, 2: string currentPassword, 3: string newName, 4: string newLastName) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        4: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *    
    *   Update previously registered Content Creator password.
    *
    *   @param email
    *       The Content Creator Email of the Consumer which require an update password.
    *
    *   @return ContentCreator
    *       Modified Content Creator obejct.
    **/

    SpotifakeManagement.ContentCreator UpdateContentCreatorPassword(1: string email, 2: string currentPassword, 3: string newPassword) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        4: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)
   
    /**
    *    
    *   Update previously registered Content Creator image.
    *
    *   @param email
    *       The Content Creator Email of the Consumer which require an update image.
    *
    *   @return bool
    *       True or False
    **/

    bool UpdateContentCreatorImage(1: string email, 2: string fileName) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        4: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)
   
    /**
    *    
    *   Update previously registered Content Creator stage name.
    *
    *   @param email
    *       The Content Creator Email of the Consumer which require an update stage name.
    *
    *   @return ContentCreator
    *       Modified Content Creator obejct.
    **/

    SpotifakeManagement.ContentCreator UpdateContentCreatorStageName(1: string email, 2: string currentPassword, 3: string newStageName) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        4: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)
   
    /**
    *    
    *   Update previously registered Content Creator description.
    *
    *   @param email
    *       The Content Creator Email of the Consumer which require an update description.
    *
    *   @return ContentCreator
    *       Modified Content Creator obejct
    **/

    SpotifakeManagement.ContentCreator UpdateContentCreatorDescription(1: string email, 2: string currentPassword, 3: string newDescription) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE, 2: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        3: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        4: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *   Allows the login of a content creator
    *
    *   @param email
    *       The Conntent Creator email
    *
    *   @param password 
    *       The Email password of the content creator
    *
    *   @return Content Creator
    *       Content Creator object
    **/

    SpotifakeManagement.ContentCreator LoginContentCreator(1: string email, 2: string password) 
        throws (1: SpotifakeManagement.SErrorUserException sErrorUserE,
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Add a ContentCreator to Library.
    *
    *   @param idLibrary
    *       The Library Id to which a content creator will be added
    * 
    *   @param newContentCreator
    *
    *   @return ContentCreator
    *       ContentCreator object added
    **/
    bool AddContentCreatorToLibrary(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.Id idContenCreator) 
        throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)
    
    /**
    *   Delete a Content Creator from a Library
    *
    *   @param idLibrary
    *       The Library Id which a content creator will be deleted.
    *
    *   @param idContentCreator
    *       The Content Creator Id which will be deleted
    *
    *   @return Id
    *       The Content Creator Id of the Content Creator deleted.
    **/

    SpotifakeManagement.Id DeleteLibraryContentCreator(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.Id idContentCreator) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Get ContentCreator by Query
    *
    *   @param query
    *       The query to be obtained
    *
    *   @return ContentCreator
    *       list<contentCreator> 
    **/
    list<SpotifakeManagement.ContentCreator> GetContentCreatorByQuery(1: string query) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Add image file binary
    *
    *   @param binary image
    *       The binary number that will be keep.
    *
    *   @return bool
    *       true or false.
    **/

    bool AddImageToMedia(1:string fileName, 2:binary image) throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)


    /**
    *   Get image file binary
    *
    *   @param binary image
    *       The binary number that will be keep.
    *
    *   @return binary
    *       binary image.
    **/

    binary GetImageToMedia(1:string fileName) throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Delete image file binary
    *
    *   @param fileName
    *       The fileName of file that will be delete.
    *
    *   @return bool
    *       True or False
    **/

    bool DeleteImageToMedia(1:string fileName) throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)
}

service TrackService {

    /**
    *   Get Track by Title
    *
    *   @param title
    *       The Track Title to be obtained
    *
    *   @return Track
    *       Track object 
    **/

    SpotifakeManagement.Track GetTrackByTitle(1: string title) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Get Track by idAlbum
    *
    *   @param idAlbum
    *       The Track Title to be obtained
    *
    *   @return Track
    *       list<Track>
    **/

    list<SpotifakeManagement.Track> GetTrackByAlbumId(1: SpotifakeManagement.Id idAlbum) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Get Track by idAlbum
    *
    *   @param idAlbum
    *       The Track Title to be obtained
    *
    *   @return Track
    *       list<Track>
    **/

    list<SpotifakeManagement.Track> GetTrackByPlaylistId(1: SpotifakeManagement.Id idPlaylist) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Get Track by idLibrary
    *
    *   @param idLibrary
    *       The Library Id to be obtained
    *
    *   @return Track
    *       list<Track>
    **/

    list<SpotifakeManagement.Track> GetTrackByLibraryId(1: SpotifakeManagement.Id idLibrary) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Add a Track to an Album.
    *
    *   @param idAlbum
    *       The Album Id which a track will be added
    * 
    *   @param newTrack 
    *
    *   @return Track
    *       Track object added
    **/

    SpotifakeManagement.Id AddTrackToAlbum(1: SpotifakeManagement.Id idAlbum, 2: SpotifakeManagement.Track newTrack, 3: SpotifakeManagement.Id idContentCreator) 
        throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    * Register a featuring Track
    *
    * @param newTrack
    * 
    * @return idNewTrack
    *   Featuring added
    **/
    
    SpotifakeManagement.Id AddFeaturingTrack(1: SpotifakeManagement.Id idNewTrack, 2:SpotifakeManagement.Id idContenCreator) 
        throws (1: SpotifakeManagement.SErrorSystemException sErrorSystemE)  

    /**
    *   Delete a Track from an Album
    *
    *   @param idAlbum
    *       The Album Id which a track will be deleted.
    *
    *   @param trackNumber
    *       The Track number which will be deleted
    *
    *   @return Id
    *       The Track Id of the Track deleted.
    **/

    SpotifakeManagement.Id DeleteAlbumTrack(1: SpotifakeManagement.Id idAlbum, 2: SpotifakeManagement.Int trackNumber) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *   Get Track by Query
    *
    *   @param query
    *       The query to be obtained
    *
    *   @return Track
    *       list<Track>
    **/

    list<SpotifakeManagement.Track> GetTrackByQuery(1: string query) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *    
    *   Update previously registered Album track title.
    *
    *   @param idAlbum
    *       The Album Id of the Album which require an update track title.
    *
    *   @param trackNumber
    *       The Track number of the Track which require an update title
    *
    *   @return Album
    *       Modified Album obejct.
    **/

    SpotifakeManagement.Track UpdateAlbumTrackTitle(1: SpotifakeManagement.Id idAlbum, 2: SpotifakeManagement.Int trackNumber, 3: string newAlbumTrackTitle) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)
   
       /**
    *    
    *   Update previously registered Album track featuring.
    *
    *   @param idAlbum
    *       The Album Id of the Album which require an update track featuring.
    *
    *   @param trackNumber
    *       The Track number of the Track which require an update featuring
    *
    *   @return Album
    *       Modified Album obejct.
    **/

    SpotifakeManagement.Track UpdateAlbumTrackFeaturing(1: SpotifakeManagement.Id idAlbum, 2: SpotifakeManagement.Int trackNumber, 3: SpotifakeManagement.ContentCreator newFeaturing) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *   Add a Track to Library.
    *
    *   @param idLibrary
    *       The Library Id to which a track will be added
    * 
    *   @param newTrack 
    *
    *   @return Track
    *       Track object added
    **/

    bool AddTrackToLibrary(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.Id idTrack) 
        throws (1: SpotifakeManagement.SErrorSystemException sErrorSystemE)
    
    /**
    *   Delete a Track from a Library
    *
    *   @param idLibrary
    *       The Library Id which a track will be deleted.
    *
    *   @param trackNumber
    *       The Track number which will be deleted
    *
    *   @return Id
    *       The Track Id of the Track deleted.
    **/
    
    SpotifakeManagement.Id DeleteLibraryTrack(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.Int trackNumber) #CAMBIAR POR IDTRACK
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Add a Track to Playlist.
    *
    *   @param idPlaylist
    *       The Playlist Id to which a track will be added
    * 
    *   @param newTrack 
    *
    *   @return Track
    *       Track object added
    **/

    bool AddTrackToPlaylist(1: SpotifakeManagement.Id idPlaylist, 2: SpotifakeManagement.Id idTrack) 
        throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Delete a Track from a Playlist
    *
    *   @param idPlaylist
    *       The Playlist Id which a track will be deleted.
    *
    *   @param trackNumber
    *       The Track number which will be deleted
    *
    *   @return Id
    *       The Track Id of the Track deleted.
    **/

    SpotifakeManagement.Id DeletePlaylistTrack(1: SpotifakeManagement.Id idPlaylist, 2: SpotifakeManagement.Int trackNumber) #CAMBIAR POR IDTRACK
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Add a Track to PlayQueue.
    *
    *   @param idPlayQueue
    *       The PlayQueue Id to which a track will be added
    * 
    *   @param newTrack 
    *
    *   @return Track
    *       Track object added
    **/

    SpotifakeManagement.Track AddTrackToPlayQueue(1: SpotifakeManagement.Id idPlayQueu, 2: SpotifakeManagement.Track newTrack) 
        throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Delete a Track from a PlayQueue
    *
    *   @param idPlayQueue
    *       The PlayQueue Id which a track will be deleted.
    *
    *   @param trackNumber
    *       The Track number which will be deleted
    *
    *   @return Id
    *       The Track Id of the Track deleted.
    **/
   
    SpotifakeManagement.Id DeletePlayQueueTrack(1: SpotifakeManagement.Id idPlayQueu, 2: SpotifakeManagement.Int trackNumber) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Generate a Radio Station
    *
    *   @param gender
    *       The gender which the radio station will be generated.
    *
    *   @return tracks
    *       List of tracks which belong to the gender entered.
    **/

    list<SpotifakeManagement.Track> GenerateRadioStation(1: SpotifakeManagement.MusicGender gender) 
         throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)


    /**
    *   Get Local Tracks By Id Consumer.
    *
    *   @param idConsumer
    *       The Consumer Id which is required to get Tracks
    *
    *   @return LocalTracks
    *       List of tracks which belong to idConsumer
    **/

    list<SpotifakeManagement.LocalTrack> GetLocalTracksByIdConsumer(1: SpotifakeManagement.Id idConsumer)
        throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)


    /**
    *   Add Local Track.
    *
    *   @param LocalTrack
    *       The Local Track which will be added
    *
    *    @return LocalTracks
    *       List of tracks which belong to idConsumer
    **/

    bool AddLocalTrack(1: SpotifakeManagement.LocalTrack LocalTrack)
        throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)
}

service AlbumService {

    /**
    *   Get Album by Title
    *
    *   @param title
    *       The Album Title to be obtained
    *
    *   @return Album
    *       Album object 
    **/

    SpotifakeManagement.Album GetAlbumByTitle(1: string title) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *   Get list of Track from Content creator by idContentCreator.
    *
    *   @param idContentCreator
    *       The ContentCreator Id which a track will be added
    *
    *   @return list<Album>
    *       Album found by idContenCreator
    **/

    list<SpotifakeManagement.Album> GetAlbumsByContentCreatorId(1: SpotifakeManagement.Id idContentCreator)
    throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Get list of Track from Content creator by idContentCreator.
    *
    *   @param idContentCreator
    *       The ContentCreator Id which a track will be added
    *
    *   @return list<String>
    *       Album found by idContenCreator
    **/

    list<SpotifakeManagement.Album> GetSinglesByContentCreatorId(1: SpotifakeManagement.Id idContentCreator)
    throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Get list of Album from Library by idLibrary.
    *
    *   @param idLibrary
    *       The Library Id
    *
    *   @return list<Album>
    *       Album found by idLibrary
    **/

    list<SpotifakeManagement.Album> GetAlbumByLibraryId(1: SpotifakeManagement.Id idLibrary)
    throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    * Register an Album.
    *
    * @param newAlbum
    * 
    * @return idNewAlbum
    *   Album object added
    **/
    
    SpotifakeManagement.Id AddAlbum(1: SpotifakeManagement.Album newAlbum, 2:SpotifakeManagement.Id idContenCreator) 
        throws (1: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    * Register a featuring Album.
    *
    * @param newAlbum
    * 
    * @return idNewAlbum
    *   Featuring added
    **/
    
    SpotifakeManagement.Id AddFeaturingAlbum(1: SpotifakeManagement.Id idNewAlbum, 2:SpotifakeManagement.Id idContenCreator) 
        throws (1: SpotifakeManagement.SErrorSystemException sErrorSystemE)  

    /**
    *   Delete a Album
    *
    *   @param idAlbum
    *       The Album Id of the Album to be deleted.
    *
    *   @return Id
    *       The Album Id of the Album deleted.
    **/

    SpotifakeManagement.Id DeleteAlbum(1: SpotifakeManagement.Id idAlbum) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE,
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *    
    *   Update previously registered Album title.
    *
    *   @param idAlbum
    *       The Album Id of the Album which require an update title.
    *
    *   @return Album
    *       Modified Album obejct.
    **/

    SpotifakeManagement.Album UpdateAlbumTitle(1: SpotifakeManagement.Id idAlbum, 2: string newAlbumTitle) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)
    
    /**
    *    
    *   Update previously registered Album cover.
    *
    *   @param idAlbum
    *       The Album Id of the Album which require an update cover.
    *
    *   @return Album
    *       Modified Album obejct.
    **/

    SpotifakeManagement.Album UpdateAlbumCover(1: SpotifakeManagement.Id idAlbum, 2: string newCoverStoragePath) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)
    

    /**
    *   Add an Album to Library.
    *
    *   @param idLibrary
    *       The Library Id to which an album will be added
    * 
    *   @param newAlbum 
    *
    *   @return Album
    *       Album object added
    **/
    bool AddAlbumToLibrary(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.Id idAlbum) 
         throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)
    
    /**
    *   Delete an Album from a Library
    *
    *   @param idLibrary
    *       The Library Id which an album will be deleted.
    *
    *   @param idAlbum
    *       The Album Id which will be deleted
    *
    *   @return Id
    *       The Album Id of the Album deleted.
    **/
    
    SpotifakeManagement.Id DeleteLibraryAlbum(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.Id idAlbum) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Get Album by Query
    *
    *   @param query
    *       The query to be obtained
    *
    *   @return Album
    *       list<Album>
    **/

    list<SpotifakeManagement.Album> GetAlbumByQuery(1: string query) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Add image file binary
    *
    *   @param binary image
    *       The binary number that will be keep.
    *
    *   @return bool
    *       true or false.
    **/

    bool AddImageToMedia(1:string fileName, 2:binary image) throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Get image file binary
    *
    *   @param binary image
    *       The binary number that will be keep.
    *
    *   @return binary
    *       binary image.
    **/

    binary GetImageToMedia(1:string fileName) throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)
}

service PlaylistService {

    /**
    * Register a Playlist
    *
    * @param newPlaylist
    * 
    * @return bool
    *   True or False
    **/
    
    bool AddPlaylist(1: SpotifakeManagement.Playlist newPlaylist, 2:SpotifakeManagement.Id idConsumer) 
        throws (1: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Get Playlist by Title
    *
    *   @param title
    *       The Playlist Title to be obtained
    *
    *   @return Playlist
    *       Playlist object 
    **/

    SpotifakeManagement.Playlist GetPlaylistByTitle(1: string title) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystem,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *   Get Playlist by idLibrary
    *
    *   @param idLibrary
    *       The Library Id to be obtained
    *
    *   @return Playlist list
    *       list<Playlist> 
    **/

    list<SpotifakeManagement.Playlist> GetPlaylistByLibraryId(1: SpotifakeManagement.Id idLibrary) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystem,
        3: SpotifakeManagement.SErrorInvalidRequestException sErrorInvalidRequestE)

    /**
    *   Add a Playlist to Library.
    *
    *   @param idLibrary
    *       The Library Id to which a playlist will be added
    * 
    *   @param newPlaylist
    *
    *   @return Playlist
    *       Playlist object added
    **/

    bool AddPlaylistToLibrary(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.Id idPlaylist) 
        throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)
    
    /**
    *   Delete a Playlist from a Library
    *
    *   @param idLibrary
    *       The Library Id which a playlist will be deleted.
    *
    *   @param idPlaylist
    *       The Playlist Id which will be deleted
    *
    *   @return Id
    *       The Playlist Id of the Playlist deleted.
    **/

    SpotifakeManagement.Id DeleteLibraryPlaylist(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.Id idPlaylist) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *    
    *   Update previously registered Playlist title.
    *
    *   @param playlistId
    *       The Playlist Id of the Playlist which require an update title.
    *
    *   @return Playlist
    *       Modified Playlist obejct.
    **/

    SpotifakeManagement.Playlist UpdatePlaylistTitle(1: SpotifakeManagement.Id idPlaylist, 2: string newPlaylistTitle) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *    
    *   Update previously registered Playlist cover.
    *
    *   @param playlistId
    *       The Playlist Id of the Playlist which require an update cover.
    *
    *   @return Playlist
    *       Modified Playlist obejct.
    **/

    SpotifakeManagement.Playlist UpdatePlaylistCover(1: SpotifakeManagement.Id idPlaylist, 2: string newImageStoragePath) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *    
    *   Update previously registered Playlist description.
    *
    *   @param playlistId
    *       The Playlist Id of the Playlist which require an update description.
    *
    *   @return Playlist
    *       Modified Playlist obejct.
    **/

    SpotifakeManagement.Playlist UpdatePlaylistDescription(1: SpotifakeManagement.Id idPlaylist, 2: string newDescription) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Get Playlist by Query
    *
    *   @param query
    *       The query to be obtained
    *
    *   @return Playlist
    *       list<Playlist>
    **/

    list<SpotifakeManagement.Playlist> GetPlaylistByQuery(1: string query) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Add image file binary
    *
    *   @param binary image
    *       The binary number that will be keep.
    *
    *   @return bool
    *       true or false.
    **/

    bool AddImageToMedia(1:string fileName, 2:binary image) throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Get image file binary
    *
    *   @param binary image
    *       The binary number that will be keep.
    *
    *   @return binary
    *       binary image.
    **/

    binary GetImageToMedia(1:string fileName) throws (1:SpotifakeManagement.SErrorSystemException sErrorSystemE)
}

service LibraryService {

    /**
    * Register a Library.
    *
    * @param idConsumer
    * 
    * @return bool
    *   True or False
    **/

    bool AddLibrary(1: SpotifakeManagement.Id idConsumer) throws (1: SpotifakeManagement.SErrorSystemException sErrorSystemE)

    /**
    *   Get Library
    *
    *   @param idConsumer
    *       idConsumer
    *
    *   @return idLibrary
    *       idLibrary    
    **/

    SpotifakeManagement.Id getLibraryByIdConsumer(1: SpotifakeManagement.Id idConsumer) 
        throws (1: SpotifakeManagement.SErrorNotFoundException sErrorNotFoundE, 
        2:SpotifakeManagement.SErrorSystemException sErrorSystemE)
}