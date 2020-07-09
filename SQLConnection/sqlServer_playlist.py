from SQLConnection.connection import SQLConnection

class SqlServerPlaylistManagement:
    def __init__(self):
        self.connection: SQLConnection = SQLConnection()

    def GetPlaylistByTitle(self,title:str):
        self.connection.open()
        sql = """
            DECLARE	@return_value int,
		            @salida nvarchar(1000)
            EXEC	@return_value = [dbo].[SPC_GetPlaylist]
                    @title = ?,
                    @salida = @salida OUTPUT
            SELECT	@salida as N'@salida'
        """
        self.connection.cursor.execute(sql, title)
        row = self.connection.cursor.fetchall()
        self.connection.save()
        print(row)
        self.connection.close()

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