# CRUDApp

A simple console-based Student CRUD application built with **C#**, **.NET 10**, and **ADO.NET** using `Microsoft.Data.SqlClient`.

The app supports:
- Add student
- View students
- Update student
- Delete student

## Tech Stack
- .NET SDK 10 (`net10.0`)
- C# Console App
- SQL Server (SQLEXPRESS in current config)
- `Microsoft.Data.SqlClient` (v6.1.4)

## Project Structure
- `Program.cs` – Main menu and CRUD operations
- `CRUDApp.csproj` – Project configuration and NuGet package references
- `CRUDApp.sln` – Solution file

## Prerequisites
1. Install .NET 10 SDK
2. Install SQL Server / SQL Server Express
3. Ensure SQL Server instance is running (current code uses `FLEEK\\SQLEXPRESS`)

## Database Setup
Create a database named `StudentDB` and table `Students`:

```sql
CREATE DATABASE StudentDB;
GO

USE StudentDB;
GO

CREATE TABLE Students (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Email NVARCHAR(150) NOT NULL
);
```

## Configuration
Open `Program.cs` and update the connection string if needed:

```csharp
static string connectionString = "Data Source=FLEEK\\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=True;Encrypt=False;";
```

Change:
- `Data Source` to your SQL Server instance
- Authentication settings if you are not using Windows Integrated Security

## Run the App
From the project root:

```bash
dotnet restore
dotnet run
```

You will see a menu:
1. Add Student
2. View Students
3. Update Student
4. Delete Student
5. Exit

## Notes
- Inputs for `Id` and `Age` are parsed as integers.
- If invalid text is entered for numeric fields, the app will throw a parse error.
- Build output folders (`bin/`, `obj/`) are ignored via `.gitignore`.
