import sys
sys.path.append("../")
sys.path.append("gen-py")
from SpotifakeService import SpotifakeService
from SpotifakeService.ttypes import *
from SQLConnection.sqlServer_consumer import SqlServerConsumerManagement

class SpotifakeServerHandler(SpotifakeService.Iface):

    def __init__(self):
        pass

    def GetConsumerById(self, idConsumer):
        repo: SqlServerConsumerManagement = SqlServerConsumerManagement()
        pass
