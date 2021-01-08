#!/bin/bash

echo -e "$(date +%F\ %T.%N) \t SQL Server entrypoint.."
/opt/mssql/bin/sqlservr &

until /opt/mssql-tools/bin/sqlcmd -S 127.0.0.1 -U sa -P PassWord123@ -d master -i script.sql; do
echo -e "$(date +%F\ %T.%N) \t Creating database"
sleep 1
done

echo -e "$(date +%F\ %T.%N) \t Created database"