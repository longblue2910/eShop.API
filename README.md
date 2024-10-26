## Ecommerce Microservices:

## Prepare environment

* Install dotnet core version in file `global.json`
* Visual studio 2022+
* Docker desktop
---
## How to run the project

Run command for build project
```Powershell	
donet build
```

Go to folder contain file `docker-compose`

1. Using docker-compose
```Powershell
docker-compose down -v
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build
```

## Applications URLs - LOCAL Environment (Docker Container):
- Product API: http://localhost:6002/api/products
- Customer API: http://localhost:6003/api/products
- Basket API: http://localhost:6004/api/products



## Docker Applications URLs - LOCAL Environment (Docker Container)
- Portainer: http://localhost:9000 - username: admin | password: tienlong291099
- Kibana: http://localhost:5601 - username: elastic | password: admin
- RabbitMQ: http://localhost:15672 - username: guest | password: guest

2. Using Visual Studio 2022
- Open aspnetcore-microservice.sln - `aspnetcore-microservice.sln`
- Run Compound to start multi projects
---
## Applications URLs - DEVELOPMENT Environment:
- Product API: http://localhost:5002/api/products


---
## Applications URLs - PRODUCTION Environment:


---
## Pakages References


## Install Environment

- https://dotnet.microsoft.com/download/dotnet/6.0
- https://visualstudio.microsoft.com/	

## References URLs
- https://github.com/jasontaylordev/CleanArchitecture

## Docker Commands:
- docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build

## Useful Command

- ASPNETCORE_ENVIRONMENT=Development dotnet ef database update
- donet watch run --environment "Development"
- donet restore
- donet build
- Migration commands:
	- dotnet ef migrations add "SampleMigration" -p {project dir} --startup-project {project dir} -o {project dir}\Migrations
	- CD into Ordering folder: dotnet ef migrations remove --project Ordering.Infrastructure --startup-project Ordering.API
	- dotnet ef database update -p Ordering.Infrastructure --startup-project Ordering.API