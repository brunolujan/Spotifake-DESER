using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System.Threading.Tasks;
using Thrift;

namespace SpotifakeTests {
    [TestClass]
    public class MainWindowTests {

        [TestMethod]
        public void GetAlbumsFromLibrary() {
            var albums = Session.serverConnection.albumService.GetAlbumByLibraryIdAsync(1);
            albums.Wait();
            var album = albums.Result;
            Assert.AreEqual(albums.IsFaulted, false);
        }

    }
}