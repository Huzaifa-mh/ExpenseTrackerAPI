# 💰 Personal Expense Tracker API

A robust RESTful API built with **ASP.NET Core 8.0** for tracking personal expenses with category-based analytics. This backend service provides comprehensive expense management with date filtering, category summaries, and full CRUD operations.

## 🎯 Project Overview

### The Problem
People often lose track of their spending and struggle to understand where their money goes each month. Without proper categorization and analytics, it's difficult to make informed financial decisions.

### The Solution
This API provides a structured way to:
- ✅ Track every expense with detailed information
- ✅ Categorize spending for better insights
- ✅ Filter expenses by date ranges
- ✅ Generate spending summaries by category
- ✅ Analyze spending patterns over time

---

## 🛠️ Tech Stack

| Technology | Purpose |
|------------|---------|
| **ASP.NET Core 10.0** | Web API Framework |
| **C# 12** | Programming Language |
| **Entity Framework Core** | ORM for Database Access |
| **SQL Server** | Relational Database |
| **Swagger/OpenAPI** | API Documentation |
| **LINQ** | Data Querying & Aggregation |

---

## 🏗️ Architecture & Design Patterns

### Design Patterns Used:
- **Repository Pattern** (can be implemented)
- **DTO (Data Transfer Object) Pattern** - Clean separation between models and API responses
- **Dependency Injection** - For DbContext and services
- **Async/Await Pattern** - For non-blocking I/O operations

### Project Structure:
```
ExpenseTracker.API/
├── Controllers/
│   ├── CategoriesController.cs    # Category endpoints
│   └── ExpensesController.cs      # Expense CRUD & analytics
├── Models/
│   ├── Category.cs                # Category entity
│   ├── Expense.cs                 # Expense entity
│   └── DTOs/
│       ├── ExpenseDTO.cs          # Create expense input
│       ├── ExpenseResponseDTO.cs  # Expense output
│       └── ExpenseSummaryDTO.cs   # Category summary output
├── Data/
│   └── ExpenseDbContext.cs        # EF Core context
├── Migrations/                     # Database migrations
├── Program.cs                      # App configuration
└── appsettings.json               # Configuration settings
```

---

## 📊 Database Schema

### Tables

#### **Categories**
| Column | Type | Description |
|--------|------|-------------|
| Id | int (PK) | Primary key |
| Name | nvarchar(50) | Category name |
| ColorCode | nvarchar(7) | Hex color code for UI |

**Seeded Data:** 7 default categories (Food & Dining, Transportation, Entertainment, Shopping, Bills & Utilities, Healthcare, Other)

#### **Expenses**
| Column | Type | Description |
|--------|------|-------------|
| Id | int (PK) | Primary key |
| Amount | decimal(18,2) | Expense amount |
| CategoryId | int (FK) | Foreign key to Categories |
| Date | datetime2 | Expense date |
| Description | nvarchar(200) | Optional description |
| CreatedAt | datetime2 | Record creation timestamp |

**Relationships:**
- Expense → Category (Many-to-One)

---

## 🚀 API Endpoints

### Base URL
```
https://localhost:7026/api
```

### Categories

| Method | Endpoint | Description | Response |
|--------|----------|-------------|----------|
| GET | `/categories` | Get all categories | 200 OK |

**Example Response:**
```json
[
  {
    "id": 1,
    "name": "Food & Dining",
    "colorCode": "#FF6384"
  }
]
```

---

### Expenses

#### **Get All Expenses**
```http
GET /expenses
GET /expenses?startDate=2024-01-01&endDate=2024-01-31
```

