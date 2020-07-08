import sys
sys.path.append("../")
sys.path.append("gen-py")
from SpotifakeServices import TrackService
from SpotifakeServices.ttypes import *
from SQLConnection.sqlServer_track import SqlServerTrackManagement

class SpotifakeServerTrackHandler(TrackService.Iface):

    def __init__(self):
        pass