using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System.Threading.Tasks;
using Thrift;

namespace SpotifakeTests {
    [TestClass]
    public class PlaylistPageTests {

        [TestMethod]
        public void GetPlaylistFromLibrary() {
            var playlists = Session.serverConnection.playlistService.GetPlaylistByLibraryIdAsync(1);
            playlists.Wait();
            Assert.AreEqual(playlists.IsFaulted, false);
        }

    }
}