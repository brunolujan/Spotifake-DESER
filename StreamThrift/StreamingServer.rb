$:.push('gen-rb')
require 'thrift'
require 'streaming_service'
require 'streaming_services_types'
require "mp3info"


def GetAudioDuration(filename)
    Mp3Info.open("../Media/Tracks/#{filename}.mp3") do |mp3info|
        return mp3info.length.to_i
      end
end

def AddTrackToMedia(fileName, audio)
        file = open "../Media/Tracks/#{trackRequest.fileName}.mp3", "wb"
        file.write(audio)
        file.close()
        return True
end

class StreamingServiceHandler

    def GetTrackAudio(trackRequest)
        puts "Entrando al servicio"
        audio = ''
        File.open "../Media/Tracks/#{trackRequest.fileName}.mp3", "wb" do |source_file|
            until source_file.eof?
                chunk = source_file.read 100000 
                audio = audio + chunk
            end
        end
        track_audio = TrackAudio.new(audio: audio)
        return track_audio
    end

    def UploadTrack(trackAudio)         
        fileName = trackAudio.trackName + SecureRandom.hex(5)
        File.open("../Media/Track/#{fileName}.mp3", 'wb') do |destin_file|
            destin_file.write trackAudio.audio
        end
        track_uploaded = TrackUploaded.new(fileName: fileName)
        save_filename_track(trackAudio.idTrack, fileName) 
        return True
    end

    def UploadPersonalTrack(trackAudio)
        filename = trackAudio.trackName() + SecureRandom.hex(5)
        File.open("../Media/Track/#{filename}.mp3", 'wb') do |destin_file|
            destin_file.write trackAudio.audio
        end
        update_personal_track(trackAudio.idTrack, filename)
        track_uploaded = TrackUploaded.new(fileName: filename)
        return True
    end

end


handler = StreamingServiceHandler.new()
processor = StreamingService::Processor.new(handler)
transport = Thrift::ServerSocket.new(8000)
transportFactory = Thrift::BufferedTransportFactory.new()
server = Thrift::SimpleServer.new(processor, transport, transportFactory)
puts "Server is running"
server.serve()