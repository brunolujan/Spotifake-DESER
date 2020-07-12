import sys
import thriftpy
sys.path.append("../")
sys.path.append("gen-py")
from SpotifakeServices import ContentCreatorService
from SpotifakeServices.ttypes import *
from SpotifakeManagement.ttypes import *
from SQLConnection.sqlServer_contentCreator import SqlServerContentCreatorManagement

class SpotifakeServerContentCreatorHandler(ContentCreatorService.Iface):

    connection: SqlServerContentCreatorManagement = SqlServerContentCreatorManagement()
    spotifakeManagement_thrift = thriftpy.load('../Thrift/SpotifakeManagement.thrift', module_name='spotifakeManagement_thrift')
    spotifakeServices_thrift = thriftpy.load('../Thrift/SpotifakeServices.thrift', module_name='spotifakeServices_thrift')
    ContentCreator = spotifakeManagement_thrift.ContentCreator

    def __init__(self):
        pass

    def GetContentCreatorById(self, idContentCreator):
        connection.GetContentCreatorById(idContentCreator)

    def GetContentCreatorByEmailPassword(self, email, password):
        connection.GetContentCreatorByEmailPassword(email, password)

    def LoginContentCreator(self, email, password):
        contentCreator = ContentCreator()
        contentCreatorFound = SqlServerContentCreatorManagement.GetContentCreatorByEmailPassword(self, email, password)
        if (contentCreatorFound != None):
            contentCreator.idConsumer = contentCreatorFound.IdConsumer
            contentCreator.givenName = contentCreatorFound.name
            contentCreator.lastName = contentCreatorFound.lastname
            contentCreator.stageName = contentCreatorFound.stageName
            contentCreator.password = contentCreatorFound.password
            contentCreator.email = contentCreatorFound.email
            contentCreator.description = contentCreatorFound.description
            contentCreator.imageStoragePath = contentCreatorFound.imageStoragePath
            
            return contentCreator
        else:
            return None

    def AddContentCreator(self, newContentCreator):
        SqlServerContentCreatorManagement.AddContentCreator(self, newContentCreator)