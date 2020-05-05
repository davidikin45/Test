# Upwork Test API

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

```
.NET Core 3.1
```

### Installing
1. Create a new SQL database by executing scripts/DemoDBScriptQuery.sql
2. Modify appsettings.json DefaultConnection as required. By default it is set to 'Server=(LocalDB)\\MSSQLLocalDB;Database=Demo;Trusted_Connection=True;MultipleActiveResultSets=true;'
3. Run the following from a command line:
```
dotnet run --project src\Test.csproj
```
4. Direct to https://localhost:5001/stats to view summary of jobs stats per room type

## Authors

* **Dave Ikin** - [davidikin45](https://github.com/davidikin45)

## License

This project is licensed under the MIT License