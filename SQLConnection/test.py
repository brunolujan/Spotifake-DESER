import sys
sys.path.append("../")
from SQLConnection.sqlServer_consumer import SqlServerConsumerManagement

query: SqlServerConsumerManagement = SqlServerConsumerManagement()

#query.GetConsumerById(2)

#query.DeleteConsumer("bruno1529@live.com.mx")

query.UpdateConsumerName("bechecita@bechecita.bechecita", "bechecita", "Victor", "Niño")