using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System.Threading.Tasks;
using Thrift;

namespace SpotifakeTests {
    [TestClass]
    public class TrackAlbumPageTests {

        [TestMethod]
        public void GetTrackByAlbumIdTest() {
            var tracks = Session.serverConnection.trackService.GetTrackByAlbumIdAsync(83);
            tracks.Wait();
            Assert.AreEqual(tracks.IsFaulted, false);
        }

        [TestMethod]
        public void AddTrackToLibraryTest() {
            var track = Session.serverConnection.trackService.AddTrackToLibraryAsync(7,54);
            track.Wait();
            Assert.AreEqual(track.IsFaulted, false);
        }

        [TestMethod]
        public void GenerateRadioStationTest() {
            var tracks = Session.serverConnection.trackService.GenerateRadioStationAsync(17);
            tracks.Wait();
            Assert.AreEqual(tracks.IsFaulted, false);
        }

    }
}
