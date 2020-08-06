from SQLConnection.connection import SQLConnection

class SqlServerPlaylistManagement:
    def __init__(self):
        self.connection: SQLConnection = SQLConnection()

    def GetPlaylistByTitle(self,title:str):
        self.connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPC_GetPlaylistByIdLibrary]
                    @idLibrary = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'
        """
        self.connection.cursor.execute(sql, title)
        row = self.connection.cursor.fetchall()
        self.connection.save()
        print(row)
        self.connection.close()

    def GetPlaylistByLibraryId(self, idLibrary):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPC_GetPlaylistsByIdLibrary]
                    @idLibrary = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'
        """
        connection.cursor.execute(sql, idLibrary)
        row = connection.cursor.fetchall()
        return row

    def UpdatePlaylistTitle(self, idPlaylist:int,newPlaylistTitle:str):
        self.connection.open()
        sql = """
            UPDATE Playlist 
            SET title = ?
            Where idPlaylist = ?
        """
        params = (newPlaylistTitle, idPlaylist)
        self.connection.cursor.execute(sql, params)
        self.connection.save()
        print("Playlist title has been updated")
        self.connection.close()

    def UpdatePlaylistDescription(self, idPlaylist:int, newDescription:str):
        self.connection.open()
        sql = """
            UPDATE Playlist 
            SET description = ?
            Where idPlaylist = ?
        """
        params = (newDescription, idPlaylist)
        self.connection.cursor.execute(sql, params)
        self.connection.save()
        print("Playlist description has been updated")
        self.connection.close()

    def UpdatePlaylistCover(self, idPlaylist:int, newImageStoragePath:str):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            UPDATE Playlist
            SET coverPath = ?
            Where idPlaylist = ? 
        """
        params = (newImageStoragePath, idPlaylist)
        connection.cursor.execute(sql, params)
        connection.save()
        print("Your image has been updated")
        connection.close()

    
    def DeleteLibraryPlaylist(self, idLibrary:int, idPlaylist:int):
        self.connection.open()
        sql = """
            DELETE FROM LibraryPlaylist WHERE idLibrary = ? AND idPlaylist = ?
        """
        params = (idLibrary, idPlaylist)
        self.connection.cursor.execute(sql, params)
        self.connection.save()
        print("Playlist has been deleted")
        self.connection.close()

    def AddPlaylistToLibrary(self, idLibrary:int, idPlaylist:int):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPI_LibraryPlaylist]
                    @idLibrary = ?,
                    @idPlaylist = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'   
        """
        params = (idLibrary, idPlaylist)
        connection.cursor.execute(sql, params)
        connection.save()
        return True

    def GetPlaylistByQuery(self, query:str):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            EXEC	[dbo].[SPC_GetPlaylistByQuery]
		            @query = ?
        """
        connection.cursor.execute(sql, query)
        rows = connection.cursor.fetchall()
        return rows