**Query Parameters:**
- `startDate` (optional): Filter expenses from this date (YYYY-MM-DD)
- `endDate` (optional): Filter expenses until this date (YYYY-MM-DD)

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "amount": 50.99,
    "categoryId": 1,
    "categoryName": "Food & Dining",
    "categoryColor": "#FF6384",
    "date": "2024-01-26T00:00:00",
    "description": "Lunch at cafe",
    "createdAt": "2024-01-26T10:30:00Z"
  }
]
```

---

#### **Get Expense by ID**
```http
GET /expenses/{id}
```

**Response:** `200 OK` or `404 Not Found`

---

#### **Create Expense**
```http
POST /expenses
Content-Type: application/json
```

**Request Body:**
```json
{
  "amount": 50.99,
  "categoryId": 1,
  "date": "2024-01-26",
  "description": "Lunch at cafe"
}
```

**Validations:**
- `amount`: Required, must be > 0
- `categoryId`: Required, must exist
- `date`: Required
- `description`: Optional, max 200 characters

**Response:** `201 Created`
```json
{
  "id": 1,
  "amount": 50.99,
  "categoryId": 1,
  "categoryName": "Food & Dining",
  "categoryColor": "#FF6384",
  "date": "2024-01-26T00:00:00",
  "description": "Lunch at cafe",
  "createdAt": "2024-01-26T10:30:00Z"
}
```

---

#### **Delete Expense**
```http
DELETE /expenses/{id}
```

**Response:** `204 No Content` or `404 Not Found`

---

#### **Get Category Summary**
```http
GET /expenses/summary
```

**Description:** Aggregates total spending and expense count by category.

**Response:** `200 OK`
```json
[
  {
    "categoryId": 1,
    "categoryName": "Food & Dining",
    "categoryColor": "#FF6384",
    "totalAmount": 250.50,
    "expenseCount": 8
  },
  {
    "categoryId": 2,
    "categoryName": "Transportation",
    "categoryColor": "#36A2EB",
    "totalAmount": 120.00,
    "expenseCount": 3
  }
]
```

---

## 🔧 Setup & Installation

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation Steps

1. **Clone the repository**
```bash
git clone <your-repo-url>
cd ExpenseTracker.API
```

2. **Update Connection String**

Edit `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=ExpenseTracker;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER` with:
- `localhost` (SQL Server Developer)
- `localhost\\SQLEXPRESS` (SQL Server Express)

3. **Install Dependencies**
```bash
dotnet restore
```

4. **Apply Database Migrations**
```bash
dotnet ef database update
```

This creates:
- `ExpenseTracker` database
- `Categories` and `Expenses` tables
- Seeds 7 default categories

5. **Run the Application**
```bash
dotnet run
```

API runs at: `https://localhost:7026`

6. **Access Swagger Documentation**
```
https://localhost:7026/swagger
```

---

## 🧪 Testing the API

### Using Swagger UI
1. Navigate to `https://localhost:7026/swagger`
2. Expand any endpoint
3. Click "Try it out"
4. Fill in parameters
5. Click "Execute"

### Using Postman

**Import this collection or test manually:**

**1. Get Categories:**
```http
GET https://localhost:7026/api/categories
```

**2. Create Expense:**
```http
POST https://localhost:7026/api/expenses
Content-Type: application/json

{
  "amount": 50.00,
  "categoryId": 1,
  "date": "2024-01-26",
  "description": "Lunch"
}
```

**3. Get All Expenses:**
```http
GET https://localhost:7026/api/expenses
```

**4. Get Expenses by Date Range:**
```http
GET https://localhost:7026/api/expenses?startDate=2024-01-01&endDate=2024-01-31
```

**5. Get Summary:**
```http
GET https://localhost:7026/api/expenses/summary
```

**6. Delete Expense:**
```http
DELETE https://localhost:7026/api/expenses/1
```

---

## 🔑 Key Features Implemented

### 1. **Type-Safe Money Handling**
```csharp
[Column(TypeName = "decimal(18,2)")]
public decimal Amount { get; set; }
```
- Uses `decimal` instead of `double` to avoid floating-point errors
- Ensures accurate financial calculations

### 2. **Date Range Filtering**
```csharp
var query = _context.Expenses.AsQueryable();
if (startDate.HasValue)
    query = query.Where(e => e.Date >= startDate.Value);
if (endDate.HasValue)
    query = query.Where(e => e.Date <= endDate.Value);
```
- Flexible querying with optional parameters
- Efficient database queries

