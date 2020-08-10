using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System.Threading.Tasks;
using Thrift;

namespace SpotifakeTests {
    [TestClass]
    public class SearchPageTests {

        [TestMethod]
        public void SearchTrackByQuery() {
            var tracks = Session.serverConnection.trackService.GetTrackByQueryAsync("Artificial");
            tracks.Wait();
            Assert.AreEqual(tracks.IsFaulted, false);
        }

        [TestMethod]
        public void SearchAlbumByQuery() {
            var albums = Session.serverConnection.albumService.GetAlbumByQueryAsync("Foráneo");
            albums.Wait();
            Assert.AreEqual(albums.IsFaulted, false);
        }

        [TestMethod]
        public void SearchContentCreatoryQuery() {
            var contentCreators = Session.serverConnection.contentCreatorService.GetContentCreatorByQueryAsync("Coldplay");
            contentCreators.Wait();
            Assert.AreEqual(contentCreators.IsFaulted, false);
        }

        [TestMethod]
        public void SearchPlaylistByQuery() {
            var playlists = Session.serverConnection.playlistService.GetPlaylistByQueryAsync("Estudiar");
            playlists.Wait();
            Assert.AreEqual(playlists.IsFaulted, false);
        }

        [TestMethod]
        public void AddAlbumToLibrary() {
            var albums = Session.serverConnection.albumService.AddAlbumToLibraryAsync(7,83);
            albums.Wait();
            Assert.AreEqual(albums.IsFaulted, false);
        }

        [TestMethod]
        public void AddTrackToLibrary() {
            var tracks = Session.serverConnection.trackService.AddTrackToLibraryAsync(7, 54);
            tracks.Wait();
            Assert.AreEqual(tracks.IsFaulted, false);
        }

        [TestMethod]
        public void AddContentCreatorToLibrary() {
            var contentCreators = Session.serverConnection.contentCreatorService.AddContentCreatorToLibraryAsync(7, 21);
            contentCreators.Wait();
            Assert.AreEqual(contentCreators.IsFaulted, false);
        }

        [TestMethod]
        public void AddPlaylistToLibrary() {
            var playlists = Session.serverConnection.playlistService.AddPlaylistToLibraryAsync(7, 3);
            playlists.Wait();
            Assert.AreEqual(playlists.IsFaulted, false);
        }





    }
}
