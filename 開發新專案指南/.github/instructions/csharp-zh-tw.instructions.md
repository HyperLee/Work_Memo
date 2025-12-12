---
description: 'C# Application Development Guidelines'
applyTo: '**/*.cs'
---

# C# Application Development Guidelines

> This guide applies to .NET 10.0 and C# 14, integrating best practices and development standards.

## Table of Contents

1. [C# Development Principles](#c-development-principles)
2. [General Guidelines](#general-guidelines)
3. [Naming Conventions](#naming-conventions)
4. [Code Formatting](#code-formatting)
5. [Project Setup and Structure](#project-setup-and-structure)
6. [Nullable Reference Types](#nullable-reference-types)
7. [Data Access Patterns](#data-access-patterns)
8. [Authentication and Authorization](#authentication-and-authorization)
9. [Data Validation and Error Handling](#data-validation-and-error-handling)
10. [API Versioning and Documentation](#api-versioning-and-documentation)
11. [Logging and Monitoring](#logging-and-monitoring)
12. [Testing](#testing)
13. [Performance Optimization](#performance-optimization)
14. [Deployment and DevOps](#deployment-and-devops)

---

## C# Development Principles

- Always use the latest version of C#, currently C# 14 features.
- Write clear and concise comments for each function.
- Leverage C# 14 new features:
  - Primary Constructors
  - Collection Expressions
  - Inline Arrays
  - Optional Parameters for Lambda Expressions
  - Enhanced Extension Methods

## General Guidelines

- Make only high-confidence suggestions when reviewing code changes.
- Write code with good maintainability practices, including comments on why design decisions were made.
- Handle edge cases and write clear exception handling logic.
- For libraries or external dependencies, mention their usage and purpose in comments.
- Follow SOLID principles when designing classes and interfaces.
- Prefer composition over inheritance.

## Naming Conventions

### Casing Rules

| Item | Rule | Example |
|------|------|---------|
| Classes, Methods, Public Members | PascalCase | `UserService`, `GetUserById` |
| Private Fields | camelCase or _camelCase | `_userRepository`, `userName` |
| Local Variables | camelCase | `userCount`, `isValid` |
| Interfaces | I Prefix + PascalCase | `IUserService`, `IRepository<T>` |
| Constants | PascalCase | `MaxRetryCount`, `DefaultTimeout` |
| Generic Type Parameters | T Prefix | `TEntity`, `TKey` |

### Naming Recommendations

- Use meaningful and descriptive names.
- Avoid abbreviations unless they are widely recognized (e.g., `Id`, `Url`, `Http`).
- Use prefixes like `is`, `has`, `can` for boolean variables.
- Use `Async` suffix for asynchronous methods.

## Code Formatting

### Basic Formatting Rules

- Apply code-formatting style defined in `.editorconfig`.
- Prefer file-scoped namespace declarations and single-line using directives.
- Insert a newline before the opening curly brace of any code block (`if`, `for`, `while`, `foreach`, `using`, `try`, etc.).
- Ensure that the final return statement of a method is on its own line.
- Use pattern matching and switch expressions whenever possible.
- Use `nameof` instead of string literals when referring to member names.

### XML Documentation Comments

- Create XML documentation comments for all public APIs.
- When applicable, include `<example>` and `<code>` tags.

```csharp
/// <summary>
/// Retrieves user information by user ID.
/// </summary>
/// <param name="userId">The unique identifier of the user.</param>
/// <returns>The user entity, or null if not found.</returns>
/// <exception cref="ArgumentException">Thrown when userId is empty or invalid.</exception>
/// <example>
/// <code>
/// var user = await userService.GetUserByIdAsync(123);
/// </code>
/// </example>
public async Task<User?> GetUserByIdAsync(int userId)
```

### Modern C# Syntax Preferences

```csharp
// ✓ Recommended: File-scoped namespace
namespace MyApplication.Services;

// ✓ Recommended: Primary Constructor (C# 12+)
public class UserService(IUserRepository repository, ILogger<UserService> logger)
{
    public async Task<User?> GetUserAsync(int id) => await repository.FindByIdAsync(id);
}

// ✓ Recommended: Collection Expressions (C# 12+)
List<int> numbers = [1, 2, 3, 4, 5];

// ✓ Recommended: Pattern Matching
if (user is { IsActive: true, Role: "Admin" })
{
    // Handle admin logic
}

// ✓ Recommended: Switch Expression
string GetStatusMessage(OrderStatus status) => status switch
{
    OrderStatus.Pending => "Order is pending",
    OrderStatus.Processing => "Order is being processed",
    OrderStatus.Completed => "Order is completed",
    OrderStatus.Cancelled => "Order has been cancelled",
    _ => "Unknown status"
};
```

## Project Setup and Structure

### Creating a New Project

- Guide users through creating a new .NET project with appropriate templates.
- Explain the purpose of each generated file and folder to help understand the project structure.

### Recommended Project Structure

```
src/
├── MyApp.Api/                    # Web API Project
│   ├── Controllers/              # API Controllers
│   ├── Endpoints/                # Minimal API Endpoints (choose one)
│   ├── Filters/                  # Action Filters
│   ├── Middleware/               # Custom Middleware
│   └── Program.cs
├── MyApp.Application/            # Application Layer
│   ├── Commands/                 # CQRS Commands
│   ├── Queries/                  # CQRS Queries
│   ├── Services/                 # Application Services
│   └── Validators/               # FluentValidation Validators
├── MyApp.Domain/                 # Domain Layer
│   ├── Entities/                 # Domain Entities
│   ├── ValueObjects/             # Value Objects
│   ├── Interfaces/               # Domain Interfaces
│   └── Events/                   # Domain Events
└── MyApp.Infrastructure/         # Infrastructure Layer
    ├── Data/                     # EF Core DbContext
    ├── Repositories/             # Repository Implementations
    └── Services/                 # External Service Integrations

tests/
├── MyApp.UnitTests/              # Unit Tests
├── MyApp.IntegrationTests/       # Integration Tests
└── MyApp.FunctionalTests/        # Functional Tests
```

### Feature Folder Organization

- Demonstrate how to organize code using Feature Folders or Domain-Driven Design (DDD) principles.
- Show proper separation of concerns with models, services, and data access layers.

### ASP.NET Core 10 Configuration

- Explain the `Program.cs` and configuration system in ASP.NET Core 10.
- Include explanation of environment-specific settings.

```csharp
var builder = WebApplication.CreateBuilder(args);

// Configure Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure HTTP Request Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

## Nullable Reference Types

- Declare variables as non-nullable and check for `null` at entry points.
- Always use `is null` or `is not null` instead of `== null` or `!= null`.
- Trust C# null annotations and don't add unnecessary null checks when the type system guarantees a value cannot be null.

```csharp
// ✓ Recommended
public void ProcessUser(User? user)
{
    if (user is null)
    {
        throw new ArgumentNullException(nameof(user));
    }
    
    // user is now confirmed to be non-null
    Console.WriteLine(user.Name);
}

// ✓ Recommended: Using ArgumentNullException.ThrowIfNull (C# 10+)
public void ProcessUser(User? user)
{
    ArgumentNullException.ThrowIfNull(user);
    Console.WriteLine(user.Name);
}

// ✗ Avoid
if (user == null) { }  // Use is null instead
if (user != null) { }  // Use is not null instead
```

## Data Access Patterns

### Entity Framework Core

- Guide the implementation of data access layer using Entity Framework Core.
- Explain different options for development and production environments (SQL Server, SQLite, In-Memory).

```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Order> Orders => Set<Order>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
```

### Repository Pattern

- Demonstrate repository pattern implementation and its beneficial scenarios.
- Explain how to implement database migrations and data seeding.

```csharp
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}
```

### Efficient Query Patterns

- Explain efficient query patterns to avoid common performance issues.
- Use `AsNoTracking()` for read-only queries.
- Avoid N+1 query problems by leveraging `Include()` for eager loading.

## Authentication and Authorization

### JWT Bearer Token Authentication

- Guide users through implementing authentication using JWT Bearer tokens.
- Explain OAuth 2.0 and OpenID Connect concepts as they relate to ASP.NET Core.

```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
```

### Authorization Policies

- Demonstrate how to implement role-based and policy-based authorization.
- Explain how to consistently secure both controller-based and Minimal APIs.

```csharp
// Role-based Authorization
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase { }

// Policy-based Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("Admin"));
    
    options.AddPolicy("MinimumAge", policy =>
        policy.Requirements.Add(new MinimumAgeRequirement(18)));
});
```

### Microsoft Entra ID Integration

- Demonstrate integration with Microsoft Entra ID (formerly Azure AD).

## Data Validation and Error Handling

### Model Validation

- Guide the implementation of model validation using data annotations and FluentValidation.

```csharp
// Using FluentValidation
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email format is invalid");
            
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters");
    }
}
```

### Global Exception Handling

- Demonstrate a global exception handling strategy using middleware.
- Explain Problem Details (RFC 7807) implementation for standardized error responses.

```csharp
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled exception occurred");

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server Error",
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
```

## API Versioning and Documentation

### API Versioning Strategy

- Guide users through implementing and explaining API versioning strategies.

```csharp
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
```

### Swagger/OpenAPI Documentation

- Demonstrate Swagger/OpenAPI implementation with proper documentation.
- Explain how to document endpoints, parameters, responses, and authentication.

```csharp
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "API Documentation",
        Contact = new OpenApiContact
        {
            Name = "Development Team",
            Email = "dev@example.com"
        }
    });
    
    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
```

## Logging and Monitoring

### Structured Logging

- Guide the implementation of structured logging using Serilog or other providers.
- Explain logging levels and when to use each.

```csharp
// Serilog Configuration
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .WriteTo.Console()
        .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day);
});
```

### Logging Levels Guide

| Level | Purpose |
|-------|---------|
| Trace | Most detailed information, typically used only during development |
| Debug | Debug information |
| Information | General informational messages |
| Warning | Warning messages that may indicate issues but don't affect operation |
| Error | Error messages indicating functionality cannot operate normally |
| Critical | Critical errors where the application may be unable to continue |

### Application Insights Integration

- Demonstrate integration with Application Insights for telemetry collection.
- Explain how to implement custom telemetry and correlation IDs for request tracking.

## Testing

### Testing Principles

- Always include test cases for critical paths of the application.
- Do not write "Arrange", "Act", "Assert" comments.
- Copy existing style in nearby files for test method names and capitalization.

### Unit Tests

```csharp
public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly UserService _sut;

    public UserServiceTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _sut = new UserService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetUserByIdAsync_WhenUserExists_ReturnsUser()
    {
        var expectedUser = new User { Id = 1, Name = "Test User" };
        _mockRepository
            .Setup(r => r.GetByIdAsync(1, default))
            .ReturnsAsync(expectedUser);

        var result = await _sut.GetUserByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(expectedUser.Id, result.Id);
        Assert.Equal(expectedUser.Name, result.Name);
    }

    [Fact]
    public async Task GetUserByIdAsync_WhenUserNotExists_ReturnsNull()
    {
        _mockRepository
            .Setup(r => r.GetByIdAsync(999, default))
            .ReturnsAsync((User?)null);

        var result = await _sut.GetUserByIdAsync(999);

        Assert.Null(result);
    }
}
```

### Integration Tests

- Explain integration testing approaches for API endpoints.
- Demonstrate how to mock dependencies effectively for testing.

```csharp
public class UsersControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UsersControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetUsers_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/users");

        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }
}
```

### Test-Driven Development (TDD)

- Explain test-driven development principles as applied to API development.
- Show how to test authentication and authorization logic.

## Performance Optimization

### Caching Strategies

- Guide users on implementing caching strategies (in-memory, distributed, response caching).

```csharp
// In-Memory Cache
builder.Services.AddMemoryCache();

// Distributed Cache (Redis)
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "MyApp_";
});

