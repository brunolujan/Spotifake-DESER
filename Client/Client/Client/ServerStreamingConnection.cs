using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Transport.Client;

namespace Client {

    public class ServerStreamingConnection {

        public StreamingService.Client streamingService;

        /*public static void Connection() {
            try {
                TTransport transport = new TSocketTransport("localhost", 8000);
                TProtocol protocol = new TBinaryProtocol(transport);
                streamingService = new StreamingService.Client(protocol);
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

        public static void Disconnect() {
            try {
                streamingService = null;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task<byte[]> GetTrackAudio(string fileName) {
            TrackAudio trackAudio;
            try {
                trackAudio = await streamingService.GetTrackAudioAsync(new RequestTrackAudio() { Filename = fileName });
                Console.WriteLine("Recuperado" + trackAudio.Song.Length);

            } catch (Exception ex) {
                throw ex;
            }

            return trackAudio.Song;
        }

        public static async Task<TrackUploaded> UploadTrack(TrackAudio trackAudio) {
            TrackUploaded trackUploaded;
            try {
                trackUploaded = await streamingService.UploadTrackAsync(trackAudio);

            } catch (Exception ex) {

                throw ex;
            }
            return trackUploaded;
        }*/
    }
}
