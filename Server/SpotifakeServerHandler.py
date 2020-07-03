import sys
import SpotifakeServer
sys.path.append("gen-py")
from SpotifakeService import SpotifakeService
from SpotifakeService .ttypes import *
from SQLConenection.connection import SQLConnection

class SpotifakeServerHandler(SpotifakeService.Iface):

    def __init__(self):
        
        self.connection = ConnectionSQL()

    def GetConsumerById(idConsumer):

        self.connection.open()

        self.save()

        self.close()

        return 1