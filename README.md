## Tedu AspnetCore Microservices:
A large numerous developers have heard about microservices and how it is the next big thing. In any case, for some developers I have coporate with, microservices is simply one more popular expression like DevOps. I have been dealing with various tasks involving microservices for somewhat more than a year now and here, I might want to discuss the hypothesis and the thoughts behind the idea. I built this course to help developers narrow down your challenges with my reality experiences.

- Microservice Course : [https://tedu.com.vn/khoa-hoc](https://tedu.com.vn/khoa-hoc/xay-dung-he-thong-voi-kien-truc-micro-service-49.html)
- Facebook Group: [https://www.facebook.com/groups/](https://www.facebook.com/groups/learnmicroservices)
- Slides: [Section 1](https://tedu.com.vn/uploaded/files/slides/062022/Xay%20dung%20he%20thong%20voi%20Microservice.pdf)

## Prepare environment

* Install dotnet core version in file `global.json`
* IDE: Visual Studio 2022+, Rider, Visual Studio Code
* Docker Desktop

## Warning:

Some docker images are not compatible with Apple Chip (M1, M2). You should replace them with appropriate images. Suggestion images below:
- sql server: mcr.microsoft.com/azure-sql-edge
- mysql: arm64v8/mysql:oracle
---
## How to run the project

Run command for build project
```Powershell
dotnet build
```
Go to folder contain file `docker-compose`

1. Using docker-compose
```Powershell
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans
```

## Application URLs - LOCAL Environment (Docker Container):
- Product API: http://localhost:6002/api/products

## Docker Application URLs - LOCAL Environment (Docker Container):
- Portainer: http://localhost:9000 - username: admin ; pass: admin1234
- Kibana: http://localhost:5601 - username: elastic ; pass: admin
- RabbitMQ: http://localhost:15672 - username: guest ; pass: guest

2. Using Visual Studio 2022
- Open aspnetcore-microservices.sln - `aspnetcore-microservices.sln`
- Run Compound to start multi projects
---
## Application URLs - DEVELOPMENT Environment:
- Product API: http://localhost:5002/api/products
---
## Application URLs - PRODUCTION Environment:

---
## Packages References

## Install Environment

- https://dotnet.microsoft.com/download/dotnet/6.0
- https://visualstudio.microsoft.com/

## References URLS

## Docker Commands: (cd into folder contain file `docker-compose.yml`, `docker-compose.override.yml`)

- Up & running: 
```Powershell
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build
```
- Stop & Removing: 
```Powershell
docker-compose down
```

## Useful commands:

- ASPNETCORE_ENVIRONMENT=Production dotnet ef database update
- dotnet watch run --environment "Development"
- dotnet restore
- dotnet build