# Online Course Platform with Clean Architecture (.NET 8)

This project is an online course platform built with .NET 8, following Clean Architecture principles. It provides a robust and maintainable backend for managing courses, modules, lessons, students, and instructors. This platform offers a RESTful API for potential frontend integration.

## Features

* **User Authentication and Authorization:**
    * Secure user registration and login with password management.
    * JWT (JSON Web Token) authentication for secure API access.
    * Refresh token implementation for extended sessions.
    * Role-based authorization for granular access control.
    * Search for users by name.
* **Course Management:**
    * Create, read, update, and delete courses.
    * Search for courses by title.
* **Module Management:**
    * Create and manage modules within courses.
* **Lesson Management:**
    * Create and manage lessons within modules.
* **User Roles:**
    * Different user roles: **Admin**, **Instructor**, and **Student**.
    * Admin has full control over the platform.
    * Instructors can manage their own courses, modules, and lessons.
    * Students can enroll in courses and view content.

## Roles and Permissions

The platform has three user roles with different levels of access:

* **Students:**
    * Can view courses, enroll in courses, and view course content.
    * Can track their progress.
    
* **Instructors:**
    * Has all the permissions of a Student.
    * Can create, update, and delete their own courses, modules, and lessons.
    * Can view enrolled students.

* **Admin:**
    * Has all the permissions of an Instructor and Student.
    * Can create, update, and delete any course, module, lesson, and user.
    * Can manage all aspects of the platform.

## Technologies Used

* **.NET 8**
* **ASP.NET Core Identity** for user authentication and role management.
* **JWT Authentication** for secure API access.
* **Entity Framework Core** for database management.
* **MediatR** for handling requests and queries in the CQRS pattern.
* **SQL Server** for the database.

## Getting Started

1. **Clone the repository:**
    ```bash
    git clone https://github.com/omar-salah-elshafey/OnlineCoursePlatform.git
    cd OnlineCoursePlatform
    ```
2. **Database Setup:**
    * Configure the database connection string, Email Configuration, and JWT Configuration in `appsettings.json`.
    * Run database migrations to create the necessary tables.
    ```bash
    dotnet ef database update
    ```

3. **Build and Run:**
    * Build the project using your preferred IDE or command-line tools.
    * Run the application:
    ```bash
    dotnet run
    ```

## Usage

* **Registration:** Create a new user account.
* **Login:** Authenticate with your credentials to access the platform.
* **Manage Courses:** Instructors can create, update, and delete their own courses, modules, and lessons.
* **Enroll in Courses:** Students can enroll in courses and view content.
* **Search for Courses:** Search for courses by title using keywords.
* **Search for Users:** Search for Users by Name using keywords.
## Contributing

Contributions are welcome! Please feel free to submit pull requests for bug fixes, new features, or improvements.

![screencapture-localhost-7050-swagger-index-html-2024-12-02-20_14_41](https://github.com/user-attachments/assets/18a4ab71-0027-4899-9aa1-90d72ad8ad3f)
