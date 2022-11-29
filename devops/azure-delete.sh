#! /bin/bash
clear

# variables
base="zuhid"
resource_group="$base-rg"
location="centralus"
sql_server="$base-sql-server"
appservice="$base-app-appservice"
web="$base-app-web"
identity="$base-app-identity"

# delete resource group
az group delete \
  --name $resource_group \
  --yes

# delete swl server
az sql server delete \
  --resource-group $resource_group \
  --name $sql_server \
  --yes

# delete appservice plan
az appservice plan delete \
  --resource-group $resource_group \
  --name $appservice \
  --yes

# delete web
az webapp delete \
  --resource-group $resource_group \
  --name $web

# delete identity
az webapp delete \
  --resource-group $resource_group \
  --name $identity
