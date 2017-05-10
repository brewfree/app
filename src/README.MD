Developer Setup
Visual Studio 2017 Community Edition: https://www.visualstudio.com/downloads/
Visual Studio 2017 ASP.NET and Web Development
GitHub Extensions for Visual Studio (extensions and updates)
NodeJS Version 6 or greater: https://nodejs.org/en/download/
Install SPA Templates from command line: dotnet new --install Microsoft.AspNetCore.SpaTemplates::*
Add secrets to be able to login
(Optional) ReSharper
(Optional) Angular 2 Snippet Pack

To Update the data model (for initial release only)
Delete everything in the migrations folder
Open a command prompt at the BrewFree project path
dotnet ef database drop
dotnet ef migrations remove
dotnet ef migrations add "Initial" -o "Data\Migrations"
dotnet ef database update (or just run the project which will also seed the data)

To create a script
dotnet ef migrations script --idempotent --output "C:\Dev\BrewFree\app\src\BrewFree\obj\Debug\netcoreapp1.1\PubTmp\EFSQLScripts\BrewFree.Data.ApplicationDbContext.sql" --context BrewFree.Data.ApplicationDbContext

Learn about mark down
https://www.visualstudio.com/en-us/docs/reference/markdown-guidance