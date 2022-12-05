-- To run this script against a local Docker image:
-- docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=p@ssw0rd" -p 1433:1433 --name mssql2022 -d mcr.microsoft.com/mssql/server:2022-latest
-- docker cp create-tikitapp-database.sql mssql2022:/opt/create-tikitapp-database.sql
-- docker exec -it mssql2022 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P p@ssw0rd -i /opt/create-tikitapp-database.sql

PRINT @@VERSION
GO
CREATE DATABASE [tikitapp] COLLATE Latin1_General_CI_AI
GO
CREATE LOGIN [tikitapp_user] WITH 
	PASSWORD=N'tikitapp_password', 
	DEFAULT_DATABASE=[tikitapp], 
	CHECK_EXPIRATION=OFF, 
	CHECK_POLICY=OFF
GO
USE [tikitapp]
GO
PRINT 'Adding user [tikitapp_user] to database [tikitapp]'
CREATE USER [tikitapp_user] FOR LOGIN [tikitapp_user]
PRINT 'Done.'
GO
PRINT 'Adding user [tikitapp_user] to role [db_owner] in [tikitapp] database'
ALTER ROLE [db_owner] ADD MEMBER [tikitapp_user]
PRINT 'Done'
GO