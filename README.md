Project Documentation: Retail API Using .NET Core 7

Table of Contents

•	Project Overview

•	Architecture and Design

•	SOLID Principles Applied

•	Database Optimization Techniques

•	Best Practices and Clean Code Techniques

•	How to Run the Project

•	API Endpoints

Project Overview

This project is a Retail Management API built using .NET Core 7, implementing the clean architecture pattern. The application includes functionalities for managing products, categories, and related data while ensuring scalability, maintainability, and performance optimization.

Architecture and Design

The project follows Clean Architecture principles, dividing the application into distinct layers:

Core:

Contains the business logic and domain entities.


Infrastructure:
Interfaces for services and repositories.


Implements the interfaces.

Manages database interactions using Entity Framework Core.

Includes SQL execution and repository patterns.

API:

Exposes RESTful endpoints to interact with the system.

Handles HTTP requests and responses.

Key Design Patterns Used

Repository Pattern: Abstracts database operations for cleaner separation of concerns.

Unit of Work: Manages transaction consistency.

Dependency Injection: Ensures loosely coupled components and testability.

SOLID Principles Applied

1. Single Responsibility Principle (SRP):

Each class is responsible for a single piece of functionality. For example:

ProductService handles product-specific business logic.

CategoryService handles category-related operations.

2. Open/Closed Principle (OCP):

The IRepository and IUnitOfWork interfaces allow adding new repositories or database operations without modifying existing code.

3. Liskov Substitution Principle (LSP):

All service and repository implementations can replace their abstractions without altering functionality.

4. Interface Segregation Principle (ISP):

Interfaces like IProductService and ICategoryService are specific and focused, ensuring clients only depend on what they use.

5. Dependency Inversion Principle (DIP):

High-level modules (controllers) depend on abstractions (IProductService, ICategoryService), not concrete implementations.

Database Optimization Techniques

1. Query Optimization

AsNoTracking():

Used for read-only queries to avoid the overhead of tracking changes in Entity Framework.

Example:

public async Task<IEnumerable<Product>> GetAllProductsNoTracking()
{
    return await _dbSet.AsNoTracking().ToListAsync();
}

Stored Procedures:

Used for complex queries to improve performance and centralize logic in the database.

Example:

CREATE PROCEDURE GetProductsByCategory
    @CategoryId INT
AS
BEGIN
    SELECT * FROM Products WHERE CategoryId = @CategoryId;
END;

Pagination:

Implemented using Skip() and Take() to limit the result set.

Example:

public async Task<IEnumerable<Product>> GetPagedProducts(int pageNumber, int pageSize)
{
    return await _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
}

2. Indexes

Added indexes to frequently searched columns (e.g., CategoryId) to speed up query execution.

Example Migration:

migrationBuilder.CreateIndex(
    name: "IX_Product_CategoryId",
    table: "Products",
    column: "CategoryId");

3. Eager Loading

Used Include() to load related data and avoid the N+1 query problem.

Example:

public async Task<Product> GetProductWithCategoryAsync(int productId)
{
    return await _dbSet.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == productId);
}

Best Practices and Clean Code Techniques

Layered Architecture:

Separation of concerns between Core, Infrastructure, and API layers.

Dependency Injection:

All dependencies are injected via constructors, promoting testability and loose coupling.

Validation:

Data annotations are used to validate entities.

Example:

public class Product
{
    [Required]
    public string Name { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
}

Error Handling:

Controllers return appropriate HTTP status codes based on operation success or failure.

Example:

if (product == null)
    return NotFound("Product not found.");

return Ok(product);

Data Seeding:

Sample data is seeded in the OnModelCreating method to set up initial database state.

Example:

modelBuilder.Entity<Category>().HasData(
    new Category { Id = 1, Name = "Electronics" },
    new Category { Id = 2, Name = "Books" }
);

Comments and Naming Conventions:

Clear and descriptive method names and comments improve code readability.

How to Run the Project

Clone the repository:

git clone <repository-url>

Navigate to the project directory:

cd RetailApi

Update the appsettings.json file with your database connection string:

"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=RetailDb;Trusted_Connection=True;"
}

Apply migrations to create the database:

dotnet ef database update

Run the application:

dotnet run

Access the API at:

http://localhost:5000/api

API Endpoints

Products

GET /api/product: Get all products.

GET /api/product/{id}: Get product by ID.

POST /api/product: Create a new product.

PUT /api/product/{id}: Update a product.

DELETE /api/product/{id}: Delete a product.

GET /api/product/category/{categoryId}: Get products by category.

Categories

GET /api/category: Get all categories.

GET /api/category/{id}: Get category by ID.

Conclusion

This project demonstrates a robust implementation of a retail API adhering to industry best practices, SOLID principles, and clean architecture. It ensures high performance, scalability, and maintainability, making it suitable for real-world applications.

