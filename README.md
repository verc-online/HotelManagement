# Hotel Management App
This is a `MVP` that allows costumers to book an available room 
from web and check in costumers for employees using SQL Server/SQLite 
as a database.


## What this repo contains?
* `Dockerfile` with **instruction** to build and **run ready to go** SQL server
* `SQLite` database - HotelAppDb.db
* `Class Library` that provide connection to **SQL server** - HotelAppLibrary 
* Web UI: `ASP.NET Razor Pages` - HotelApp.Web
* Desktop App: `WPF` - HotelAppDesktop

## Choosing the database
* To user SQL Server:
* * In `appsettings.json` change `DatabaseChoice` to `SQL` to use SQL server from docker image
* To use SQLite:
* * In `appsettings.json` change `DatabaseChoice` to 
 `SQLite` to use SQLite server from project folder
 * * Change `ConnectionStrings`:`SqliteDB` path to SQLite path.

### Building SQL Server Docker Container
To build a container use the following code: <br/>
1. `docker pull verconline/hotelmanagment-db:latest` <br/>
2. `docker run -p 11433:1433 -d verconline/hotelmanagment-db:latest`

# Roadmap
* ~~Add instructions to build container~~
* ~~Add instructions to connectionstring in sqlite~~
* Figure out where to put Dockerfile and StoredProcedures
* ~~Make WPF app~~
* Make readme look viable
* Shove this repo in all the cracks and resume

## ToDo WPF
* Add confirmation message after guest checked in
* Show status of reservation
* Let employee change the date to look for reservations
* Let employee to see all reservations
* Let employee wipe all bookings 
* Place them tightly to make an employee butt onetimes flame

## ToDo ASP.Net
* Add some css styles
* Add authentication
* Let guests see their bookings
* Let guests undo their bookings
* Add feedback page

 \*Me doing this first project feeling myself as a professio~~a~~nal
![og_og_1592337712244126716](https://github.com/verc-online/HotelManagement/assets/156561131/99f39daa-c7e9-456e-95d2-c0d22bbc6a3d)
