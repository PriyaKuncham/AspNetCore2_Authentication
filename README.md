# AspNetCore2_Authentication
Claim Based Authentication in aspnetcore2.0
Claim Based Authentication
Steps to build application
Open the solution file in Visual Studio.
Change connection string in the appsettings.json file of the web application.
Run the following command for migration and create database.
Tools –> NuGet Package Manager –> Package Manager Console
PM> Add-Migration MyFirstMigration
PM> Update-Database
Run the application.
Running the Sample
Before first time run, uncomment authorise attribute for both controller and action methods in user controller so that we can create a user to access application.
To run the sample, hit F5 or choose the Debug | Start Debugging menu command. You will see the application user list screen. From this screen you have user listing screen as shown in below figure. There are also top menu for the ‘User when clicks on that then same screen opens.

Figure 1: Application User
Now click on "Add User" button to add new application user in the application as per following screen.

Figure 2: Add New Application User
As per figure 1, Edit button uses to edit individual application user as per following figure.

Figure 3: Edit Application User
As per figure 1, Delete button uses to delete individual application role as per following figure.

Figure 4: Delete Application User
Now click on Log In menu button on top the right corner and login with following screen.

Figure 5: Login Application User
Clicks on ‘Log In’ button as claim and policy based show following screen.

Figure 6: Welcome Screen after authentication
If authenticate user is not authorised i.e not assigned a claim to access any resource then shown following screen.

Figure 7: Unauthorised Access
Source Code Overview
Most of folders play same role as in MVC application but there are following more folder and files.
wwwroot: It holds static js and css files.
appsettings.json:It holds database connection string.
ApplicationUser: Custom identity User Class.
Startup.cs:It holds configuration for claims and policy.


