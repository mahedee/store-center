# StoreCenter - Inventory Management System

StoreCenter is an open-source inventory management system built with ASP.NET Core Web API, React.js and SQL Server . It helps businesses efficiently manage stock, track inventory, and generate reports.

<!-- Add an image here: It can be home page or any feature page -->

## Features
- Add, update, and delete inventory items
- Real-time stock tracking and notifications
- User authentication and role-based access control
- Generate detailed inventory and sales reports
- Barcode scanning and printing
- Supplier and purchase order management
- Multi-location inventory management
- Integration with accounting software
- Customizable inventory alerts and thresholds
- Import and export inventory data
- Audit trails and transaction history
- Mobile-friendly interface
- API for third-party integrations
- Data backup and restore functionality
- Comprehensive dashboard and analytics
- Multi-language support
- Customer management and order tracking
- Inventory forecasting and demand planning
- Batch and expiry date tracking
- Support for various units of measurement
- Inventory valuation methods (FIFO, LIFO, etc.)
- Customizable user permissions and settings
- Integration with e-commerce platforms
- Inventory reconciliation and adjustment tools
- Detailed documentation and support
- Regular updates and feature enhancements

## Prerequisites
Before you begin, ensure you have the following installed:
- .NET SDK (Version: 7.0 or higher)
- Node.js (Version: 18 or higher)
- SQL Server (or any supported database)
- Git

## Getting Started
### Installation
Follow these steps to set up StockCenter on your local machine:

*  Clone the repository:
   ```bash
   git clone https://github.com/mahedee/store-center.git
   cd store-center
   ```

*  Install backend dependencies:
   ```bash
   cd backend/StoreCenter/StoreCenter.API
   dotnet restore
   
   cd backend/StoreCenter/StoreCenter.Application
   dotnet restore
   
   cd backend/StoreCenter/StoreCenter.Domain
   dotnet restore

   cd backend/StoreCenter/StoreCenter.StoreCenter.Infrastructure
   dotnet restore

   ```

* Install frontend dependencies:
   ```bash
   cd StoreCenter.Client
   npm install
   ```

*  Set up the database:
   - Create a database name - IMSDB in SQL Server.
   - Update the connection string in `appsettings.json` in the StoreCenter.API project.

* Configure the backend application:
   - Update the `appsettings.json` file with your database connection string and other environment-specific configurations.
   - Example configuration:
   ```json
   {
       "ConnectionStrings": {
           "DefaultConnection": "Server=.;Database=IMSDB; User Id = sa; Password = YourPassword; TrustServerCertificate=True;"
       }
   }
   ```

*  Apply migrations to the database:
   ```bash
   cd StoreCenter.API
   dotnet ef database update
   ```

* Start the backend server:
   ```bash
   cd backend/StoreCenter/StoreCenter.API
   dotnet run
   ```

   Alternatively, to run with HTTPS profile:

   ```bash
   cd backend/StoreCenter/StoreCenter.API
   dotnet run --launch-profile https
   ```
  You can access the application using swagger UI at `https://localhost:5001/swagger/index.html` or `http://localhost:5100/swagger/index.html` depending on your launch profile.

* Configure the frontend application:
   - Update the `apiUrl` in `src/config.js` with the backend URL.
   - Example configuration:
   ```javascript
   const apiUrl = 'https://localhost:5001/api';
   export default apiUrl;
   ```

* Start the frontend development server:
   ```bash
   cd StockCenter.Client
   npm start
   ```

* Open your browser and navigate to `http://localhost:3000`.



## Usage
After setting up, log in to the application using the following credentials:
- Username: admin
- Password: P@ssW0rd

Start managing your inventory. Refer to the documentation (link if applicable) for detailed usage instructions.

## Contributing
We welcome contributions! To contribute:
1. Fork the repository.
2. Create a feature branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add your message here"
   ```
4. Push to your branch:
   ```bash
   git push origin feature/your-feature-name
   ```
5. Open a pull request on GitHub.

## License
This project is licensed under the MIT License. See `LICENSE` for details.

## Contact
For questions or feedback, contact:
- __Maintainers__: 
    - Name: Mahedee Hasan
    - Email: mahedee.hasan@gmail.com
    - GitHub: [Mahedee](https://github.com/mahedee)
    - Website: [mahedee.net](https://mahedee.net)
- Slack: [Slack Channel](https://slack.com)
- Forum: [Forum](https://forum.com)
- WhatsApp: [WhatsApp Group](https://whatsapp.com)

<!-- How to add Contributors in github -->

## Acknowledgments
Special thanks to all contributors and the open-source community for their support.
