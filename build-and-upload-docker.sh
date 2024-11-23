#!/bin/bash
docker build . --no-cache -f ./AutoNaJuz.Web/Dockerfile -t registry.digitalocean.com/auto-na-juz/auto-na-juz-web:debug
docker build . --no-cache -f ./AutoNaJuz.Api/Dockerfile -t registry.digitalocean.com/auto-na-juz/auto-na-juz-api:debug

docker push registry.digitalocean.com/auto-na-juz/auto-na-juz-api:debug
docker push registry.digitalocean.com/auto-na-juz/auto-na-juz-web:debug

scp -r -i ~/.ssh/id_ed25519 nginx/ root@67.207.69.161:./app/
scp -r -i ~/.ssh/id_ed25519 docker-compose.dev/* root@67.207.69.161:./app/