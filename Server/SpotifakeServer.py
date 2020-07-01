import sys
import SpotifakeServerHandler
sys.path.append("gen-py")
from thrift.transport import TSocket
from thrift.server import TServer
from SpotifakeService import SpotifakeService
from SpotifakeService.ttypes import *

if __name__ == "__main__":
    serverTransport = TSocket.TServerSocket(port=5000)
    processor = SpotifakeService.Processor(SpotifakeServerHandler.SpotifakeServerHandler())
    server = TServer.TSimpleServer(processor, serverTransport)
    print("Starting service...")
    server.serve()
    print("Spotifake Service started.")