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

start-api() { (
  cd "$SCRIPT_DIR/$1"
  dotnet run
); }

# build-server
build-database "Identity.Api"
# start-api
