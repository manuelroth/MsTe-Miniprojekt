## MsTe Lecture Project

### Usage 

1. Open Visual Studio
2. Select Open from Source Control
3. Select Clone and paste the url to this repository
4. Double Click on the solution AutoReservation.sln

In order to run the application you need a connection to a MS SQL Database. Make sure that you have LocalDB installed. If not download it [here](https://www.microsoft.com/de-ch/download/details.aspx?id=42299) During the installation process a new database with the name MSSQLLocalDB gets automatically created.

5. Go to View -> SQL Server Object Explorer -> Add SQL Server
6. Enter the following server name: (localdb)\MSSQLLocalDB and click connect
7. Next open and run the file AutoReservation.Database_Create_Script.sql which creates the necessary database tables
8. Now you should be ready to run the application

