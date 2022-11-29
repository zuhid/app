#! /bin/bash
clear

# variables
base="zuhid"
resource_group="$base-rg"
location="centralus"
sql_server="$base-sql-server"
sql_server_username="REPLACE"
sql_server_password="REPLACE"
sql_server_firewall_rule="$base-sql-server-firewall-rule"
appservice="$base-app-appservice"

web="$base-app-web"
identity="$base-app-identity"
identity_db="Identity"

# create resource group
az group create \
  --name $resource_group \
  --location $location \
  --output table

# create sql server
az sql server create \
  --resource-group $resource_group \
  --location $location \
  --name $sql_server \
  --admin-user $sql_server_username \
  --admin-password $sql_server_password

# create firewall rule to allow azure apps to access to the database
az sql server firewall-rule create \
  --resource-group $resource_group \
  --server $sql_server \
  --name $sql_server_firewall_rule \
  --start-ip-address 0.0.0.0 \
  --end-ip-address 0.0.0.0

# create appservice plan
az appservice plan create \
  --name $appservice \
  --resource-group $resource_group \
  --location $location \
  --is-linux \
  --sku B1

# create web
az webapp create \
  --resource-group $resource_group \
  --plan $appservice \
  --name $web \
  --runtime "PHP:8.0"

# create identity
az webapp create \
  --resource-group $resource_group \
  --plan $appservice \
  --name $identity \
  --runtime "DOTNETCORE:6.0"

# create identity_db
az sql db create \
  --resource-group $resource_group \
  --server $sql_server \
  --name $identity_db \
  --edition Basic
