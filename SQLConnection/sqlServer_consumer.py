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
        print(row[0].name, row[0].email)

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
        print("Consumer " + newName + " " + newLastName + " has been update")
        self.connection.save()
        self.connection.close()
        


