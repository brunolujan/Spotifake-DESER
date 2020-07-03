import sys
import SpotifakeServer
sys.path.append("gen-py")
from SpotifakeService import SpotifakeService
from SpotifakeService .ttypes import *
sys.path.append("../SQLConnection")
from connection import SQLConnection

class SpotifakeServerHandler(SpotifakeService.Iface):

    def __init__(self):
        
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