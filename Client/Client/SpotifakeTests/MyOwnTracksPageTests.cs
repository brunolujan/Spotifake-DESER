using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System.Threading.Tasks;
using Thrift;

namespace SpotifakeTests {
    [TestClass]
    public class MyOwnTracksPageTests {

        [TestMethod]
        public void GetLocalTracksByIdConsumerTest() {
            var tracks = Session.serverConnection.trackService.GetLocalTracksByIdConsumerAsync(7);
            tracks.Wait();
            Assert.AreEqual(tracks.IsFaulted, false);
        }
    }
}
