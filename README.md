# Workflow Collaboration System — Backend API

This repository contains the ASP.NET Core Web API backend for a workflow collaboration system where users can create projects, collaborate with others, and manage tasks inside each project.

The backend is built using a clean layered structure that separates data access, business logic, and API endpoints.

---

## Features

* User registration and login using JWT authentication
* Create and manage projects
* Add and manage tasks within projects
* Assign tasks to users
* Project collaboration between multiple users
* Activity tracking inside projects
* RESTful API design
* Entity Framework Core with Code-First migrations

---

## Tech Stack

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* Dependency Injection

---

## Project Structure

```
Data/
 ├─ ApplicationDbContext.cs
 └─ Migrations/

Models/
 ├─ User.cs
 ├─ Project.cs
 ├─ TaskStatus.cs
 ├─ Activity.cs
 └─ ViewModels/

Services/
 ├─ JwtService.cs
 ├─ UserService.cs
 ├─ ProjectService.cs
 └─ TaskService.cs

Controllers/
 ├─ LoginController.cs
 ├─ ProjectController.cs
 └─ TaskController.cs
```

---

## Authentication

The API uses JWT Bearer Tokens.

After login, include the token in requests:

Authorization: Bearer {token}

---

## Main Entities

### User

* Can register and log in
* Can create projects
* Can collaborate on projects
* Can be assigned tasks

### Project

* Created by a user
* Contains multiple tasks
* Tracks project activities
* Supports multiple collaborators

### Task

* Belongs to a project
* Assigned to a user
* Has a status defined in `TaskStatus`

### Activity

* Logs important actions inside a project for tracking purposes

---

## Setup and Installation

### 1. Clone the repository

```
git clone [repo-url]
cd [repo-folder]
```

### 2. Configure the database

Edit `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=WorkflowDB;Trusted_Connection=True;"
}
```

### 3. Apply migrations

```
dotnet ef database update
```

### 4. Run the project

```
dotnet run
```

---

## API Endpoints Overview

| Method | Endpoint                | Description             |
| ------ | ----------------------- | ----------------------- |
| POST   | /api/login              | User login              |
| GET    | /api/projects           | Get user projects       |
| POST   | /api/projects           | Create project          |
| GET    | /api/projects/{id}      | Get project details     |
| GET    | /api/tasks/project/{id} | Get tasks for a project |
| POST   | /api/tasks              | Create task             |
| PUT    | /api/tasks/{id}         | Update task             |

---

## Design Notes

* Services layer contains all business logic
* DbContext is isolated inside the Data layer
* ViewModels are used to shape API responses
* JWT is generated using a dedicated JwtService
* Clean separation between layers for maintainability and scalability

---

## Future Improvements

* Notifications system
* Comments on tasks
* Role management inside projects
* Docker support

---

## Author

Ahmad Alrbaeei
Computer Science Student — BAU
.NET Backend Developer
