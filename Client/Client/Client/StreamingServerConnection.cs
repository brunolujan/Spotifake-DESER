using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Transport.Client;

namespace Client
{
    public class StreamingServiceConnection
    {
        private static StreamingService.Client streamingService;

        public static void Connection(){
                
            try{

                TTransport transport = new TSocketTransport("localhost", 8000);
                TProtocol protocol = new TBinaryProtocol(transport);
                streamingService = new StreamingService.Client(protocol);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
        public static void Disconnect()
        {
            try
            {
                streamingService = null;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public static async Task<byte[]> GetTrackAudio(string fileName)
        {
            TrackAudio trackAudio;
            try
            {
                trackAudio = await streamingService.GetTrackAudioAsync(new TrackRequest() { Filename = fileName, Quality = Quality.LOW });
                Console.WriteLine("Recuperado" + trackAudio.Audio.Length);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return trackAudio.Audio;
        }

        public static async Task<Boolean> UploadPersonalTrack(TrackAudio trackAudio)
        {
            
            try
            {
                await streamingService.UploadPersonalTrackAsync(trackAudio);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}

        