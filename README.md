# MovieApplication

This is a simple Web API using ASP.NET 5.0 and EntityFramework Core.

![](https://img.shields.io/github/checks-status/rafaelstarling/movie_app/master)

## Important
**Database Initialization** file is under MovieApplication.Infrastructure.Resources.movielist.csv.
If you want to overwrite it, set build action as "None" and "Copy to Output Directory" as "Copy always".

## Prerequirements

* Visual Studio 2019
* .NET 5.0

## How To Run

* Open solution in Visual Studio
* Set MovieApplication project as Startup Project and build the project.
* Run the application.

## How To Run Tests

* Open solution in Visual Studio
* Click on Test > Run All Tests.

## How To Run Without Visual Studio

* Open Command Prompt
* Move to project root folder

	`cd C:/path/to/root/MovieApplication`

* Run command

	`dotnet run --project MovieApplication`

* Open [http://localhost:5000](http://localhost:5000)

## How To Run Tests Without Visual Studio

* Open Command Prompt
* Move to project root folder

	`cd C:/path/to/root/MovieApplication`

* Run command

	`dotnet test`
