using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System.Threading.Tasks;
using Thrift;

namespace SpotifakeTests {
    [TestClass]
    public class LoginTests {

        [TestMethod]
        public void LoginConsumerWithRegisteredAccountTest() {
            var ConsumerLog = Session.serverConnection.consumerService.LoginConsumerAsync("majohdezmol@gmail.com", "majojojo");
            ConsumerLog.Wait();
            //var consumer = ConsumerLog.Result;
            Assert.AreEqual(ConsumerLog.IsFaulted, false);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void LoginConsumerWithNonRegisteredAccountTest() {
            var ConsumerLog = Session.serverConnection.consumerService.LoginConsumerAsync("correonoregistrado@gmail.com", "pass");
            ConsumerLog.Wait();
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void LoginConsumerWithoutPasswordTest() {
            var ConsumerLog = Session.serverConnection.consumerService.LoginConsumerAsync("majohdezmo@gmail.com", null);
            ConsumerLog.Wait();
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void LoginConsumerWithoutEmailTest() {
            var ConsumerLog = Session.serverConnection.consumerService.LoginConsumerAsync(null, "majojojo");
            ConsumerLog.Wait();
        }

        [TestMethod]
        public void LoginContentCreatorWithRegisteredAccountTest() {
            var ContentCreatorLog = Session.serverConnection.contentCreatorService.LoginContentCreatorAsync("coldplay@gmail.com", "coldplay22");
            ContentCreatorLog.Wait();
            //var consumer = ConsumerLog.Result;
            Assert.AreEqual(ContentCreatorLog.IsFaulted, false);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void LoginContentCreatorWithNonRegisteredAccountTest() {
            var ContentCreatorLog = Session.serverConnection.contentCreatorService.LoginContentCreatorAsync("correonoregistrado@gmail.com", "pass");
            ContentCreatorLog.Wait();
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void LoginContentCreatorWithoutPasswordTest() {
            var ContentCreatorLog = Session.serverConnection.contentCreatorService.LoginContentCreatorAsync("coldplay@gmail.com", null);
            ContentCreatorLog.Wait();
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void LoginContentCreatorWithoutEmailTest() {
            var ContentCreatorLog = Session.serverConnection.contentCreatorService.LoginContentCreatorAsync(null, "coldplay22");
            ContentCreatorLog.Wait();
        }
    }
}
