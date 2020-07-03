import sys
sys.path.append("../")
from SQLConnection.sqlServer_consumer import SqlServerConsumer

query: SqlServerConsumer = SqlServerConsumer()

query.GetConsumerById(1)
