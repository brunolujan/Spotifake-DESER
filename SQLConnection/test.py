import sys
sys.path.append("../")
from SQLConnection.sqlServer_consumer import SqlServerConsumer

query: SqlServerConsumer = SqlServerConsumer()

query.GetConsumerById(2)

#query.DeleteConsumer("bruno1529@live.com.mx")