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

    def AddContentCreator(self, newContentCreator):
        SqlServerContentCreatorManagement.AddContentCreator(self, newContentCreator)

    def GetContentCreators(self):
        contentCreatorList = []
        contentCreatorFound = SqlServerContentCreatorManagement.GetContentCreators(self)
        for n in contentCreatorFound:
            contentCreatorAux = ContentCreator()            
            contentCreatorAux.idContentCreator = n.IdContentCreator
            contentCreatorAux.givenName = n.name
            contentCreatorAux.lastName = n.lastname
            contentCreatorAux.stageName = n.stageName
            contentCreatorAux.email = n.email
            contentCreatorAux.password = n.password
            contentCreatorAux.description = n.description
            contentCreatorAux.imageStoragePath = n.imageStoragePath
            contentCreatorList.append(contentCreatorAux)
        return contentCreatorList

    def GetContentCreatorById(self, idContentCreator):
        connection.GetContentCreatorById(idContentCreator)

    def GetContentCreatorByEmailPassword(self, email, password):
        connection.GetContentCreatorByEmailPassword(email, password)

    def GetContentCreatorByEmail(self, email):
        if(SqlServerContentCreatorManagement.GetContentCreatorByEmail(self, email) != None):
            return True
        else:
            return False

    def GetContentCreatorByStageName(self, stageName):
        if(SqlServerContentCreatorManagement.GetContentCreatorByStageName(self, stageName) != None):
            return True
        else:
            return False

    def GetContentCreatorByLibraryId(self, idLibrary):
        contentCreatorList = []
        contentCreatorFound = SqlServerContentCreatorManagement.GetContentCreatorByLibraryId(self, idLibrary)
        for n in contentCreatorFound:
            contentCreatorAux = ContentCreator()            
            contentCreatorAux.idContentCreator = n.IdContentCreator
            contentCreatorAux.givenName = n.name
            contentCreatorAux.lastName = n.lastname
            contentCreatorAux.stageName = n.stageName
            contentCreatorAux.email = n.email
            contentCreatorAux.password = n.password
            contentCreatorAux.description = n.description
            contentCreatorAux.imageStoragePath = n.imageStoragePath
            contentCreatorList.append(contentCreatorAux)
        return contentCreatorList

    def LoginContentCreator(self, email, password):
        contentCreator = ContentCreator()
        contentCreatorFound = SqlServerContentCreatorManagement.GetContentCreatorByEmailPassword(self, email, password)
        if (contentCreatorFound != None):
            contentCreator.idContentCreator = contentCreatorFound.IdContentCreator
            contentCreator.givenName = contentCreatorFound.name
            contentCreator.lastName = contentCreatorFound.lastname
            contentCreator.email = contentCreatorFound.email
            contentCreator.password = contentCreatorFound.password
            contentCreator.stageName = contentCreatorFound.stageName
            contentCreator.description = contentCreatorFound.description
            contentCreator.imageStoragePath = contentCreatorFound.imageStoragePath
            
            return contentCreator
        else:
            return None

    def GetContentCreatorByQuery(self, query):
        contentCreatorList = []
        contentCreatorFound =  SqlServerContentCreatorManagement.GetContentCreatorByQuery(self, query)
        if  contentCreatorFound != 0:
                for n in contentCreatorFound:
                    contentCreatorAux = ContentCreator()            
                    contentCreatorAux.idContentCreator = n.IdContentCreator
                    contentCreatorAux.stageName = n.stageName
                    contentCreatorAux.description = n.description
                    contentCreatorList.append(contentCreatorAux)
                return contentCreatorList        
        return False