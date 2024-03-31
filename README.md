# Hotel Management App
This is a `MVP` that allows costumers to book an available room 
from web and check in costumers for employees using SQL Server/SQLite 
as a database.


## What this repo contains?
* `Dockerfile` with **instruction** to build and **run ready to go** SQL server
* `Class Library` that provide connection to **SQL server**
* Web UI: `ASP .NET Razor Pages`
* `SQLite` database

## Choosing the database
In `appsettings.json` change `DatabaseChoice` to:
* `SQL` to use SQL server from docker image
* `SQLite` to user SQLite server from project folder

### Building SQL Server Docker Container
To build a container use the following code: <br/>
1. `docker pull verconline/hotelmanagment-db:latest` <br/>
2. `docker run -p 11433:1433 -d verconline/hotelmanagment-db:latest`

# Roadmap
* ~~Add instructions to build container~~
* Figure out where to put Dockerfile and StoredProcedures
* ~~Make WPF app~~
* Make readme look viable
* Shove this repo in all the cracks and resume

*Me doing this first project feeling myself as a professio~~a~~nal*
![og_og_1592337712244126716](https://github.com/verc-online/HotelManagement/assets/156561131/99f39daa-c7e9-456e-95d2-c0d22bbc6a3d)
