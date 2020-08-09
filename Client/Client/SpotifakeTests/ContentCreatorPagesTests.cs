using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System.Threading.Tasks;
using Thrift;

namespace SpotifakeTests {
    [TestClass]
    public class ContentCreatorPagesTests {

        [TestMethod]
        public void GetContentCreatorFromLibrary() {
            var contentCreators = Session.serverConnection.contentCreatorService.GetContentCreatorByLibraryIdAsync(1);
            contentCreators.Wait();
            Assert.AreEqual(contentCreators.IsFaulted, false);
        }

    }
}
