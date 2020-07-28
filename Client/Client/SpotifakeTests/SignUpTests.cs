using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System.Threading.Tasks;
using Thrift;

namespace SpotifakeTests {
    [TestClass]
    public class SignUpTests {

        [TestMethod]
        public void SignUpConsumerWithNonExistentEmailTest() {
            var consumerAux = Session.serverConnection.consumerService.GetConsumerByEmailAsync("deser@gmail.com");
            consumerAux.Wait();
            var consumer = consumerAux.Result;
            Assert.AreEqual(consumerAux.Result, false);
        }

        [TestMethod]
        public void SignUpConsumerWithExistentEmailTest() {
            var consumerAux = Session.serverConnection.consumerService.GetConsumerByEmailAsync("majohdezmol@gmail.com");
            consumerAux.Wait();
            var consumer = consumerAux.Result;
            Assert.AreEqual(consumerAux.Result, true);
        }

        [TestMethod]
        public void SignUpContentCreatorWithNonExistentEmailTest() {
            var contentCreatorAux = Session.serverConnection.contentCreatorService.GetContentCreatorByEmailAsync("deser@gmail.com");
            contentCreatorAux.Wait();
            var contentCreator = contentCreatorAux.Result;
            Assert.AreEqual(contentCreatorAux.Result, false);
        }

        [TestMethod]
        public void SignUpContentnCreatorrWithExistentEmailTest() {
            var contentCreatorAux = Session.serverConnection.contentCreatorService.GetContentCreatorByEmailAsync("coldplay@gmail.com");
            contentCreatorAux.Wait();
            var contentnCreator = contentCreatorAux.Result;
            Assert.AreEqual(contentCreatorAux.Result, true);
        }


    }
}