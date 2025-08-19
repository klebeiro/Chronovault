# ChronoVault 🕒  
Watch E-commerce (Angular + C#)

ChronoVault is a complete e-commerce application focused on selling watches. The system was built with **Angular** on the front-end and **ASP.NET Core** on the back-end, using **Entity Framework** for database persistence.

---

## Features

- Product listing (watches)
- Account register, management and login with JWT authentication
- User account management
- Shopping cart functionality
- Payments and order creation

---

## Technologies

### Backend
- C#
- ASP.NET Core
- Entity Framework Core
- Fluent Validation
- AutoMapper

### Frontend
- Angular
- TypeScript
- Material UI

---

## How to Run

### Backend (.NET API)

```bash
cd chronovault-api
dotnet restore
dotnet ef database update
dotnet run
```

### Frontend (Angular)

```bash
cd client
npm install
npm run start
```
