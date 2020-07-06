from SQLConnection.connection import SQLConnection

class SqlServerConsumerManagement:
    def __init__(self):
        self.connection: SQLConnection = SQLConnection()

    def GetConsumerById(self, idConsumer:int):
        self.connection.open()
        sql = """
            SELECT * FROM Consumer WHERE IdConsumer = ?
        """
        self.connection.cursor.execute(sql, idConsumer)
        row = self.connection.cursor.fetchall()
        self.connection.save()
        print(row[0].name, row[0].email)
        self.connection.close()

    def DeleteConsumer(self, email:str):
        self.connection.open()
        sql = """
            DELETE FROM Consumer WHERE email = ?
        """
        self.connection.cursor.execute(sql, email)
        self.connection.save()
        self.connection.close()

    def UpdateConsumerName(self, email:str, currrentPassword:str, newName:str, newLastName:str):
        self.connection.open()
        sql = """
            UPDATE Consumer 
            SET name = ?, lastName = ?
            Where email = ? AND password = ?
        """
        params = (newName, newLastName, email, currrentPassword)

        self.connection.cursor.execute(sql, params)
        self.connection.save()
        print("Consumer " + newName + " " + newLastName + " has been updated")
        self.connection.close()

    def UpdateConsumerPassword(self, email:str, currrentPassword:str, newPassword:str):
        self.connection.open()
        sql = """
            UPDATE Consumer
            SET password = ?
            Where email = ? AND password = ?
        """
        params = (newPassword, email, currrentPassword )

        self.connection.cursor.execute(sql, params)
        self.connection.save()
        print("Your password has been updated")
        self.connection.close()
        


