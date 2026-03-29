<img width="1038" height="706" alt="dashboard" src="https://github.com/user-attachments/assets/70438983-4a85-43a6-ab85-dda75ee68d97" /># Employee Management Microservices

A production-ready **Employee Management System** built with **.NET 8 Clean Architecture** and **React**. Designed for high scalability, multi-database flexibility, and seamless Azure AKS deployment.

## 🚀 Features

- **Clean Architecture:** Separated into Core, Infrastructure, and API layers for maintainability and testing.
- **Multi-Database Support:** Choose between **SQL Server**, **PostgreSQL**, or **MySQL** by changing a single config setting.
- **Modern React UI:** A premium dashboard built with **Vite** featuring **Glassmorphism**, real-time search, and smooth animations.
- **Automated DB Migration:** Database schema and seed data are created automatically on startup.
- **Enterprise Ready:** Full **Docker** support, **Kubernetes (AKS)** manifests, and **GitHub Actions** CI/CD pipeline.

---

*The modern dashboard with glassmorphism design and real-time workforce data.*
*End-to-end verification of CRUD operations, search, and filtering.*

---

## 🛠️ Technology Stack

| Layer | Technology |
|-------|------------|
| **Backend** | .NET 8 (ASP.NET Core Web API) |
| **Frontend** | React 18, Vite, Vanilla CSS |
| **ORM** | Entity Framework Core 8 |
| **Validation** | FluentValidation |
| **Mapping** | AutoMapper |
| **Database** | SQL Server / PostgreSQL / MySQL |
| **DevOps** | Docker, K8s (AKS), GitHub Actions |

---

## 🏁 Getting Started

### 1. Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (v18+)
- A database server (SQL Server LocalDB is used by default)

### 2. Backend Setup
1. Open `EmployeeManagement.sln` in Visual Studio or VS Code.
2. Update the connection string in `appsettings.json` if necessary.
3. Run the project:
   ```powershell
   cd EmployeeManagement.API/EmployeeManagement.API
   dotnet run
   ```
   *The database will be created and seeded automatically.*
   *Swagger UI will be available at: http://localhost:5274*

### 3. Frontend Setup
1. Navigate to the UI folder:
   ```powershell
   cd employee-management-ui
   npm install
   npm start
   ```
2. The dashboard will open at **`http://localhost:3000`**.

---

## 🐳 Docker Support

To run the entire stack (App + DB) in one go:

```powershell
docker-compose up -d
```

---

## ☁️ Azure Deployment

The project includes ready-to-use manifests for **Azure Kubernetes Service (AKS)**:
- **Dockerfile:** Multi-stage build for production optimization.
- **K8s Manifests:** Located in the `/k8s` folder.
- **CI/CD:** Automated GitHub Actions workflow in `.github/workflows/azure-deploy.yml`.

---

## 📂 Project Structure

- `EmployeeManagement.Core`: Domain entities, DTOs, interfaces, and business logic.
- `EmployeeManagementInfrastructure`: Data access, EF Context, and repository implementations.
- `EmployeeManagement.API`: presentation layer, controllers, and configuration.
- `employee-management-ui`: Modern React frontend.
- `docs/images`: Project screenshots and demo recordings.