// Response Caching
builder.Services.AddResponseCaching();
app.UseResponseCaching();
```

### Asynchronous Programming

- Explain asynchronous programming patterns and why they matter for API performance.
- Use `async`/`await` correctly and avoid blocking calls.

```csharp
// ✓ Recommended: Fully Asynchronous
public async Task<IActionResult> GetUsersAsync()
{
    var users = await _userService.GetAllUsersAsync();
    return Ok(users);
}

// ✗ Avoid: Blocking Call
public IActionResult GetUsers()
{
    var users = _userService.GetAllUsersAsync().Result; // Blocks!
    return Ok(users);
}
```

### Pagination, Filtering, and Sorting

- Demonstrate pagination, filtering, and sorting for large datasets.

```csharp
public class PaginatedList<T>
{
    public List<T> Items { get; }
    public int PageIndex { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(
        IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}
```

## Deployment and DevOps

### Containerization

- Guide users through containerizing their API using .NET's built-in container support.

```bash
# Using .NET Built-in Container Publishing
dotnet publish --os linux --arch x64 -p:PublishProfile=DefaultContainer

# Or using .NET 8+ Simplified Syntax
dotnet publish -c Release --os linux --arch x64 /t:PublishContainer
```

- Explain the differences between manual Dockerfile creation and .NET container publishing features.

### CI/CD Pipeline

- Explain CI/CD pipelines for .NET applications.
- Example GitHub Actions Workflow:

```yaml
name: .NET CI/CD

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v4
    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: '10.0.x'
    - name: Restore dependencies
      run: dotnet restore
    - name: Build
      run: dotnet build --no-restore
    - name: Test
      run: dotnet test --no-build --verbosity normal
```

### Health Checks

- Demonstrate how to implement health checks and readiness probes.

```csharp
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>()
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!)
    .AddCheck("self", () => HealthCheckResult.Healthy());

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecks("/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});
```

### Deployment Targets

- Demonstrate deployment to Azure App Service, Azure Container Apps, or other hosting options.
- Explain environment-specific configurations for different deployment stages.

---

## Appendix: Common .NET CLI Commands

```bash
# Create New Projects
dotnet new webapi -n MyApi -o src/MyApi
dotnet new classlib -n MyApp.Domain -o src/MyApp.Domain
dotnet new xunit -n MyApp.Tests -o tests/MyApp.Tests

# Add Packages
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Serilog.AspNetCore
dotnet add package FluentValidation.AspNetCore

# EF Core Migrations
dotnet ef migrations add InitialCreate
dotnet ef database update

# Run Tests
dotnet test --collect:"XPlat Code Coverage"

# Publish
dotnet publish -c Release -o ./publish
```

---

*Last Updated: December 2025*
*Applicable Versions: .NET 10.0 / C# 14*
