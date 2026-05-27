# DRDO Apprenticeship Management System

ASP.NET Core MVC web application with HTML frontend, C# backend, and MySQL database.

**Phase 1 (current):** Separate Admin and Apprentice registration and login pages.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/) (8.x recommended)
- Optional: MySQL Workbench

## Setup

### 1. Create the database

Open MySQL and run:

```sql
source Database/init.sql
```

Or copy the contents of `Database/init.sql` into MySQL Workbench and execute.

### 2. Configure connection string

Edit `appsettings.json` and set your MySQL password:

```json
"DefaultConnection": "Server=localhost;Port=3306;Database=drdo_apprenticeship;User=root;Password=YOUR_MYSQL_PASSWORD;"
```

### 3. Run the application

```powershell
cd ApprenticeshipManagement
dotnet run
```

Open the URL shown in the terminal (usually `http://localhost:5000` or `http://localhost:5xxx`).

## Usage

1. From the home page, choose **Admin** or **Apprentice** portal.
2. Use the separate **Register** page for that role, then the matching **Login** page.
3. After login:
   - **Admin Login** → Admin Panel only (apprentice accounts are rejected)
   - **Apprentice Login** → Apprentice Portal only (admin accounts are rejected)

| Page | URL |
|------|-----|
| Admin Register | `/Account/AdminRegister` |
| Admin Login | `/Account/AdminLogin` |
| Apprentice Register | `/Account/ApprenticeRegister` |
| Apprentice Login | `/Account/ApprenticeLogin` |

## Project structure

| Folder / File | Purpose |
|---------------|---------|
| `Controllers/AccountController.cs` | Register, Login, Logout |
| `Controllers/AdminController.cs` | Admin dashboard (protected) |
| `Controllers/ApprenticeController.cs` | Apprentice dashboard (protected) |
| `Models/Admin.cs` | Admin table entity |
| `Models/Apprentice.cs` | Apprentice table entity |
| `Data/AppDbContext.cs` | Entity Framework + MySQL |
| `Views/Account/` | Login & Register HTML pages |
| `Database/init.sql` | Creates `admins` + `apprentices` tables |

## Database tables

| Table | Used by |
|-------|---------|
| `admins` | Admin Register / Admin Login |
| `apprentices` | Apprentice Register / Apprentice Login |

## Next steps (planned)

- Admin: manage apprentices, departments, training programs
- Apprentice: view training, attendance, assignments
- Reports and notifications
