import sys
sys.path.append("../")
sys.path.append("gen-py")
from SpotifakeServices import AlbumService
from SpotifakeServices.ttypes import *
from SQLConnection.sqlServer_album import SqlServerAlbumManagement

class SpotifakeServerAlbumHandler(AlbumService.Iface):

    def __init__(self):
        pass