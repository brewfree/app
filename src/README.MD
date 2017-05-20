# Developer Getting Started
First, thank you for your contributions!  This guide will help you get started on the project.

## Development Machine Setup
Here's a suggested set up, but feel free to use your favorite tools.
- Visual Studio 2017 Community Edition: https://www.visualstudio.com/downloads/ with ASP.NET Web Development Workload
- GitHub Extensions for Visual Studio (extensions and updates)
- NodeJS Version 6 or greater: https://nodejs.org/en/download/
- SPA Templates (command line only template used to create this project)
```
dotnet new --install Microsoft.AspNetCore.SpaTemplates::*
```
-Webpack (from command line): 
```
npm install -g webpack
```

#### Optional Development Machine Setup
- ReSharper
- Angular 2 Snippet Pack
- Bootstrap Snippet Pack
- Markdown Editor

### Configure Secrects
Each developer must create their secrets locally so that we don't store secrets in the code.  Contact the project lead if you need the actual secrets, but you can put in dummy secrets so that the app will run (you won't be able to login with social providers though).

##### Facebook Login Secrets
```
dotnet user-secrets set Authentication:Facebook:AppId [HIDDEN]
dotnet user-secrets set Authentication:Facebook:AppSecret [HIDDEN]
```

##### Microsoft Login Secrets
```
dotnet user-secrets set Authentication:Microsoft:ClientId [HIDDEN]
dotnet user-secrets set Authentication:Microsoft:ClientSecret [HIDDEN]
```

##### Twitter Login Secrets
```
dotnet user-secrets set Authentication:Twitter:ConsumerKey [HIDDEN]
dotnet user-secrets set Authentication:Twitter:ConsumerSecret [HIDDEN] 
```

##### Google+ Login Secrets
```
dotnet user-secrets set Authentication:Google:ClientID [HIDDEN]
dotnet user-secrets set Authentication:Google:ClientSecret [HIDDEN]
```

## Getting Started

### Technologies Used
Here's a list of the major architectural components of the app:
- ASP.NET Core
- Entity Framework Core
- SQL Server
- Angular 2 (or whatever it's called now)
- Node.js
- Azure

### Product Backlog
Where is the list of what needs to be worked on?
- The product backlog is located in [VSTS](https://brewfree.visualstudio.com)

### Committing Code
When you are ready to start development:
- Fork the BrewFree repo
- Create a branch
- Make the changes and submit a pull request
- The project lead will approve and merge the pull request into master

### Builds and Release
Whenever a change is committed in master, a CI build will run that executes tests and publishes to staging.

### Database
Localdb is used in development and Azure SQL is used for staging/production.  Follow these instructions if you need to make a database change.

**IMPORTANT Follow these stps for initial release only.** 

Do not create a migration for every change you make.  We only need 1 migration per release.  We will only have a single migration at launch named "1.0.0"

Open a command window in the project directory.
```
dotnet ef database drop
dotnet ef migrations remove
dotnet ef migrations add "1.0.0" -o "Data\Migrations"
```

To update the database, simply run the project which will also seed it.  If you just want to update the database without data, run this command:
```
dotnet ef database update
```

Migrations and seeding of data is automatic in development, but is disabled in staging and production.  As part of a release, we will need to prepare a SQL script.  The following command will create the script:
```
dotnet ef migrations script --idempotent --output "C:\Dev\BrewFree\Scripts\Release1.sql" --context BrewFree.Data.ApplicationDbContext
```
### API
Swagger is used in development to browse/test the API.  Navigate to https://localhost:44399/api/ to get the swagger interface.

### Webpack
Webpack is used to bundle files and put them in the dist folder.  
Note: it is safe to delete everything in the dist folders in both www/dist and ClientApp/dist and run both the vendor and client webpack to re-create the files.

The first time you run the project run the following command:

```
npm rebuild node-sass --force
```

To rebuild the webpack dist files run this command:
```
npm install && npm run build:dev && dotnet restore
```

## Learning
The following are helpful for learning the technologies used in the application.

### Articles
- [Angular 2 Template for Visual Studio](http://blog.stevensanderson.com/2016/10/04/angular2-template-for-visual-studio/)
- [Webpack](https://angular.io/docs/ts/latest/guide/webpack.html) 

### Pluralsight (subscription required)
- [Using ASP.NET Core to Build Single-page Applications](https://app.pluralsight.com/library/courses/aspnet-core-build-single-page-applications/table-of-contents)

### Markdown
- [Markdown guidance](https://www.visualstudio.com/en-us/docs/reference/markdown-guidance)