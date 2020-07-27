
struct TrackRequest{
    1:  string filename
    2:  Quality quality 
}

struct TrackAudio{
    1:  string idTrack
    2:  string trackName
    3:  binary audio 
}

service StreamingService{
    bool GetTrackAudio(1: TrackRequest trackRequest)
    bool UploadTrack(1: TrackAudio trackAudio)
    bool UploadPersonalTrack(1: TrackAudio trackAudio)
}
