from SQLConnection.connection import SQLConnection
import datetime

class SqlServerAlbumManagement:
    def __init__(self):
        self.connection: SQLConnection = SQLConnection()

    def GetAlbumByTitle(self,title:str):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)
            EXEC	@return_value = [dbo].[SPC_GetAlbum]
                    @title = ?,
                    @salida = @salida OUTPUT
            SELECT  @salida as N'@salida'
        """
        connection.cursor.execute(sql, title)
        row = self.connection.cursor.fetchone()
        connection.save()
        print(row)
        connection.close()
        return row

    def GetAlbumsByContentCreatorId(self,idContentCreator):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPC_GetAlbumsByContentCreatorId]
                    @idContentCreator = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'
        """
        connection.cursor.execute(sql, idContentCreator)
        row = connection.cursor.fetchall()
        return row

    def GetSinglesByContentCreatorId(self,idContentCreator):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPC_GetSinglesByContentCreatorId]
                    @idContentCreator = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'
        """
        connection.cursor.execute(sql, idContentCreator)
        row = connection.cursor.fetchall()
        return row
        connection.close()

    def DeleteAlbum(self, idAlbum:int):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DELETE FROM Album WHERE idAlbum = ?
        """
        connection.cursor.execute(sql, idAlbum)
        connection.save()
        connection.close()

    def UpdateAlbumTitle(self, idAlbum:int, newAlbumTitle:str):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            UPDATE Album 
            SET title = ?
            Where idAlbum = ?
        """
        params = (newAlbumTitle, idAlbum)
        connection.cursor.execute(sql, params)
        connection.save()
        print("Album title has been updated")
        connection.close()

    def UpdateAlbumCover(self, idAlbum:int, newCoverStoragePath:str):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            UPDATE Album
            SET coverPath = ?
            Where idAlbum = ? 
        """
        params = (newCoverStoragePath, idAlbum)
        connection.cursor.execute(sql, params)
        connection.save()
        print("Your image has been updated")
        connection.close()

    def DeleteLibraryAlbum(self, idLibrary:int, idAlbum:int):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DELETE FROM LibraryAlbum WHERE idLibrary = ? AND idAlbum = ?
        """
        params = (idLibrary, idAlbum)
        connection.cursor.execute(sql, params)
        connection.save()
        print("Album has been deleted")
        connection.close()

    def AddAlbum(self, newAlbum, idContentCreator):
        releaseDate = datetime.datetime(newAlbum.releaseDate.year, newAlbum.releaseDate.month, newAlbum.releaseDate.day)
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPI_Album]
                    @title = ?,
                    @type = ?,
                    @releaseDate = ?,
                    @coverPath = ?,
                    @idContentCreator = ?,
                    @idGenre = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'
                    """
        params = (newAlbum.title, newAlbum.isSingle, releaseDate, newAlbum.coverPath,
                    idContentCreator, newAlbum.gender)
        connection.cursor.execute(sql, params)
        connection.cursor.nextset()
        row = connection.cursor.fetchval()
        connection.save()
        print(newAlbum.title, newAlbum.releaseDate)
        return row

    
    def AddAlbumToLibrary(self, idLibrary:int, newAlbum):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPI_LibraryAlbum]
                    @idLibrary = ?,
                    @idAlbum = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'   
        """
        params = (idLibrary, newTrack.idAlbum)
        connection.cursor.execute(sql, params)
        connection.save()
        connection.close()
        print(idLibrary, newAlbum.idAlbum)