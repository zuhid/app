# Setup Devlopment

1. run the file `./setup-local.sh` to setup a docker sql server instanse and deplloy the latest database changes
1. `dotnet run --project Identity` to start the Zuhid.Identity project
1. `npm --prefix Web run start` to start the Web project

```
./setup-local.sh
dotnet run --project Identity
npm --prefix Web run start
```

To Kill already running localhost

```
sudo kill -9 `sudo lsof -t -i:18001`
```
