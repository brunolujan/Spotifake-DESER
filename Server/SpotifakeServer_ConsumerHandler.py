import sys
import thriftpy
sys.path.append("../")
sys.path.append("gen-py")
from SpotifakeServices import ConsumerService
from SpotifakeServices.ttypes import *
from SpotifakeManagement.ttypes import *
from SQLConnection.sqlServer_consumer import SqlServerConsumerManagement

class SpotifakeServerConsumerHandler(ConsumerService.Iface):
    
    connection: SqlServerConsumerManagement = SqlServerConsumerManagement()
    spotifakeManagement_thrift = thriftpy.load('../Thrift/SpotifakeManagement.thrift', module_name='spotifakeManagement_thrift')
    spotifakeServices_thrift = thriftpy.load('../Thrift/SpotifakeServices.thrift', module_name='spotifakeServices_thrift')
    Consumer = spotifakeManagement_thrift.Consumer

    def __init__(self):
        pass

    def GetConsumerById(self, idConsumer):
        connection.GetConsumerById(idConsumer)

    def GetConsumerByEmailPassword(self, email, password):
        connection.GetConsumerByEmailPassword(email, password)

    def AddConsumer(self, newConsumer):
        SqlServerConsumerManagement.AddConsumer(self, newConsumer)

    def LoginConsumer(self, email, password):
        consumer = Consumer()
        consumerFound = SqlServerConsumerManagement.GetConsumerByEmailPassword(self, email, password)
        if (consumerFound != None):
            consumer.idConsumer = consumerFound.IdConsumer
            consumer.givenName = consumerFound.name
            consumer.lastName = consumerFound.lastname
            consumer.email = consumerFound.email
            consumer.password = consumerFound.password
            consumer.imageStoragePath = consumerFound.imageStoragePath
            
            return consumer
        else:
            return None