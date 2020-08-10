using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System.Threading.Tasks;
using Thrift;

namespace SpotifakeTests {
    [TestClass]
    public class TrackPlaylistPageTests {

        [TestMethod]
        public void GetTrackByPlaylistIdTest() {
            var tracks = Session.serverConnection.trackService.GetTrackByPlaylistIdAsync(2);
            tracks.Wait();
            Assert.AreEqual(tracks.IsFaulted, false);
        }
    }
}