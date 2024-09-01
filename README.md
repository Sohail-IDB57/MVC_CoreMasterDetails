
# MVC_CoreMasterDetails

**MVC_CoreMasterDetails** is an ASP.NET Core MVC project designed to demonstrate a master-detail data management system.
 This project showcases how to build a web application using ASP.NET Core MVC with a focus on handling master-detail relationships efficiently.

## Features

- **Master-Detail Data Model:** Manage and display master and related detail data in a web application.
- **ASP.NET Core MVC:** Utilizes the MVC (Model-View-Controller) pattern for organizing application logic and user interface.
- **Entity Framework Core:** ORM for database operations, providing a seamless way to interact with SQL databases.
- **CRUD Operations:** Create, Read, Update, and Delete operations for both master and detail entities.
- **Responsive Design:** The application includes a responsive UI for a better user experience.

## Technologies

- **ASP.NET Core MVC**: Framework for building the web application with the MVC pattern.
- **Entity Framework Core**: ORM for data access and manipulation.
- **Microsoft SQL Server**: Database provider used for storing and retrieving data.

## Installation and Setup

1. **Clone the Repository**

  
   git clone https://github.com/Sohail-IDB57/MVC_CoreMasterDetails.git
   cd MVC_CoreMasterDetails
   

2. **Configure the Database**

   Open `appsettings.json` and configure your SQL Server connection string under the `ConnectionStrings` section:
   
   "ConnectionStrings": {
     "DefaultConnection": "YourConnectionStringHere"
   }
   

3. **Install Dependencies**

   Ensure you have all required packages installed. Use the .NET CLI to restore them:

   
   dotnet restore
   

4. **Run Migrations**

   Create and apply database migrations using the .NET CLI:

   
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   

5. **Run the Application**

   Start the application:

  
   dotnet run
   

   The application will be accessible at `https://localhost:5001`.

## Usage

- **Master-Detail Management:** Navigate through the application to manage master and detail records. You can create, edit, and delete records as required.
- **UI Navigation:** Use the provided UI to interact with the application and perform CRUD operations.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your enhancements or bug fixes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For any questions or feedback, please open an issue on the [GitHub repository](https://github.com/Sohail-IDB57/MVC_CoreMasterDetails).



