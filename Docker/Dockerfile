FROM mcr.microsoft.com/mssql/server:2019-latest AS build
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Pwd12345

WORKDIR /tmp

COPY createDatabase.sql . 
COPY createStoredProcedures.sql . 
COPY Script.PostDeployment.sql .

RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 10 \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Pwd12345" -i /tmp/createDatabase.sql \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Pwd12345" -i /tmp/createStoredProcedures.sql \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Pwd12345" -i /tmp/Script.PostDeployment.sql \
    && pkill sqlservr

FROM mcr.microsoft.com/mssql/server:2019-latest AS release
ENV ACCEPT_EULA=Y
COPY --from=build /var/opt/mssql/data /var/opt/mssql/data