from SQLConnection.connection import SQLConnection

class SqlServerConsumer:
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