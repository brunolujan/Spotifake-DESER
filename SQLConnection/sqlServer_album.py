from SQLConnection.connection import SQLConnection

class SqlServerAlbumManagement:
    def __init__(self):
        self.connection: SQLConnection = SQLConnection()

    def GetAlbumByTitle(self,title:str):
        self.connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)
            EXEC	@return_value = [dbo].[SPC_GetAlbum]
                    @title = ?,
                    @salida = @salida OUTPUT
            SELECT  @salida as N'@salida'
        """
        self.connection.cursor.execute(sql, title)
        row = self.connection.cursor.fetchall()
        self.connection.save()
        print(row)
        self.connection.close()

    def DeleteAlbum(self, idAlbum:int):
        self.connection.open()
        sql = """
            DELETE FROM Album WHERE idAlbum = ?
        """
        self.connection.cursor.execute(sql, idAlbum)
        self.connection.save()
        self.connection.close()

    def UpdateAlbumTitle(self, idAlbum:int, newAlbumTitle:str):
        self.connection.open()
        sql = """
            UPDATE Album 
            SET title = ?
            Where idAlbum = ?
        """
        params = (newAlbumTitle, idAlbum)
        self.connection.cursor.execute(sql, params)
        self.connection.save()
        print("Album title has been updated")
        self.connection.close()
    

