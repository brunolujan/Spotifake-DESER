from SQLConnection.connection import SQLConnection

class SqlServerLibraryManagement:
    def __init__(self):
        self.connection: SQLConnection = SQLConnection()

    def GetLibraryByIdConsumer(self, idConsumer):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
		            @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPC_GetLibraryConsumer]
                    @idConsumer = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'
        """
        connection.cursor.execute(sql, idConsumer)
        row = connection.cursor.fetchval()
        return row
    
    def AddLibrary(self, idConsumer):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql ="""
            INSERT INTO Library (IdConsumer) VALUES (?);
        """
        connection.cursor.execute(sql, idConsumer)
        row = connection.cursor.fetchval()
        return row