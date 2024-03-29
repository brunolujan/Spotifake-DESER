#
# Autogenerated by Thrift Compiler (0.13.0)
#
# DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
#

require 'thrift'
require 'streaming_services_types'

module StreamingService
  class Client
    include ::Thrift::Client

    def GetTrackAudio(requestTrackAudio)
      send_GetTrackAudio(requestTrackAudio)
      return recv_GetTrackAudio()
    end

    def send_GetTrackAudio(requestTrackAudio)
      send_message('GetTrackAudio', GetTrackAudio_args, :requestTrackAudio => requestTrackAudio)
    end

    def recv_GetTrackAudio()
      result = receive_message(GetTrackAudio_result)
      return result.success unless result.success.nil?
      raise ::Thrift::ApplicationException.new(::Thrift::ApplicationException::MISSING_RESULT, 'GetTrackAudio failed: unknown result')
    end

    def UploadTrack(trackAudio)
      send_UploadTrack(trackAudio)
      return recv_UploadTrack()
    end

    def send_UploadTrack(trackAudio)
      send_message('UploadTrack', UploadTrack_args, :trackAudio => trackAudio)
    end

    def recv_UploadTrack()
      result = receive_message(UploadTrack_result)
      return result.success unless result.success.nil?
      raise ::Thrift::ApplicationException.new(::Thrift::ApplicationException::MISSING_RESULT, 'UploadTrack failed: unknown result')
    end

  end

  class Processor
    include ::Thrift::Processor

    def process_GetTrackAudio(seqid, iprot, oprot)
      args = read_args(iprot, GetTrackAudio_args)
      result = GetTrackAudio_result.new()
      result.success = @handler.GetTrackAudio(args.requestTrackAudio)
      write_result(result, oprot, 'GetTrackAudio', seqid)
    end

    def process_UploadTrack(seqid, iprot, oprot)
      args = read_args(iprot, UploadTrack_args)
      result = UploadTrack_result.new()
      result.success = @handler.UploadTrack(args.trackAudio)
      write_result(result, oprot, 'UploadTrack', seqid)
    end

  end

  # HELPER FUNCTIONS AND STRUCTURES

  class GetTrackAudio_args
    include ::Thrift::Struct, ::Thrift::Struct_Union
    REQUESTTRACKAUDIO = 1

    FIELDS = {
      REQUESTTRACKAUDIO => {:type => ::Thrift::Types::STRUCT, :name => 'requestTrackAudio', :class => ::RequestTrackAudio}
    }

    def struct_fields; FIELDS; end

    def validate
    end

    ::Thrift::Struct.generate_accessors self
  end

  class GetTrackAudio_result
    include ::Thrift::Struct, ::Thrift::Struct_Union
    SUCCESS = 0

    FIELDS = {
      SUCCESS => {:type => ::Thrift::Types::STRUCT, :name => 'success', :class => ::TrackAudio}
    }

    def struct_fields; FIELDS; end

    def validate
    end

    ::Thrift::Struct.generate_accessors self
  end

  class UploadTrack_args
    include ::Thrift::Struct, ::Thrift::Struct_Union
    TRACKAUDIO = 1

    FIELDS = {
      TRACKAUDIO => {:type => ::Thrift::Types::STRUCT, :name => 'trackAudio', :class => ::TrackAudio}
    }

    def struct_fields; FIELDS; end

    def validate
    end

    ::Thrift::Struct.generate_accessors self
  end

  class UploadTrack_result
    include ::Thrift::Struct, ::Thrift::Struct_Union
    SUCCESS = 0

    FIELDS = {
      SUCCESS => {:type => ::Thrift::Types::BOOL, :name => 'success'}
    }

    def struct_fields; FIELDS; end

    def validate
    end

    ::Thrift::Struct.generate_accessors self
  end

end

