# Getting Started


# Configure local environment

## Configure Docker
### 1. Generate docker image, docker conntainer and run Migrations:

> cd ApiIntegratedWithDocker\scripts

> ./install-local.ps1

###		1.1. Validate if the container was created

            > docker-compose up -d

> docker-compose ps

Expected return:

```
NAME                                                COMMAND                  SERVICE                   STATUS              PORTS
apiintegratedwithdocker-docker-api-compose-1        "dotnet ApiIntegrate..."   docker-api-compose        running             0.0.0.0:5010->80/tcp, 0.0.0.0:5011->443/tcp
apiintegratedwithdocker-docker-pgadmin-compose-1    "/entrypoint.sh"         docker-pgadmin-compose    running             80/tcp, 443/tcp, 0.0.0.0:16543->5000/tcp
apiintegratedwithdocker-docker-postgres-compose-1   "docker-entrypoint.s..."   docker-postgres-compose   running (healthy)   0.0.0.0:15432->5432/tcp
```


## Configure PgAdmin
### 2. Execute local PgAdmin

Open this url in your browser: http://localhost:16543/login?next=%2F

###		2.1. Sign in to PgAdmin

Connect with the login and password you defined in docker-compose.yml

###		    2.2. Sign in to Citus

Go on 'Object Menu', and select the 'Register - Server' option, then on the 'Name' field, write 'citus'.
Go to the 'Connection' tab and put 'docker-postgres-compose' on the 'Host name/address' field, then press 'Save' button.
On the inputs 'Username' and 'Password' put the configurations that you defined in docker-compose.yml .


# Documentations

https://cakebuild.net/docs/getting-started/setting-up-a-new-scripting-project

https://www.nuget.org/packages/Cake.Docker