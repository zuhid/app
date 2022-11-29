build image

```
docker build -t dockeragent:latest .
```

run the container

```
docker container stop dockeragent1
docker container rm dockeragent1
docker run --name dockeragent1 -d -e AZP_URL=https://dev.azure.com/tzather -e AZP_TOKEN=[REPACE_WITH_AZP_TOKEN]-e AZP_AGENT_NAME=dockeragent1 dockeragent:latest

docker exec -it dockeragent1 bash
docker container prune
docker container ls --all
```
