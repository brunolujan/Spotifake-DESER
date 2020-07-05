from thrift.Thrift import TProcessor, TMessageType
from thrift.protocol.TProtocol import TProtocolBase
from types import *

SEPARATOR = ":"

class TMultiplexedProcessor(TProcessor):
	def __init__(self):
		self.services = {}

	def registerProcessor(self, serviceName, processor):
		self.services[serviceName] = processor

	def process(self, iprot, oprot):
		(name, type, seqid) = iprot.readMessageBegin()
		if type != TMessageType.CALL & type != TMessageType.ONEWAY:
			raise Exception("This should not have happened!?")

		index = name.find(SEPARATOR)
		if index < 0:
			raise Exception("Service name not found in message name: " + message.name + ". Did you forget to use a TMultiplexProtocol in your client?")

		serviceName = name[0:index]
		call = name[index+len(SEPARATOR):]
		if not self.services.has_key(serviceName):
			raise Exception("Service name not found: " + serviceName + ". Did you forget to call registerProcessor()?")

		standardMessage = (
			call,
			type,
			seqid
		)
		return self.services[serviceName].process(StoredMessageProtocol(iprot, standardMessage), oprot)

class TProtocolDecorator():
	def __init__(self, protocol):
		TProtocolBase(protocol)
		self.protocol = protocol;
	def __getattr__(self, name):
		if hasattr(self.protocol, name):
			member = getattr(self.protocol, name)
			if type(member) in [MethodType, UnboundMethodType, FunctionType, LambdaType, BuiltinFunctionType, BuiltinMethodType]:
				return lambda *args, **kwargs: self._wrap(member, args, kwargs)
			else:
				return member
		raise AttributeError(name)
	def _wrap(self, func, args, kwargs):
		if type(func) == MethodType:
			result = func(*args, **kwargs)
		else:
			result = func(self.protocol, *args, **kwargs)
		return result

class TMultiplexedProtocol(TProtocolDecorator):
	def __init__(self, protocol, serviceName):
		TProtocolDecorator.__init__(self, protocol)
		self.serviceName = serviceName
	def writeMessageBegin(self, name, type, seqid):
		if (type == TMessageType.CALL or
		    type == TMessageType.ONEWAY):
			self.protocol.writeMessageBegin(
			    self.serviceName + SEPARATOR + name,
			    type,
			    seqid
			)
		else:
			self.protocol.writeMessageBegin(name, type, seqid)
            
class StoredMessageProtocol(TProtocolDecorator):
	def __init__(self, protocol, messageBegin):
		TProtocolDecorator.__init__(self, protocol)
		self.messageBegin = messageBegin

	def readMessageBegin(self):
		return self.messageBegin

