using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System.Threading.Tasks;
using Thrift;

namespace SpotifakeTests {
    [TestClass]
    public class AddTrackToPlaylistPageTests {

        [TestMethod]
        public void GetPlaylistFromLibrary() {
            var playlists = Session.serverConnection.trackService.AddTrackToPlaylistAsync(2, 51);
            playlists.Wait();
            Assert.AreEqual(playlists.IsFaulted, false);
        }
    }
}