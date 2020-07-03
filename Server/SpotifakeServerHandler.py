import sys
import SpotifakeServer
from Thrift.SpotifakeService import SpotifakeService
from SpotifakeService .ttypes import *
<<<<<<< HEAD
from SQLConnection.sqlServer_consumer import SqlServerConsumer
=======
sys.path.append("../SQLConnection")
from connection import SQLConnection
>>>>>>> 2513789b5d23fc22248d3780ae13131abcac34c4

class SpotifakeServerHandler(SpotifakeService.Iface):

    def __init__(self):
<<<<<<< HEAD
        pass

    def GetConsumerById(self, idConsumer):
        repo: SqlServerConsumer = SqlServerConsumer()
        result = repo.save(consumer)
        pass
=======
        
        self.connection = SQLConnection()

    def DeleteConsumer(self, email):
        self.connection.open()
        sql = """
            DECLARE     @return_value int,
                    @estado int,
                    @salida nvarchar(1000)

            EXEC        @return_value = [dbo].[SPD_DeleteConsumer]
                    @idConsumer = ?,
                    @estado = @estado OUTPUT,
                    @salida = @salida OUTPUT
        """
        params = (email)
        self.connection.cursor.execute(sql, params)
        row = self.connection.cursor.rowcount
        print (row)
               







    #def GetConsumerById(idConsumer):

        #self.connection.open()

        #self.save()

        #self.close()

        return 1
>>>>>>> 2513789b5d23fc22248d3780ae13131abcac34c4
