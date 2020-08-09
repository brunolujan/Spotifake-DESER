using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System.Threading.Tasks;
using Thrift;

namespace SpotifakeTests {
    [TestClass]
    public class TracksPagesTests {

        [TestMethod]
        public void GetTracksFromLibrary() {
            var tracks = Session.serverConnection.trackService.GetTrackByLibraryIdAsync(1);
            tracks.Wait();
            Assert.AreEqual(tracks.IsFaulted, false);
        }

    }
}
