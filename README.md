# Online Course Platform with Clean Architecture (.NET 8)

This project is a robust online course platform built with .NET 8, following the principles of Clean Architecture and the CQRS pattern. 
It provides features for course management, user management, and enrollment tracking, with a secure and scalable backend implementation.

---

## Features

### Authentication and Authorization
- Secure user registration and login with role-based functionality (Admin, Instructor, Student).
- JWT (JSON Web Token) authentication for secure API access.
- Refresh token implementation for extended sessions.
- Email confirmation for new user accounts.
- Password management, including:
  - Changing passwords using the old password.
  - Resetting forgotten passwords with a verification code sent via email.

### User Management
- View all users or filter by role (Students, Instructors).
- Search for users by keywords in `UserName`, `FirstName`, or `LastName`.
- Roles:
  - **Admin**: Full access to all features and data.
  - **Instructor**: Manage their own courses, modules, lessons, and enrollments.
  - **Student**: Enroll in courses and track their progress.

### Course Management
- **CRUD Operations**:
  - Admins and Instructors can create, update, and delete courses.
  - Admins can manage any course.
  - Instructors can manage their own courses.
  - Students can view all courses and enroll in them.
- **Search**:
  - Search for courses by name or keyword.
- Specific endpoints:
  - Get all courses.
  - Get course by ID.
  - Get courses by Instructor ID.

### Module Management
- **CRUD Operations**:
  - Admins and Instructors can create, update, and delete modules.
  - Admins can manage any module.
  - Instructors can manage modules related to their own courses.
  - Students can view modules.
- Specific endpoints:
  - Get all modules.
  - Get module by ID.
  - Get modules by Course ID.

### Lesson Management
- **CRUD Operations**:
  - Admins and Instructors can create, update, and delete lessons.
  - All users can view lessons.
- Specific endpoints:
  - Get all lessons.
  - Get lesson by ID.
  - Get lessons by Module ID.

### Enrollment Management
- **CRUD Operations**:
  - Admins and Instructors can enroll students in courses, update enrollments, and delete enrollments.
  - Students can enroll themselves (not others) and update their progress.
- **Validation**:
  - Prevent duplicate enrollments.
  - Provide feedback if a student is already enrolled.
- Specific endpoints:
  - Get all enrollments.
  - Get enrollment by ID.
  - Get enrollments by Student ID.
  - Get enrollments by Course ID.

---

## Project Structure

The project adheres to Clean Architecture principles and is divided into four layers:

1. **Domain**:
   - Core business logic and entities.
   - Example: `User`, `Course`, `Module`, `Lesson`, `Enrollment`.

2. **Application**:
   - Business logic, DTOs, services, commands, queries, and response models.
   - Implements the CQRS (Command Query Responsibility Segregation) pattern.

3. **Infrastructure**:
   - Database context and migrations.
   - External services like email.
   - Dependency injection.

4. **API**:
   - Controllers for handling HTTP requests.
   - Route configurations and project entry point.

---

## Technologies Used

- **.NET 8**
- **ASP.NET Core Identity**
- **JWT Authentication**
- **Entity Framework Core**
- **SQL Server Database**
- **Clean Architecture**
- **CQRS Pattern**
- **MediatR** (for request handling)
- **Logger** (to log info, warnings, and errors to the console)

---

## Getting Started

### Prerequisites
- .NET SDK 8.0+
- SQL Server

### Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/omar-salah-elshafey/OnlineCoursePlatform

### Configure the Application
1. Update the `appsettings.json` file with:
   - **Database Connection String**: Provide the connection string for your SQL Server database.
   - **Email Service Configuration**: Add your email service settings for functionalities like email confirmation and password reset.
   - **JWT Settings**: Configure JWT settings, including secret keys and token expiration values.

### Set Up the Database
- Apply migrations to create the database schema:
  ```bash
  dotnet ef database update

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

### Authentication
- **Register**: Create an account as an Admin, Instructor, or Student with email confirmation.
- **Login**: Log in with your credentials to obtain a secure JWT and refresh token for API access.

### User Management
- **View Users**: Retrieve a complete list of users or filter them by role (Admin, Instructor, Student).
- **Search Users**: Search for users by keywords matching `UserName`, `FirstName`, or `LastName`.

### Course, Module, and Lesson Management
- **Admins and Instructors**:
  - Create, update, and delete courses, modules, and lessons.
  - Instructors can manage only the content they own.
- **Students**:
  - View details of available courses, modules, and lessons.
  - Interact with these entities through dedicated endpoints.

### Enrollment Management
- **Admins and Instructors**:
  - Enroll students in courses.
  - Update or delete existing enrollments.
- **Students**:
  - Self-enroll in courses.
  - Update their own enrollment details to track progress.

---

## Repository

The project is available on GitHub:  
[Online Course Platform Repository](https://github.com/omar-salah-elshafey/OnlineCoursePlatform)

---

## Contribution

Contributions are highly encouraged! If you have suggestions or ideas for improvement.

Feel free to reach out for questions or collaboration opportunities.  
Enjoy exploring and enhancing the Online Course Platform!


![screencapture-localhost-7050-swagger-index-html-2024-12-02-20_14_41](https://github.com/user-attachments/assets/18a4ab71-0027-4899-9aa1-90d72ad8ad3f)
