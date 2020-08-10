using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System.Threading.Tasks;
using Thrift;

namespace SpotifakeTests {
    [TestClass]
    public class AlbumsContentCreatorTests {

        [TestMethod]
        public void GetTrackByPlaylistIdTest() {
            var albums = Session.serverConnection.albumService.GetAlbumsByContentCreatorIdAsync(1);
            albums.Wait();
            Assert.AreEqual(albums.IsFaulted, false);
        }
    }
}
