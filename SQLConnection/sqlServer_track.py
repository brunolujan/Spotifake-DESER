from SQLConnection.connection import SQLConnection

class SqlServerTrackManagement:
    def __init__(self):
        self.connection: SQLConnection = SQLConnection()

    def DeleteAlbumTrack(self, idAlbum:int, trackNumber:int):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DELETE FROM Track WHERE idAlbum = ? AND trackNumber = ?
        """
        params = (idAlbum, trackNumber)
        connection.cursor.execute(sql, params)
        connection.save()
        connection.close()

    def UpdateAlbumTrackTitle(self, idAlbum:int, trackNumber:int, newAlbumTrackTitle:str):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            UPDATE Track 
            SET title = ?
            Where idAlbum = ? AND trackNumber = ?
        """
        params = (newAlbumTrackTitle, idAlbum, trackNumber)
        connection.cursor.execute(sql, params)
        connection.save()
        print("Track title has been updated")
        connection.close()

    def GetTrackByTitle(self,title:str):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
		            @salida nvarchar(1000)
            EXEC	@return_value = [dbo].[SPC_GetTrack]
		            @title = ?,
		            @salida = @salida OUTPUT
            SELECT	@salida as N'@salida'
        """
        connection.cursor.execute(sql, title)
        row = connection.cursor.fetchall()
        connection.save()
        print(row)
        connection.close()

    def GetTrackByIdAlbum(self,idAlbum:int):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPC_GetTracksByAlbumId]
                    @idAlbum = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'
        """
        connection.cursor.execute(sql, idAlbum)
        row = connection.cursor.fetchall()
        return row

    def GetTrackByPlaylistId(self,idPlaylist:int):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPC_GetTracksByIdPlaylist]
                    @idPlaylist= ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'
        """
        connection.cursor.execute(sql, idPlaylist)
        row = connection.cursor.fetchall()
        return row

    def GetTrackByIdLibrary(self,idLibrary:int):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPC_GetTracksByIdLibrary]
                    @idLibrary = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'
        """
        connection.cursor.execute(sql, idLibrary)
        row = connection.cursor.fetchall()
        return row

    def DeleteLibraryTrack(self, idLibrary:int, idTrack:int):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DELETE FROM LibraryTrack WHERE idLibrary = ? AND idTrack = ?
        """
        params = (idLibrary, idTrack)
        connection.cursor.execute(sql, params)
        connection.save()
        print("Track has been deleted")
        connection.close()

    def DeletePlaylistTrack(self, idPlaylist:int, idTrack:int):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DELETE FROM LibraryTrack WHERE idPlaylist = ? AND idTrack = ?
        """
        params = (idPlaylist, idTrack)
        connection.cursor.execute(sql, params)
        connection.save()
        print("Track has been deleted")
        connection.close()

    def AddTrackToAlbum(self, idAlbum, newTrack):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPI_Track]
                    @durationSeconds = ?,
                    @title = ?  ,
                    @trackNumber = ?,
                    @storagePath = ?,
                    @idGenre = ?,
                    @idAlbum = ?,
                    @salida = @salida OUTPUT

        SELECT	@salida as N'@salida'
        """
        params = (newTrack.durationSeconds, newTrack.title, newTrack.trackNumber, newTrack.storagePath, newTrack.gender, idAlbum)
        connection.cursor.execute(sql, params)
        connection.cursor.nextset()
        row = int(connection.cursor.fetchval())
        connection.save()
        print(newTrack.title, row)
        return row

    def AddFeaturingTrack(self, idNewTrack, idContentCreator):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPI_FeaturingTrack]
                    @IdTrack = ?,
                    @IdContentCeator = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'
                    """
        params = (idNewTrack, idContentCreator)
        connection.cursor.execute(sql, params)
        connection.cursor.nextset()
        row = connection.cursor.fetchone()
        connection.save()
        return row

    def AddTrackToLibrary(self, idLibrary:int, idTrack:int):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPI_LibraryTrack]
                    @idLibrary = ?,
                    @idTrack = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'   
        """
        params = (idLibrary, idTrack)
        connection.cursor.execute(sql, params)
        connection.save()
        return True

    def AddTrackToPlaylist(self, idPlaylist:int, idTrack:int):
        connection: SQLConnection = SQLConnection()
        connection.open()
        
        sql = """
            EXEC	[dbo].[SPI_TrackPlaylist]
                    @idTrack = ?,
                    @idPlaylist = ?
   
        """
        params = (idTrack, idPlaylist) 
        connection.cursor.execute(sql, params)
        connection.save()
        return True

    def GetTrackByQuery(self, query:str):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            EXEC	[dbo].[SPC_GetTrackByQuery]
		            @query = ?
        """
        connection.cursor.execute(sql, query)
        rows = connection.cursor.fetchall()
        return rows

    def GetLocalTracksByIdConsumer(self, idConsumer:int):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)
            EXEC	@return_value = [dbo].[SPC_GetLocalTracks]
                    @idConsumer = ?,
                    @salida = @salida OUTPUT
        """
        connection.cursor.execute(sql, idConsumer)
        row = connection.cursor.fetchall()
        return row
        
    def AddLocalTrack(self, localTrack):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPI_LocalTrack]
                    @IdConsumer = ?,
                    @fileName = ?,
                    @artistName = ?,
                    @title = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'
        """
        params = (localTrack.idConsumer, localTrack.fileName, localTrack.artistName,localTrack.title)
        connection.cursor.execute(sql,params)
        connection.save()
        return True

        
        

    

    