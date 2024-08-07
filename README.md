# Out Of Office Project

This is a project to demonstrate my programming skills.

## Project Overview

The Out Of Office Project is a demonstration web application designed to showcase my skills in both back-end and front-end development. It simulates a company management system, providing a simple interface for managing various company operations.

[Screenshots with comments.pdf](Screenshots%20with%20comments.pdf)

## Technologies Used

- **Back-end**: ASP.NET Framework, Entity Framework (EF) for database management and CRUD operations.
- **Front-end**: Angular 18.

## Installation Instructions

1. **Clone the Repository**:
    ```sh
    git clone https://github.com/yourusername/out-of-office.git
    cd out-of-office
    ```

   2. **Database Setup**:
       - Modify the connection string for the database in `appsettings.json`  and `appsettings.Development.json` to point to your database instance.
       ```json
       {
         "ConnectionStrings": {
           "DefaultConnection": "YourDatabaseConnectionString"
         }
       }
       ```
      - Apply the migration to create all necessary tables and insert initial data.
       ```sh
       dotnet ef database update
       ```
   
      - Also make you are using PostgreSQL as database. Or change this lines to your needs in `Program.cs`:
       ```csharp
         builder.Services.AddDbContext<ApplicationContext>(
            opt => opt.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")
            ).LogTo(Console.WriteLine, LogLevel.Information)
         );
       ```
      - Modify the secret keys for JWT token in `appsettings.json` and `appsettings.Development.json`.
       ```json
       "JWT": {
       "Audience": "http://localhost:5151",
       "Issuer": "Out-Of-Office",
       "Key": "YourSecretKeyHere",


       "RefAudience": "http://localhost:5151",
       "RefIssuer": "Out-Of-Office-Ref-token",
       "RefKey": "YourSecretKeyHere"
      },
       ```

3. **Run the Back-end**:
    - Use your preferred method to build and run the application.
   
4. **Run the Front-end**:
   - Navigate to the front-end directory and start the Angular development server.
    ```sh
    cd Frontend/AplicationPage
    ng serve
    ```

## Features

- **JWT Authentication**: Secure the application using JSON Web Tokens for authentication.
- **Role-Based Authorization**: Implement role-based access control where each role has specific permissions.
- **Data Validation:** Validate user input to ensure data integrity.
- **CRUD Operations**: Manage various entities with create, read, update, and delete operations.

## Database Schema

<img src="Images/OutOfOffice-DB-schema.png" alt="OutOfOffice-DB-schema" width="300"/>

## Technical Details

- The project is built using a horizontal architecture, separating concerns across different layers and promoting modularity and maintainability.

## Testing

Added file `OutOfOffice.postman_collection.json` with Postman collection for testing API. Import it to your Postman and test API.

## Attention

While backend is supporting full manipulations with data, frontend is still in development. Only Employee table has functionality to edit data!

## Screenshots

Here are some screenshots of the application:

<img src="Images/img.png" alt="Demonstrative image of project" width="300"/>

<img src="Images/img2.png" alt="Demonstrative image of project" width="300"/>

<img src="Images/img3.png" alt="Demonstrative image of project" width="300"/>
