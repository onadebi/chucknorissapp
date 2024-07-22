# This application is the backend that connects to two seperate server endpoints to make calls to StarWars actors and also jokes about chuckNorris.

ChuckNorris Jokes (Chuck): https://api.chucknorris.io/jokes/

Star Wars API (SWAPI) : https://swapi.dev/api/people/

# The application is currently deployed using Github actions from within Azure, and can be access at: https://chucknorrisapp.azurewebsites.net/swagger/index.html
Application was built using .NET 6 (LTS) WebAPI

## Also hosted in Docker container running at: https://chucknorissapp.onrender.com/swagger/index.html

# Steps to Run locally:

> 1. Clone repo from: https://github.com/onadebi/chucknorissapp

> 2. run command **dotnet restore** to restore all nuget packages

> 3. run command **dotnet build**

> 4. run command **dotnet run**
