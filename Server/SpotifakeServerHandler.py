import sys
import pyodbc
sys.path.append("gen-py")
from thrift.transport import TSocket
from thrift.server import TServer
from SpotifakeService import #AQUI FALTA
from #AQUI FALTA .ttypes import Result

class SpotifakeServerHandler(SpotifakeServer.Iface):

    conn = pyodbc.connect('Driver={SQL Server}; Server=DESKTOP-R2N3EUU; Database=Spotifake; Trusted_Connection=yes;')
    cursor = conn.cursor()

    def LoginConsumer(email, password):
        
