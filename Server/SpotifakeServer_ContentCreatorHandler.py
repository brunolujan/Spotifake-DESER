import sys
sys.path.append("../")
sys.path.append("gen-py")
from SpotifakeServices import ContentCreatorService
from SpotifakeServices.ttypes import *
from SQLConnection.sqlServer_contentCreator import SqlServerContentCreatorManagement

class SpotifakeServerContentCreatorHandler(ContentCreatorService.Iface):

    def __init__(self):
        pass
