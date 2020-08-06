from SQLConnection.connection import SQLConnection

class SqlServerContentCreatorManagement:
    def __init__(self):
        self.connection: SQLConnection = SQLConnection()

    def GetContentCreatorById(self, idContentCreator:int):
        self.connection.open()
        sql = """
            SELECT * FROM ContentCreator WHERE IdContentCreator = ?
        """
        self.connection.cursor.execute(sql, idContentCreator)
        row = self.connection.cursor.fetchall()
        self.connection.save()
        print(row[0].name, row[0].email)
        self.connection.close()

    def GetContentCreatorByEmail(self, email:str):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            SELECT * FROM ContentCreator WHERE email = ?
        """
        connection.cursor.execute(sql, email)
        row = connection.cursor.fetchone()
        if (row != None):
            print(row.email)
            return row.email
            connection.close()
        return None
        connection.close()

    def GetContentCreators(self):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            SELECT * FROM ContentCreator
        """
        connection.cursor.execute(sql)
        row = connection.cursor.fetchall()
        print (row)
        return row

    def GetContentCreatorByStageName(self, stageName:str):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            SELECT * FROM ContentCreator WHERE stageName = ?
        """
        connection.cursor.execute(sql, stageName)
        row = connection.cursor.fetchone()
        if (row != None):
            print(row.stageName)
            return row.stageName
            connection.close()
        return None
        connection.close()

    def GetContentCreatorByEmailPassword(self, email:str, password:str):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            SELECT * FROM ContentCreator WHERE email = ? AND password = ?
        """
        params = (email, password )
        connection.cursor.execute(sql, params)
        row = connection.cursor.fetchone()
        if (row != None):
            print(row.email, row.password)
            return row
            connection.close()
        return None
        connection.close()

    def GetContentCreatorByLibraryId(self, idLibrary):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPC_GetContentCreatorByIdLibrary]
                    @idLibrary = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'
        """
        connection.cursor.execute(sql, idLibrary)
        row = connection.cursor.fetchall()
        return row

    def GetContentCreatorByQuery(self, query:str):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            EXEC	[dbo].[SPC_GetContentCreatorByQuery]
		            @query = ?
        """
        connection.cursor.execute(sql, query)
        rows = connection.cursor.fetchall()
        return rows

    def DeleteContentCreator(self, email:str):
        self.connection.open()
        sql = """
            DELETE FROM ContentCreator WHERE email = ?
        """
        self.connection.cursor.execute(sql, email)
        self.connection.save()
        self.connection.close()

    def UpdateContentCreatorName(self, email:str, currrentPassword:str, newName:str, newLastName:str):
        self.connection.open()
        sql = """
            UPDATE ContentCreator 
            SET name = ?, lastName = ?
            Where email = ? AND password = ?
        """
        params = (newName, newLastName, email, currrentPassword)

        self.connection.cursor.execute(sql, params)
        self.connection.save()
        print("ContentCreator " + newName + " " + newLastName + " has been updated")
        self.connection.close()

    def UpdateContentCreatorPassword(self, email:str, currrentPassword:str, newPassword:str):
        self.connection.open()
        sql = """
            UPDATE ContentCreator
            SET password = ?
            Where email = ? AND password = ?
        """
        params = (newPassword, email, currrentPassword )

        self.connection.cursor.execute(sql, params)
        self.connection.save()
        print("Your password has been updated")
        self.connection.close()

    def UpdateContentCreatorStageName(self, email:str, currrentPassword:str, newStageName:str):
        self.connection.open()
        sql = """
            UPDATE ContentCreator 
            SET stageName = ?
            Where email = ? AND password = ?
        """
        params = (newStageName, email, currrentPassword)

        self.connection.cursor.execute(sql, params)
        self.connection.save()
        print("ContentCreator " + newStageName + " has been updated")
        self.connection.close()

    def UpdateContentCreatorDescription(self, email:str, currrentPassword:str, newDescription:str):
        self.connection.open()
        sql = """
            UPDATE ContentCreator 
            SET description = ?
            Where email = ? AND password = ?
        """
        params = (newDescription, email, currrentPassword)
        self.connection.cursor.execute(sql, params)
        self.connection.save()
        print("ContentCreator description has been updated")
        self.connection.close()

    def UpdateContentCreatorImage(self, email:str, newImageStoragePath:str):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            UPDATE ContentCreator
            SET imageStoragePath = ?
            Where email = ? 
        """
        params = (newImageStoragePath, email)
        connection.cursor.execute(sql, params)
        connection.save()
        print("Your image has been updated")
        connection.close()

    def DeleteLibraryContentCreator(self, idLibrary:int, idContentCreator:int):
        self.connection.open()
        sql = """
            DELETE FROM LibraryContentCreator WHERE idLibrary = ? AND idContentCreator = ?
        """
        params = (idLibrary, idContentCreator)
        self.connection.cursor.execute(sql, params)
        self.connection.save()
        print("Content Creator has been deleted")
        self.connection.close()

    def AddContentCreator(self, newContentCreator):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPI_ContentCreator]
                    @name = ?,
                    @lastname = ?,
                    @stageName = ?,
                    @password = ?,
                    @email = ?,
                    @description = ?,
                    @imageStoragePath = ?,
                    @IdGenre = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'         
        """
        params = (newContentCreator.givenName, newContentCreator.lastName, newContentCreator.stageName, 
            newContentCreator.password, newContentCreator.email, newContentCreator.description, newContentCreator.imageStoragePath, None)
        connection.cursor.execute(sql, params)
        connection.save()
        connection.close()
        print(newContentCreator.stageName, newContentCreator.password)

    def AddContentCreatorToLibrary(self, idLibrary:int, idContentCreator:int):
        connection: SQLConnection = SQLConnection()
        connection.open()
        sql = """
            DECLARE	@return_value int,
                    @salida nvarchar(1000)

            EXEC	@return_value = [dbo].[SPI_LibraryContentCreator]
                    @idLibrary = ?,
                    @idContentCreator = ?,
                    @salida = @salida OUTPUT

            SELECT	@salida as N'@salida'
        """
        params = (idLibrary, idContentCreator)
        connection.cursor.execute(sql, params)
        connection.save()
        return True

