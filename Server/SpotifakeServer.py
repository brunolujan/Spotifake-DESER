import sys
sys.path.append("gen-py")
from thrift.transport import TSocket
from thrift.server import TServer
from SpotifakeService import #AQUI FALTA
from #AQUI FALTA .ttypes import Result

if __name__ == "__main__":
    serverTransport = TSocket.TServerSocket(port=5000)
    processor = #AQUI FALTA.Processor(#AQUI FALTA())
    server = TServer.TSimpleServer(processor, serverTransport)
    print("Starting service...")
    server.serve()
    print("Spotifake Service started.")