### 3. **Category Aggregation with LINQ**
```csharp
var summary = await _context.Expenses
    .Include(e => e.Category)
    .GroupBy(e => e.CategoryId)
    .Select(group => new ExpenseSummaryDTO
    {
        CategoryId = group.Key,
        TotalAmount = group.Sum(e => e.Amount),
        ExpenseCount = group.Count()
    })
    .ToListAsync();
```
- Efficient database-level aggregation
- Returns summarized data for analytics

### 4. **DTO Pattern for Security**
- Prevents over-posting attacks
- Controls exactly what data is exposed
- Separates internal models from API contracts

### 5. **Async/Await Throughout**
- Non-blocking I/O operations
- Better scalability and performance
- Proper use of `Task<T>` return types

### 6. **Proper HTTP Status Codes**
- `200 OK` - Successful GET
- `201 Created` - Successful POST with Location header
- `204 No Content` - Successful DELETE
- `400 Bad Request` - Validation errors
- `404 Not Found` - Resource not found

### 7. **CORS Configuration**
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```
- Enables frontend integration
- Configured for React development server

---

## 📚 What I Learned

### Technical Skills
- ✅ Entity Framework Core migrations and relationships
- ✅ LINQ queries with `Include()`, `GroupBy()`, `Sum()`, `Count()`
- ✅ DTO pattern implementation
- ✅ Handling circular references in JSON serialization
- ✅ Async/await pattern for database operations
- ✅ Query parameter filtering
- ✅ RESTful API design principles
- ✅ Proper HTTP status code usage
- ✅ Data validation with Data Annotations
- ✅ Dependency injection in ASP.NET Core

### Best Practices
- ✅ Using `decimal` for monetary values
- ✅ Nullable types for optional parameters
- ✅ Explicit type specifications with `Column` attribute
- ✅ Navigation properties for relationships
- ✅ Separating concerns (Models, DTOs, Controllers)
- ✅ Database seeding for initial data

---

## 🚀 Future Enhancements

### Planned Features
- [ ] **User Authentication** - JWT-based authentication
- [ ] **Multi-user Support** - User-specific expenses
- [ ] **Budget Limits** - Set and track category budgets
- [ ] **Recurring Expenses** - Auto-create monthly bills
- [ ] **Export Functionality** - CSV/PDF export
- [ ] **Advanced Analytics** - Monthly trends, year-over-year comparison
- [ ] **Currency Support** - Multi-currency handling
- [ ] **Expense Attachments** - Upload receipts
- [ ] **Search Functionality** - Full-text search on descriptions
- [ ] **Pagination** - For large datasets

### Technical Improvements
- [ ] **Repository Pattern** - Abstract data access layer
- [ ] **Unit Testing** - xUnit tests for controllers and services
- [ ] **Logging** - Serilog integration
- [ ] **API Versioning** - Support multiple API versions
- [ ] **Rate Limiting** - Prevent API abuse
- [ ] **Caching** - Redis for frequently accessed data
- [ ] **Validation Library** - FluentValidation
- [ ] **AutoMapper** - Automated DTO mapping

---

## 🤝 Contributing

This is a personal learning project, but feedback and suggestions are welcome!

---

## 📝 License

This project is for educational purposes.

---

## 👨‍💻 Developer

**[Your Name]**
- LinkedIn: [[Muhammad Huzaifa](https://www.linkedin.com/in/muhammadhuzaifamh/)]
- GitHub: [[Muhammad Huzaifa](https://github.com/Huzaifa-mh)]
- Email: [muhammadhuzaifa_mh@yahoo.com]

---

## 📸 Screenshots

### Swagger UI
![Swagger Documentation](link-to-screenshot)

### Database Schema
![Database Tables in SSMS](link-to-screenshot)

### API Response Example
![Postman Response](link-to-screenshot)

---

## 🙏 Acknowledgments

- ASP.NET Core Documentation
- Entity Framework Core Documentation
- Stack Overflow Community
- YouTube tutorials and courses

---

**Built with ❤️ while learning ASP.NET Core**

*Last Updated: January 2025*
