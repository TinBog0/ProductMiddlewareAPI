# Product Middleware API

## Project Overview

This project is a middleware API designed to fetch products from different sources. The current implementation fetches products from a provided REST API and includes advanced filtering capabilities.

## Features

- Retrieve a list of products with image, name, price, and a short description.
- Retrieve detailed information for a specific product.
- Filter products by category and price range.
- Search for products by name.
- Implemented logging for better traceability and debugging.
- Implemented caching to optimize performance for repeated queries.

## Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- AutoMapper
- Swagger for API documentation
- MemoryCache for caching
- Logging with built-in ASP.NET Core logging framework

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio or any other C# IDE

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/TinBog0/ProductMiddlewareAPI.git
   cd ProductMiddlewareAPI
   
3. **Set up the project:**

Open the solution in Visual Studio.

3. **Install dependencies:**

Open the Package Manager Console and run: Update-Package


## The configuration settings can be found in appsettings.json.

### Build and run the application

## Access the API documentation:

Open your browser and navigate to https://localhost:5007/swagger to view the Swagger UI.

### Authentication
I attempted to implement JWT authentication to secure the endpoints. However, I encountered issues with the signing key provided by the external authentication service(DummyJSON). Despite multiple configurations and debugging attempts, the validation of the JWT token's signature could not be completed successfully.

### Known Issue
JWT Signature Validation: The JWT token validation fails due to the absence of a valid signing key. This configuration bypasses signature validation, which is not recommended for production environments.

## Additional Features
Implemented caching to optimize performance.
Implemented detailed logging (Info, Warning, Error) using the built-in logging framework.
