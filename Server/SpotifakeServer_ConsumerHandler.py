import sys
sys.path.append("../")
sys.path.append("gen-py")
from SpotifakeServices import ConsumerService
from SpotifakeServices.ttypes import *
from SQLConnection.sqlServer_consumer import SqlServerConsumerManagement

class SpotifakeServerConsumerHandler(ConsumerService.Iface):

    def __init__(self):
        pass

    def GetConsumerById(self, idConsumer):
        repo: SqlServerConsumerManagement = SqlServerConsumerManagement()
        pass
