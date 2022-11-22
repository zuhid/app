clear

# define variables
SCRIPT_DIR=$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &>/dev/null && pwd)

build-server() {
  docker container rm --force dev-mssql
  docker run --env "ACCEPT_EULA=Y" --env "SA_PASSWORD=P@ssw0rd" --publish 1433:1433 --name dev-mssql --hostname dev-mssql --detach mcr.microsoft.com/mssql/server:2019-latest
}

build-database() { (
  cd "$SCRIPT_DIR/$1"
  dotnet ef database update 0
  rm -rf Migrations
  dotnet ef migrations add Initial
  dotnet build
  dotnet ef database update
  # dotnet ef migrations script --idempotent --output Migrations/db_script.sql
); }

build-database-log() {
  docker exec dev-mssql /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd -d master -Q "
    if exists (select 1 from sys.databases where name = 'Log')
      drop database Log;
    create database Log;
    use Log;
    create table dbo.Log (
      Id int not null identity,
      Updated datetime2 null,
      EventId nvarchar(100) null,
      LogLevel nvarchar(100) null,
      Category nvarchar(100) null,
      Message nvarchar(max) null,
      constraint PK_Common_Log primary key nonclustered(Id) with (fillfactor = 70),
    )"
}

start-api() { (
  cd "$SCRIPT_DIR/$1"
  dotnet run
); }

# build-server
# build-database "Identity"
build-database-log
# start-api "Identity"
