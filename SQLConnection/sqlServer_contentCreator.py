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
