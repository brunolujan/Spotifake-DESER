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
 *  Application Programming Interface definition for Spotijake Streaming Services.
 *  this parent thrift file is contains all service interfaces. The data models are 
 *  described in respective thrift files.
*/


struct RequestTrackAudio{
    1:  string filename
}

struct TrackAudio{
    1:  binary song
    2:  string filename
}

service StreamingService{
    TrackAudio GetTrackAudio(1: RequestTrackAudio requestTrackAudio)
    bool UploadTrack(1: TrackAudio trackAudio)
}
