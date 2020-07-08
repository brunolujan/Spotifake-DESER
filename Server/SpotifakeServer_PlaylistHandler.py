import sys
sys.path.append("../")
sys.path.append("gen-py")
from SpotifakeServices import PlaylistService
from SpotifakeServices.ttypes import *
from SQLConnection.sqlServer_playlist import SqlServerPlaylistManagement

class SpotifakeServerPlaylistHandler(PlaylistService.Iface):

    def __init__(self):
        pass