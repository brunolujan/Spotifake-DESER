#
# Autogenerated by Thrift Compiler (0.13.0)
#
# DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
#

require 'thrift'

class RequestTrackAudio; end

class TrackAudio; end

# Application Programming Interface definition for Spotijake Streaming Services.
# this parent thrift file is contains all service interfaces. The data models are
# described in respective thrift files.
class RequestTrackAudio
  include ::Thrift::Struct, ::Thrift::Struct_Union
  FILENAME = 1

  FIELDS = {
    FILENAME => {:type => ::Thrift::Types::STRING, :name => 'filename'}
  }

  def struct_fields; FIELDS; end

  def validate
  end

  ::Thrift::Struct.generate_accessors self
end

class TrackAudio
  include ::Thrift::Struct, ::Thrift::Struct_Union
  SONG = 1
  FILENAME = 2

  FIELDS = {
    SONG => {:type => ::Thrift::Types::STRING, :name => 'song', :binary => true},
    FILENAME => {:type => ::Thrift::Types::STRING, :name => 'filename'}
  }

  def struct_fields; FIELDS; end

  def validate
  end

  ::Thrift::Struct.generate_accessors self
end

