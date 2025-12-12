---
description: 'C# 應用程式開發指南 (繁體中文版)'
applyTo: '**/*.cs'
---

# C# 應用程式開發指南

> 本指南適用於 .NET 10.0 與 C# 14，整合最佳實務與開發規範。

## 目錄

1. [C# 開發原則](#c-開發原則)
2. [一般指導方針](#一般指導方針)
3. [命名規則](#命名規則)
4. [程式碼格式](#程式碼格式)
5. [專案設定與結構](#專案設定與結構)
6. [Nullable 參考型別](#nullable-參考型別)
7. [資料存取模式](#資料存取模式)
8. [驗證與授權](#驗證與授權)
9. [資料驗證與錯誤處理](#資料驗證與錯誤處理)
10. [API 版本控制與文件](#api-版本控制與文件)
11. [日誌記錄與監控](#日誌記錄與監控)
12. [測試](#測試)
13. [效能最佳化](#效能最佳化)
14. [部署與 DevOps](#部署與-devops)

---

## C# 開發原則

- 始終使用最新版本的 C#，目前為 C# 14 的功能特性。
- 為每個函式撰寫清晰且簡潔的註解。
- 善用 C# 14 新特性：
  - 主要建構函式 (Primary Constructors)
  - 集合表達式 (Collection Expressions)
  - 內嵌陣列 (Inline Arrays)
  - 選擇性參數的 Lambda 表達式
  - 擴展方法 (Extension Methods) 增強功能

## 一般指導方針

- 程式碼審查時，僅提出高度確信的建議。
- 撰寫具有良好可維護性的程式碼，包含設計決策原因的註解。
- 妥善處理邊界情況，撰寫清晰的例外處理邏輯。
- 對於函式庫或外部相依套件，在註解中說明其用途與目的。
- 遵循 SOLID 原則設計類別與介面。
- 優先使用組合 (Composition) 而非繼承 (Inheritance)。

## 命名規則

### 大小寫規則

| 項目 | 規則 | 範例 |
|------|------|------|
| 類別、方法、公開成員 | PascalCase | `UserService`, `GetUserById` |
| 私有欄位 | camelCase 或 _camelCase | `_userRepository`, `userName` |
| 區域變數 | camelCase | `userCount`, `isValid` |
| 介面 | I 前綴 + PascalCase | `IUserService`, `IRepository<T>` |
| 常數 | PascalCase | `MaxRetryCount`, `DefaultTimeout` |
| 泛型型別參數 | T 前綴 | `TEntity`, `TKey` |

### 命名建議

- 使用有意義且具描述性的名稱。
- 避免使用縮寫，除非是廣為人知的縮寫 (如 `Id`, `Url`, `Http`)。
- 布林值變數使用 `is`, `has`, `can` 等前綴。
- 非同步方法使用 `Async` 後綴。

## 程式碼格式

### 基本格式規則

- 套用 `.editorconfig` 中定義的程式碼格式樣式。
- 偏好使用檔案範圍的 namespace 宣告與單行 using 指示詞。
- 在任何程式碼區塊 (`if`, `for`, `while`, `foreach`, `using`, `try` 等) 的左大括號前插入換行。
- 確保方法的最終 return 陳述式位於獨立的一行。
- 盡可能使用模式比對 (Pattern Matching) 和 switch 表達式。
- 使用 `nameof` 取代字串常值來參考成員名稱。

### XML 文件註解

- 為所有公開 API 建立 XML 文件註解。
- 適用時，包含 `<example>` 和 `<code>` 標籤。

```csharp
/// <summary>
/// 根據使用者 ID 取得使用者資訊。
/// </summary>
/// <param name="userId">使用者的唯一識別碼。</param>
/// <returns>使用者實體，若找不到則回傳 null。</returns>
/// <exception cref="ArgumentException">當 userId 為空或無效時擲出。</exception>
/// <example>
/// <code>
/// var user = await userService.GetUserByIdAsync(123);
/// </code>
/// </example>
public async Task<User?> GetUserByIdAsync(int userId)
```

### 現代 C# 語法偏好

```csharp
// ✓ 推薦：檔案範圍的 namespace
namespace MyApplication.Services;

// ✓ 推薦：主要建構函式 (C# 12+)
public class UserService(IUserRepository repository, ILogger<UserService> logger)
{
    public async Task<User?> GetUserAsync(int id) => await repository.FindByIdAsync(id);
}

// ✓ 推薦：集合表達式 (C# 12+)
List<int> numbers = [1, 2, 3, 4, 5];

// ✓ 推薦：模式比對
if (user is { IsActive: true, Role: "Admin" })
{
    // 處理管理員邏輯
}

// ✓ 推薦：switch 表達式
string GetStatusMessage(OrderStatus status) => status switch
{
    OrderStatus.Pending => "訂單待處理",
    OrderStatus.Processing => "訂單處理中",
    OrderStatus.Completed => "訂單已完成",
    OrderStatus.Cancelled => "訂單已取消",
    _ => "未知狀態"
};
```

## 專案設定與結構

### 建立新專案

- 引導使用者使用適當的範本建立新 .NET 專案。
- 說明每個產生檔案和資料夾的用途，幫助理解專案結構。

### 專案結構建議

```
src/
├── MyApp.Api/                    # Web API 專案
│   ├── Controllers/              # API 控制器
│   ├── Endpoints/                # Minimal API 端點 (擇一使用)
│   ├── Filters/                  # Action 過濾器
│   ├── Middleware/               # 自訂中介軟體
│   └── Program.cs
├── MyApp.Application/            # 應用程式層
│   ├── Commands/                 # CQRS 命令
│   ├── Queries/                  # CQRS 查詢
│   ├── Services/                 # 應用程式服務
│   └── Validators/               # FluentValidation 驗證器
├── MyApp.Domain/                 # 領域層
│   ├── Entities/                 # 領域實體
│   ├── ValueObjects/             # 值物件
│   ├── Interfaces/               # 領域介面
│   └── Events/                   # 領域事件
└── MyApp.Infrastructure/         # 基礎建設層
    ├── Data/                     # EF Core DbContext
    ├── Repositories/             # 儲存庫實作
    └── Services/                 # 外部服務整合

tests/
├── MyApp.UnitTests/              # 單元測試
├── MyApp.IntegrationTests/       # 整合測試
└── MyApp.FunctionalTests/        # 功能測試
```

### 功能資料夾組織

- 展示如何使用功能資料夾 (Feature Folders) 或領域驅動設計 (DDD) 原則組織程式碼。
- 呈現模型、服務和資料存取層的適當關注點分離。

### ASP.NET Core 10 設定

- 說明 ASP.NET Core 10 中的 `Program.cs` 和設定系統。
- 包含環境特定設定的說明。

```csharp
var builder = WebApplication.CreateBuilder(args);

// 設定服務
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 相依性注入
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 設定 HTTP 請求管線
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

## Nullable 參考型別

- 將變數宣告為非 null，並在進入點檢查 `null`。
- 始終使用 `is null` 或 `is not null` 而非 `== null` 或 `!= null`。
- 信任 C# 的 null 註解，當型別系統表明值不能為 null 時，不要新增不必要的 null 檢查。

```csharp
// ✓ 推薦
public void ProcessUser(User? user)
{
    if (user is null)
    {
        throw new ArgumentNullException(nameof(user));
    }
    
    // 此時 user 已確認非 null
    Console.WriteLine(user.Name);
}

// ✓ 推薦：使用 ArgumentNullException.ThrowIfNull (C# 10+)
public void ProcessUser(User? user)
{
    ArgumentNullException.ThrowIfNull(user);
    Console.WriteLine(user.Name);
}

// ✗ 避免
if (user == null) { }  // 使用 is null 代替
if (user != null) { }  // 使用 is not null 代替
```

## 資料存取模式

### Entity Framework Core

- 引導使用 Entity Framework Core 實作資料存取層。
- 說明開發和生產環境的不同選項 (SQL Server, SQLite, In-Memory)。

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

### 儲存庫模式

- 展示儲存庫模式的實作及其適用場景。
- 說明資料庫遷移和資料種子的實作方式。

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

### 高效查詢模式

- 說明高效的查詢模式以避免常見的效能問題。
- 使用 `AsNoTracking()` 進行唯讀查詢。
- 避免 N+1 查詢問題，善用 `Include()` 預先載入。

## 驗證與授權

### JWT Bearer Token 驗證

- 引導使用者使用 JWT Bearer Token 實作驗證。
- 說明與 ASP.NET Core 相關的 OAuth 2.0 和 OpenID Connect 概念。

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

### 授權策略

- 展示如何實作角色型和策略型授權。
- 說明如何一致地保護控制器型和 Minimal API。

```csharp
// 角色型授權
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase { }

// 策略型授權
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("Admin"));
    
    options.AddPolicy("MinimumAge", policy =>
        policy.Requirements.Add(new MinimumAgeRequirement(18)));
});
```

### Microsoft Entra ID 整合

- 展示與 Microsoft Entra ID (前身為 Azure AD) 的整合。

## 資料驗證與錯誤處理

### 模型驗證

- 引導使用資料註解和 FluentValidation 實作模型驗證。

```csharp
// 使用 FluentValidation
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("電子郵件為必填")
            .EmailAddress().WithMessage("電子郵件格式無效");
            
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("密碼為必填")
            .MinimumLength(8).WithMessage("密碼長度至少 8 個字元");
    }
}
```

### 全域例外處理

- 展示使用中介軟體的全域例外處理策略。
- 說明 Problem Details (RFC 7807) 的實作以提供標準化錯誤回應。

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
        _logger.LogError(exception, "發生未處理的例外");

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "伺服器錯誤",
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
```

## API 版本控制與文件

### API 版本控制策略

- 引導使用者實作並說明 API 版本控制策略。

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

### Swagger/OpenAPI 文件

- 展示具有適當文件的 Swagger/OpenAPI 實作。
- 說明如何記錄端點、參數、回應和驗證。

```csharp
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "API 說明文件",
        Contact = new OpenApiContact
        {
            Name = "開發團隊",
            Email = "dev@example.com"
        }
    });
    
    // 加入 XML 註解
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
```

## 日誌記錄與監控

### 結構化日誌

- 引導使用 Serilog 或其他提供者實作結構化日誌記錄。
- 說明日誌層級及其使用時機。

```csharp
// Serilog 設定
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

### 日誌層級指南

| 層級 | 用途 |
|------|------|
| Trace | 最詳細的資訊，通常僅在開發時使用 |
| Debug | 除錯用資訊 |
| Information | 一般資訊性訊息 |
| Warning | 警告訊息，可能有問題但不影響運作 |
| Error | 錯誤訊息，功能無法正常運作 |
| Critical | 嚴重錯誤，應用程式可能無法繼續執行 |

### Application Insights 整合

- 展示與 Application Insights 的整合以收集遙測資料。
- 說明如何實作自訂遙測和相關聯識別碼進行請求追蹤。

## 測試

### 測試原則

- 始終為應用程式的關鍵路徑包含測試案例。
- 不要撰寫 "Arrange"、"Act"、"Assert" 註解。
- 複製鄰近檔案的現有風格，包括測試方法名稱和大小寫。

### 單元測試

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
        var expectedUser = new User { Id = 1, Name = "測試使用者" };
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

### 整合測試

- 說明 API 端點的整合測試方法。
- 展示如何有效地模擬相依性進行測試。

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

### 測試驅動開發 (TDD)

- 說明應用於 API 開發的測試驅動開發原則。
- 展示如何測試驗證與授權邏輯。

## 效能最佳化

### 快取策略

- 引導使用者實作快取策略 (記憶體內、分散式、回應快取)。

```csharp
// 記憶體內快取
builder.Services.AddMemoryCache();

// 分散式快取 (Redis)
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "MyApp_";
});

// 回應快取
builder.Services.AddResponseCaching();
app.UseResponseCaching();
```

### 非同步程式設計

- 說明非同步程式設計模式及其對 API 效能的重要性。
- 正確使用 `async`/`await`，避免阻塞呼叫。

```csharp
// ✓ 推薦：完全非同步
public async Task<IActionResult> GetUsersAsync()
{
    var users = await _userService.GetAllUsersAsync();
    return Ok(users);
}

// ✗ 避免：阻塞呼叫
public IActionResult GetUsers()
{
    var users = _userService.GetAllUsersAsync().Result; // 阻塞！
    return Ok(users);
}
```

### 分頁、篩選與排序

- 展示大型資料集的分頁、篩選和排序功能。

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

## 部署與 DevOps

### 容器化

- 引導使用者使用 .NET 的內建容器支援將 API 容器化。

```bash
# 使用 .NET 內建容器發佈功能
dotnet publish --os linux --arch x64 -p:PublishProfile=DefaultContainer

# 或使用 .NET 8+ 的簡化語法
dotnet publish -c Release --os linux --arch x64 /t:PublishContainer
```

- 說明手動建立 Dockerfile 與 .NET 容器發佈功能的差異。

### CI/CD 管線

- 說明 .NET 應用程式的 CI/CD 管線。
- 範例 GitHub Actions 工作流程：

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

### 健康檢查

- 展示如何實作健康檢查和就緒探測。

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

### 部署目標

- 展示部署到 Azure App Service、Azure Container Apps 或其他主機選項。
- 說明不同部署階段的環境特定設定。

---

## 附錄：常用 .NET CLI 命令

```bash
# 建立新專案
dotnet new webapi -n MyApi -o src/MyApi
dotnet new classlib -n MyApp.Domain -o src/MyApp.Domain
dotnet new xunit -n MyApp.Tests -o tests/MyApp.Tests

# 新增套件
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Serilog.AspNetCore
dotnet add package FluentValidation.AspNetCore

# EF Core 遷移
dotnet ef migrations add InitialCreate
dotnet ef database update

# 執行測試
dotnet test --collect:"XPlat Code Coverage"

# 發佈
dotnet publish -c Release -o ./publish
```

---

*最後更新：2025 年 12 月*
*適用版本：.NET 10.0 / C# 14*
