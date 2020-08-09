using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System.Threading.Tasks;
using Thrift;

namespace SpotifakeTests {
    [TestClass]
    public class AlbumPagesTests {

        [TestMethod]
        public void GetAlbumsFromLibrary() {
            var albums = Session.serverConnection.albumService.GetAlbumByLibraryIdAsync(1);
            albums.Wait();
            Assert.AreEqual(albums.IsFaulted, false);
        }

    }
}