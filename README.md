# Product Inventory Management API

---

##  Project Overview

This project is a **RESTful Web API** built using **ASP.NET Core** and **Entity Framework Core** to manage product inventory.

It supports full **CRUD operations** along with **JWT Authentication** and **Role-Based Authorization**, ensuring secure access to API endpoints.

---

##  Features

*  JWT Authentication
*  Role-Based Authorization (Admin, Manager, Viewer)
*  CRUD Operations for Products
*  Entity Framework Core with SQL Server
*  Swagger UI for API testing
*  Input validation and error handling

---

##  Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT (JSON Web Tokens)
* Swagger (Swashbuckle)

---

##  Roles & Permissions

| Role    | Permissions                          |
| ------- | ------------------------------------ |
| Admin   | Full access (GET, POST, PUT, DELETE) |
| Manager | GET, POST, PUT                       |
| Viewer  | GET only                             |

---

##  Project Structure

```
ProductInventoryManagement/

├── Controllers/
│   ├── AuthController.cs
│   └── ProductsController.cs
│
├── Models/
│   ├── Product.cs
│   └── User.cs
│
├── Data/
│   └── ProductDbContext.cs
│
├── Migrations/
├── Properties/
│
├── Program.cs
├── appsettings.json
└── ProductInventoryManagement.csproj
```

---

##  Setup Instructions

1. Clone the repository

```
git clone <your-repo-link>
```

2. Open the project in Visual Studio

3. Update database connection string in:

```
appsettings.json
```

4. Apply migrations:

```
Update-Database
```

5. Run the project

6. Open Swagger:

```
https://localhost:<port>/swagger
```

---

##  API Endpoints

###  Authentication

* **POST** `/api/Auth/login` → Get JWT Token

---

###  Products

| Method | Endpoint           | Access         |
| ------ | ------------------ | -------------- |
| GET    | /api/Products      | All roles      |
| GET    | /api/Products/{id} | All roles      |
| POST   | /api/Products      | Admin, Manager |
| PUT    | /api/Products/{id} | Admin, Manager |
| DELETE | /api/Products/{id} | Admin only     |

---

##  Sample Request (POST)

```json
[
  {
    "name": "Mobile",
    "category": "Electronics",
    "price": 40000,
    "stockQuantity": 5
  }
]
```

---

##  JWT Configuration

Add in `appsettings.json`:

```json
"Jwt": {
  "Key": "ThisIsMySuperSecretKey12345",
  "Issuer": "ProductAPI",
  "Audience": "ProductAPIUsers",
  "DurationInMinutes": 60
}
```

---

##  Testing using Swagger

1. Run the project
2. Open Swagger UI
3. Call `/api/Auth/login`:

```json
{
  "username": "admin",
  "password": "admin123"
}
```

4. Copy the token
5. Click **Authorize**
6. Enter:

```
Bearer YOUR_TOKEN
```

7. Test APIs

---

## Test Scenarios (Completed)

* ✔ GET without token → **401 Unauthorized**
* ✔ Viewer tries DELETE → **403 Forbidden**
* ✔ Admin DELETE → **204 No Content**
* ✔ Manager POST → **Success**
* ✔ Manager DELETE → **403 Forbidden**
* ✔ JWT authentication working correctly
* ✔ Role-based authorization implemented

---

## 🛠️ Validation & Error Handling

* Returns **400 BadRequest** for invalid input
* Returns **404 NotFound** if product does not exist
* Ensures price is greater than zero

---

##  Key Concepts Used

* Dependency Injection
* Entity Framework Core (Code First)
* REST API Design
* Asynchronous Programming (async/await)
* JWT Authentication
* Role-Based Authorization

---

##  Learning Outcome

* Built secure APIs using JWT
* Implemented role-based access control
* Integrated Swagger with authentication
* Handled HTTP status codes (401, 403, 204)

---



## ⭐ Future Enhancements

* Add refresh tokens
* Add frontend UI
* Deploy to cloud (Azure/AWS)

---




