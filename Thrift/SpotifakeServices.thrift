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

service UserManagement {
    #Consultar usuario
    Consumer GetConsumerById(1: SpotifakeManagement.Id idConsumer)
    ContentCreator GetContentCreatorById(1: SpotifakeManagement.Id idContentCreator)

    #Agregar usuario
    Consumer AddConsumer(1: SpotifakeManagement.Consumer newConsumer) throws (1: ExceptionUser eu)
    ContentCreator AddConContentCreator(1:SpotifakeManagement.ContentCreator newContentCreator) throws (1:ExceptionUser eu)

    #Eliminar usuario
    Id DeleteConsumer(1: string email) throws (1: ExceptionUser eu)
    Id DeleteContentCreator(1: string email) throws (1: ExceptionUser eu)

    #Actualizar usuario - Consumer
    Consumer UpdateConsumerName(1: string email, 2: string currentPassword, 3: string newName, 4: string newLastName) throws (1: ExceptionUser eu)
    Consumer UpdateConsumerPassword(1: string email, 2: string currentPassword, 3: string newPassword) throws (1: ExceptionUser eu)
    Consumer UpdateConsumerImage(1: string email, 2: string newImageStoragePath) throws (1: ExceptionUser eu)

    #Actualizar usuario - ContentCreator
    ContentCreator UpdateContentCreatorName(1: string email, 2: string currentPassword, 3: string newName, 4: string newLastName) throws (1: ExceptionUser eu)
    ContentCreator UpdateContentCreatorPassword(1: string email, 2: string currentPassword, 3: string newPassword) throws (1: ExceptionUser eu)
    ContentCreator UpdateContentCreatorImage(1: string email, 2: string newImageStoragePath) throws (1: ExceptionUser eu)
    ContentCreator UpdateContentCreatorStageName(1: string email, 2: string currentPassword, 3: string newStageName) throws (1: ExceptionUser eu)
    ContentCreator UpdateContentCreatorDescription(1: string email, 2: string currentPassword, 3: string newDescription) throws (1: ExceptionUser eu)

    #Iniciar sesión
    bool isConsumer(string email) throws (1: ExceptionUser eu)
    Consumer LoginConsumer(1: string email, 2: string password) throws (1: ExceptionUser eu)
    ContentCreator LoginContentCreator(1: string email, 2: string password) throws (1: ExceptionUser eu)
}

service ContentCreatorManagement {
    #Consultar contenido 
    Album GetAlbumByTitle(1: string title) throws (1: ExceptionContent ec)
    Track GetTrackByTitle(1: string title) throws (1: ExceptionContent ec)
    Playlist GetPlaylistByTitle(1: string title) throws (1: ExceptionContent ec)

    #Agregar álbum al contenido
    Album AddAlbum(1: SpotifakeManagement.Album newAlbum) throws (1: ExceptionContent ec)

    #Eliminar álbum
    Id DeleteAlbum(1: SpotifakeManagement.Id idAlbum) throws (1: ExceptionContent ec)

    #Actualizar álbum
    Album UpdateAlbumTitle(1: SpotifakeManagement.Id idAlbum, 2: string newAlbumTitle) throws (1: ExceptionContent ec)
    Album UpdateAlbumCover(1: SpotifakeManagement.Id idAlbum, 2: string newImageStoragePath) throws (1: ExceptionContent ec)
    Album UpdateAlbumFeaturing(1: SpotifakeManagement.Id idAlbum, 2: SpotifakeManagement.Interpreter newFeaturing) throws (1: ExceptionContent ec)

    #Actualizar canción
    Track AddTrackToAlbum(1: SpotifakeManagement.Id idAlbum, 2: SpotifakeManagement.Track newTrack) throws (1: ExceptionContent ec)
    Id DeleteAlbumTrack(1: SpotifakeManagement.Id idAlbum, 2: SpotifakeManagement.Int trackNumber) throws (1: ExceptionContent ec)
    Track UpdateAlbumTrackTitle(1: SpotifakeManagement.Id idAlbum, 2: SpotifakeManagement.Int trackNumber, 3: string newAlbumTrackTitle) throws (1: ExceptionContent ec)
    Track UpdateAlbumTrackFeaturing(1: SpotifakeManagement.Id idAlbum, 2: SpotifakeManagement.Int trackNumber, 3: SpotifakeManagement.Interpreter newFeaturing) throws (1: ExceptionContent ec)
}

service ConsumerManagement {
    Track AddTrackToLibrary(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.rack newTrack) throws (1: ExceptionContent ec)
    Id DeleteLibraryTrack(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.Int trackNumber) throws (1: ExceptionContent ec)

    Album AddAlbumToLibrary(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.Album  newAlbum) throws (1: ExceptionContent ec)
    Id DeleteLibraryAlbum(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.Id idAlbum) throws (1: ExceptionContent ec)

    Playlist AddPlaylistToLibrary(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.Playlist newPlaylist) throws (1: ExceptionContent ec)
    Id DeleteLibraryPlaylist(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.Id Alaylist) throws (1: ExceptionContent ec)

    ContentCreator AddContentCreatorToLibrary(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.ContentCreator newContentCreator) throws (1: ExceptionContent ec)
    Id DeleteLibraryContentCreator(1: SpotifakeManagement.Id idLibrary, 2: SpotifakeManagement.Id idContentCreator) throws (1: ExceptionContent ec)

    RelatedResult getContent(1: string query) throws (1: ExceptionContent ec)

    Playlist UpdatePlaylistTitle(1: Id idPlaylist, 2: string newPlaylistTitle) throws (1: ExceptionContent ec)
    Album UpdatePlaylistCover(1: Id idPlaylist, 2: string newImageStoragePath) throws (1: ExceptionContent ec)
    Album UpdatePlaylistDescription(1: Id idPlaylist, 2: string newDescription) throws (1: ExceptionContent ec)
    Track AddTrackToPlaylist(1: Id idPlaylist, 2: Track newtrack) throws (1: ExceptionContent ec)
    Id DeletePlaylistTrack(1: Id idPlaylist, 2: Int trackNumber) throws (1: ExceptionContent ec)

    Track AddTrackToPlayQueue(1: Id idPlayQueu, 2: Track newTrack) throws (1: ExceptionContent ec)
    Id DeletePlayQueueTrack(1: Id idPlayQueu, 2: Int trackNumber) throws (1: ExceptionContent ec)

    list<Track> GenerateRadioStation(1: MusicGender gender) throws (1: ExceptionUser eu)
}
