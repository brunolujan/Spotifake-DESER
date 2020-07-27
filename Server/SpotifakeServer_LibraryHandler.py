import sys
import thriftpy
sys.path.append("../")
sys.path.append("gen-py")
from SpotifakeServices import LibraryService
from SpotifakeServices.ttypes import *
from SpotifakeManagement.ttypes import *
from SQLConnection.sqlServer_library import SqlServerLibraryManagement

class SpotifakeServerLibraryHandler(LibraryService.Iface):

    connection: SqlServerLibraryManagement = SqlServerLibraryManagement()
    spotifakeManagement_thrift = thriftpy.load('../Thrift/SpotifakeManagement.thrift', module_name='spotifakeManagement_thrift')
    spotifakeServices_thrift = thriftpy.load('../Thrift/SpotifakeServices.thrift', module_name='spotifakeServices_thrift')
    Library = spotifakeManagement_thrift.Library

    def __init__(self):
        pass

    def getLibraryByIdConsumer(self, idConsumer):
        idLibrary = SqlServerLibraryManagement.GetLibraryByIdConsumer(self, idConsumer)
        return idLibrary