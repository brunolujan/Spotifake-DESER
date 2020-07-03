import pyodbc
from config import DATABASE_SERVER_IP, DATABASE_NAME, DATABASE_USERNAME, DATABASE_PASSWORD

class SQLConnection:

    def __init__(self):
        self.connection = None
        self.cursor = None
        self.server = DATABASE_SERVER_IP
        self.database = DATABASE_NAME
        self.username = DATABASE_USERNAME
        self.password = DATABASE_PASSWORD
        self.connectionQuery = "DRIVER={ODBC Driver 17 for SQL Server}" + ";SERVER={0};DATABASE={1};UID={2};PWD={3}".format(
        self.server, self.database,self.username,self.password)

    def open(self):
        try:
            self.connection = pyodbc.connect(self.connectionQuery)
            self.cursor = self.connection.cursor()

    def close(self):
        self.cursor.close()
        self.connection.close()

    def save(self):
        self.connection.commit()