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

typedef i16 Id
typedef bool IsSingle
typedef i16 Int
typedef string Name
typedef string Path

/**
*   This file describes the definitions of the model which encapsulates
*   the information that needs to be passed to the API methods in order to 
*   manage Consumer and Content Creator users and Content.
**/

struct Consumer {
    1: required Id      idConsumer
    2: Name             givenName
    3: Name             lastName
    4: string           email
    5: string           password
    6: Path             imageStoragePath
}


struct ContentCreator {
    1: required Id      idContentCreator
    2: Name             givenName
    3: Name             lastName
    4: string           email
    5: string           password
    6: string           stageName
    7: optional string  description
    8: Path             imageStoragePath
}


exception ExceptionUser {
    1: string           message
    2: Int              error_code
}

enum MusicGender {
    Pop         =       1,
    Rock        =       2,
    Bachata     =       3,
    Balada      =       4,
    Blues       =       5,
    Classic     =       6,
    Corrido     =       7,
    Country     =       8,
    Cumbia      =       9,
    Electronic  =       10,
    Flamenco    =       11,
    Reggaeton   =       12,
    Punk        =       13,
    Funk        =       14,
    Metal       =       15,
    HipHop      =       16,
    Indie       =       17,
    Jazz        =       18,
    Merengue    =       19,
    Salsa       =       20,
    Reggae      =       21,
    RocknRoll   =       22,
    Ska         =       23,
    Trap        =       24,
    Son         =       25,
    Vals        =       26,
    Ranchero    =       27,
    BosaNova    =       28,
    Bolero      =       29,
    Disco       =       30
}


struct Track {
    1: required Id      idTrack
    2: Int              trackNumber
    3: double           durationSeconds
    4: Path             storagePath
    5: string           title
    6: MusicGender      gender
}

struct LocalTrack{
    1: required Id      idLocalTrack
    2: Id               idConsumer
    3: Name             fileName
    4: Name             artistName
    5: string           title
}

struct Date {
    1: Int              day
    2: Int              month
    3: Int              year
}


struct Album {
    1: required Id      idAlbum
    2: string           title
    3: Path             coverPath
    4: Date             releaseDate
    5: MusicGender      gender
    6: IsSingle         isSingle
}


struct Playlist{
    1: required Id      idPlaylist
    2: string           name
    3: string           description
    4: Date             creationDate
    5: Path             coverPath
}


struct PlayQueue {
    1: required Id      idPlayQueu
    2: list<Track>      Tracks = []
}


struct Library {
    1: required Id      idLibrary
}

enum SErrorType {
    UNKNOWN                 =       1,
    PERMISSION_DENIED       =       2,
    INTERNAL_ERROR          =       3,
    AUTHENTICATION_FAILURE  =       4,
    INVALID_AUTHORIZATION   =       5,
    AUTORIZATHION_EXPIRED   =       6,
    UNKNOWN_GATEWAY_ID      =       7,
    UNSUPPORTED_OPERATION   =       8
}

/**
*   This is exception is thrown by SError procedures when  
*   a call fails as a result of a problem that a caller may be able to resolve.
*
*   error_code: The numeric code indicating the type of error that occurred.
*   Must be one of the values of SErrorType
*
*   message: This may contain additional information about the error.
*/
exception SErrorUserException {
    1: required SErrorType  error_code
    2: string               message
}

/**
*   This exception is thrown by SError procedures when a call fails as a result of
*   a problem in the service that could not be changed through caller action.
*
*   error_code: The numeric code indicating the type of error that occurred.
*   Must be one of the values of SErrorType
* 
*   message: This may contain additional information about the error.
*
*   rateLimitDuration: Indicates the minimum number of seconds that an application 
*   should expect subsequent API calls for this user to fail.
*/
exception SErrorSystemException{
    1: required SErrorType  error_code
    2: string               message
    3: i32                  rateLimitDuration
}

/** 
*   This exception is thrown by SError procedures when a caller asks to perform an operation on 
*   an object that does not exist.
*
*   identifier: A description of the object that was not found on the serve.
* 
*   key: The value passed from the client in the identifier, which was not found.
*/
exception SErrorNotFoundException{
    1: required string      identifier
    2: string               key
}

/** 
*   This exception is thrown for invalid requests that occur 
*   from any reasons like required input parameters are missing, 
*   or a parameter is malformed.
* 
*   message: ontains the associated error message.
*/
exception SErrorInvalidRequestException{
    1: string               message
}