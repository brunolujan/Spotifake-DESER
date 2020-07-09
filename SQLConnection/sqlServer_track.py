from SQLConnection.connection import SQLConnection

class SqlServerTrackManagement:
    def __init__(self):
        self.connection: SQLConnection = SQLConnection()

    def DeleteAlbumTrack(self, idAlbum:int, trackNumber:int):
        self.connection.open()
        sql = """
            DELETE FROM Track WHERE idAlbum = ? AND trackNumber = ?
        """
        params = (idAlbum, trackNumber)
        self.connection.cursor.execute(sql, params)
        self.connection.save()
        self.connection.close()

    def UpdateAlbumTrackTitle(self, idAlbum:int, trackNumber:int, newAlbumTrackTitle:str):
        self.connection.open()
        sql = """
            UPDATE Track 
            SET title = ?
            Where idAlbum = ? AND trackNumber = ?
        """
        params = (newAlbumTrackTitle, idAlbum, trackNumber)
        self.connection.cursor.execute(sql, params)
        self.connection.save()
        print("Track title has been updated")
        self.connection.close()

    def GetTrackByTitle(self,title:str):
        self.connection.open()
        sql = """
            DECLARE	@return_value int,
		            @salida nvarchar(1000)
            EXEC	@return_value = [dbo].[SPC_GetTrack]
		            @title = ?,
		            @salida = @salida OUTPUT
            SELECT	@salida as N'@salida'
        """
        self.connection.cursor.execute(sql, title)
        row = self.connection.cursor.fetchall()
        self.connection.save()
        print(row)
        self.connection.close()

    def DeleteLibraryTrack(self, idLibrary:int, idTrack:int):
        self.connection.open()
        sql = """
            DELETE FROM LibraryTrack WHERE idLibrary = ? AND idTrack = ?
        """
        params = (idLibrary, idTrack)
        self.connection.cursor.execute(sql, params)
        self.connection.save()
        print("Track has been deleted")
        self.connection.close()

    def DeletePlaylistTrack(self, idPlaylist:int, idTrack:int):
        self.connection.open()
        sql = """
            DELETE FROM LibraryTrack WHERE idPlaylist = ? AND idTrack = ?
        """
        params = (idPlaylist, idTrack)
        self.connection.cursor.execute(sql, params)
        self.connection.save()
        print("Track has been deleted")
        self.connection.close()

    

    