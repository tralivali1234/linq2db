#!/bin/bash

#docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Password12!' -p 1433:1433 -h mssql --name=mssql -d mcr.microsoft.com/mssql/server:2017-latest
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Password12!' -p 1433:1433 -h mssql --name=mssql -d marvalsoftware/sqlserverfts:latest
docker ps -a

# Wait for start
echo "Waiting for SQL Server to accept connections"
docker exec mssql /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Password12! -Q "SELECT 1"
is_up=$?
while [ $is_up -ne 0 ] ; do
    docker exec mssql /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Password12! -Q "SELECT 1"
    is_up=$?
done
echo "SQL Server is operational"

docker exec mssql /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Password12! -Q 'CREATE DATABASE TestData;'
docker exec mssql /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Password12! -Q 'CREATE DATABASE TestData2017;'
docker cp scripts/northwind.sql mssql:/northwind.sql
docker exec mssql /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Password12! -i /northwind.sql
