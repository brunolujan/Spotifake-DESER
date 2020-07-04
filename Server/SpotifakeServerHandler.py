import sys
import SpotifakeServer
from Thrift.SpotifakeService import SpotifakeService
from SpotifakeService .ttypes import *
from SQLConnection.sqlServer_consumer import SqlServerConsumerManagement

class SpotifakeServerHandler(SpotifakeService.Iface):

    def __init__(self):
        pass

    def GetConsumerById(self, idConsumer):
        repo: SqlServerConsumerManagement = SqlServerConsumerManagement()
        pass
