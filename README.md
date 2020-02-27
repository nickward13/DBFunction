# DBFunction
This is an Azure Function for scaling a database down to GP_Gen5_2.  

It relies on two environment variables being set:
* SQLCONN - Azure SQL DB connection string
* DBNAME - Azure SQL DB database name