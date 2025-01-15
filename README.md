# Family Management Application

This repository contains a comprehensive application for managing family data. The application is divided into several APIs, each responsible for different aspects of the family management system. Below is an overview of the entire application, including the technologies used, communication methods, and main functionalities of each API.

## Overview

The Family Management Application is designed to help users manage family information, including relatives, houses, and family trees. The application is built using modern web technologies and follows a microservices architecture.

# Reference

Guided by ASP.NET Core Enterprise Applications by Eduardo Pires, in his project we created an e-commerce and in my idea I wanted to transpose what I learned in the course, so I created a system that manages the Genealogical Tree, Hidden Friend (Christmas), List of desires among others.

## Technologies Used
- **.NET 8.0**: Development platform.
- **C# 12.0**: Programming language.
- **Blazor**: Framework for building interactive web UIs.
- **RESTful API**: Comunication betwen some API's
- **RabbitMq**: Comunication betwen some API's 
- **FluentValidation**: Library for data validation.
- **Entity Framework Core**: ORM for database access.
- **IdentityServer4**: OpenID Connect and OAuth 2.0 framework for ASP.NET Core.
- **JWT (JSON Web Tokens)**: Standard for securely transmitting information between parties as a JSON object.
- **Bootstrap**: CSS framework for responsive design.
- **jQuery**: JavaScript library for DOM manipulation and AJAX requests.

## Projects

### FML.WebApp.MVC
The FML.WebApp.MVC project is a web application built using the Model-View-Controller (MVC) pattern. It serves as the front-end for the FML system, providing a user interface for interacting with the various services.

#### Technologies Used
- **ASP.NET Core MVC**: Framework for building web applications using the MVC pattern.
- **Razor**: View engine for rendering HTML in ASP.NET Core MVC.
- **Entity Framework Core**: ORM for database access.
- **Bootstrap**: CSS framework for responsive design.
- **jQuery**: JavaScript library for DOM manipulation and AJAX requests.

#### Key Features
- User authentication and authorization.
- CRUD operations for managing entities.
- Responsive design for mobile and desktop devices.

### FML.Identidade.API
The FML.Identidade.API project is responsible for handling authentication and authorization within the FML system. It provides endpoints for user login, registration, and token management.

#### Technologies Used
- **ASP.NET Core Web API**: Framework for building RESTful APIs.
- **IdentityServer4**: OpenID Connect and OAuth 2.0 framework for ASP.NET Core.
- **Entity Framework Core**: ORM for database access.
- **JWT (JSON Web Tokens)**: Standard for securely transmitting information between parties as a JSON object.
- **FluentValidation**: Library for validating API request data.

#### Key Features
- User registration and login.
- Token-based authentication using JWT.
- Role-based authorization.
- Secure password storage and management.

### FML.Familiares.API
The FML.Familiares.API project manages family member information.

#### Technologies Used
- **ASP.NET Core Web API**: Framework for building RESTful APIs.
- **Entity Framework Core**: ORM for database access.
- **FluentValidation**: Library for validating API request data.

#### Key Features
- CRUD operations for family members.
- Data validation using FluentValidation.

### FML.Evento.API
The FML.Evento.API project manages events within the FML system.

#### Technologies Used
- **ASP.NET Core Web API**: Framework for building RESTful APIs.
- **Entity Framework Core**: ORM for database access.
- **FluentValidation**: Library for validating API request data.

#### Key Features
- CRUD operations for events.
- Data validation using FluentValidation.

### FML.File.API
The FML.File.API project handles file management within the FML system.

#### Technologies Used
- **ASP.NET Core Web API**: Framework for building RESTful APIs.
- **Entity Framework Core**: ORM for database access.
- **FluentValidation**: Library for validating API request data.

#### Key Features
- File upload and download.
- Data validation using FluentValidation.

### Other APIs
The FML API Suite may include additional APIs for various functionalities. These APIs typically use the same core technologies and patterns as described above.

## How to Run
1. Clone the repository.
2. Navigate to the project directory.
3. Restore dependencies: `dotnet restore`.
4. Run the project: `dotnet run`.

## Contribution
Contributions are welcome! Feel free to open issues and pull requests.

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

---

*This README was generated using GitHub Copilot.*

