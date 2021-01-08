FROM mcr.microsoft.com/mssql/server:2017-latest

RUN mkdir -p /opt
WORKDIR /opt
COPY ./sql/entrypoint.sh /opt/
COPY ./sql/script.sql /opt/

RUN chmod u+x /opt/entrypoint.sh