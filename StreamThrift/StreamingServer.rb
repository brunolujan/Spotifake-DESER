$:.push('thrift/gen-rb')
require 'thrift'
require 'streaming_service'
require 'streaming_services_types'

class StreamingServiceHandler
    
    def GetTrackAudio(requestTrackAudio)
        puts requestTrackAudio.filename
        audio = ''
        File.open "../Media/Tracks/#{requestTrackAudio.filename}.mp3", "r" do |source_file|
            until source_file.eof?
                chunk = source_file.read 100000 
                audio = audio + chunk
            end
        end
        track_audio = TrackAudio.new(song: audio)
        return track_audio
    end

    def UploadTrack(trackAudio)         
        File.open("../Media/Tracks/#{trackAudio.filename}.mp3", 'wb') do |destin_file|
            destin_file.write trackAudio.song
        end
        return true
    end
end

handler = StreamingServiceHandler.new()
processor = StreamingService::Processor.new(handler)
transport = Thrift::ServerSocket.new(8000)
transportFactory = Thrift::BufferedTransportFactory.new()
server = Thrift::SimpleServer.new(processor, transport, transportFactory)
puts "Server is running..."
server.serve()