---
description: "Canonical C# application development guidelines (merged from en + ja sources) — by HyperLee"
applyTo: '**/*.cs'
---

# C# Application Development — Canonical Guidelines

This canonical guideline merges best practices from existing English and Japanese instructions into a single, practical, English-language reference for C# development teams. It targets .NET 7+ and C# 10+ (adapt to your project's target framework and language version).

Use this file as a living document: keep it in your repo's .github/instructions directory and sync updates across localized versions.

## Quick Principles
- Prefer clarity and readability over cleverness.
- Follow team conventions defined in .editorconfig and code review checklists.
- Document design decisions and non-obvious trade-offs.
- Prefer small, well-tested components with clear responsibilities.

---

## Frontmatter
- Keep description and applyTo fields for automation (linters, bots).

---

## Naming Conventions
Follow Microsoft's conventions with team-specific rules documented here.

- Types, public members, methods, properties: PascalCase (e.g., UserService, GetCustomer).
- Private fields: underscore + camelCase (e.g., _connectionString).
- Local variables and parameters: camelCase.
- Interfaces: prefix `I` (e.g., IUserRepository).
- Generic type parameters: `TResult`, `TKey`, `TValue`.
- Async methods: suffix `Async` (e.g., GetUsersAsync).
- Constants: PascalCase (e.g., DefaultTimeout).
- Use meaningful names; avoid abbreviations unless commonly understood.

---

## File / Project Layout
- Use feature folders or layered folders as team prefers (e.g., Controllers, Services, Data, Features/Orders).
- Keep small files; prefer one top-level public type per file.
- Use file-scoped namespaces (C# 10+) to reduce nesting.

Example:
```csharp
namespace MyCompany.MyApp.Services;

public class OrderService { ... }
```

---

## Formatting & Readability
- Indent with 4 spaces (no tabs).
- Use braces `{}` for all control blocks, even single-line bodies.
- Keep one statement per line.
- Limit line length to a reasonable width (e.g., 100-120 chars) per .editorconfig.
- Prefer explicit types unless `var` makes the type obvious.
- Use `nameof(...)` instead of hard-coded member name strings.
- Document public APIs with XML doc comments; include `<example>` and `<code>` where helpful.

XML doc example:
```csharp
/// <summary>
/// Retrieves user by identifier.
/// </summary>
/// <param name="userId">The user's id.</param>
/// <returns>The user if found; otherwise null.</returns>
public Task<User?> GetUserAsync(Guid userId) { ... }
```

---

## Language Features & Patterns
- Use async/await for I/O-bound work.
- Use expression-bodied members for simple properties/methods.
- Prefer pattern matching and switch expressions when appropriate.
- Enable nullable reference types (`#nullable enable` or in project file).
- Prefer `is null` / `is not null` over `== null` / `!= null`.
- Use `using` declarations for IDisposable:
```csharp
using var stream = File.OpenRead(path);
```
- Use `ConfigureAwait(false)` in library code that should not capture synchronization context:
```csharp
await db.SaveChangesAsync().ConfigureAwait(false);
```

---

## Performance & Exception Handling

Performance
- Avoid unnecessary allocations in hot paths.
- Use StringBuilder for repeated string concatenation in loops.
- For EF Core read-only queries, use `.AsNoTracking()`:
```csharp
var list = await context.Users.AsNoTracking().Where(u => u.IsActive).ToListAsync();
```
- Use paging, filtering, and projection to limit database payloads.

Exception handling
- Catch specific exceptions; avoid blanket `catch (Exception)` unless rethrowing/observability handling is required.
- Do not use exceptions to control normal flow.
- Ensure exceptions include sufficient context for diagnostics.
- Use custom exception types when you need to handle domain-specific errors.

---

## Security
- Validate and sanitize all external input.
- Never store secrets (connection strings, keys, passwords) in source code. Use Secret Manager, environment variables, or a secret store (Azure Key Vault, HashiCorp Vault).
- Use parameterized queries or an ORM to prevent SQL injection.
- Keep third-party dependencies up to date and monitor for CVEs.
- Follow least privilege principles for credentials and access tokens.
- Use HTTPS only for network communications in production.

Secrets example (configuration):
```csharp
// In local development: dotnet user-secrets
builder.Configuration.AddUserSecrets<Program>();
```

---

## Data Access
- Use EF Core or another well-supported ORM; keep queries efficient.
- Use projection (`Select(...)`) to avoid loading full entities when not required.
- Implement repository/DAO patterns where they add value, but avoid unnecessary abstraction over EF Core if it only duplicates features.
- Apply migrations as part of CI/CD or startup automation depending on your deployment model.
- Seed data in a controlled manner for dev/test environments.

Query example (projection):
```csharp
var dto = await context.Orders
    .Where(o => o.CustomerId == id)
    .Select(o => new OrderDto(o.Id, o.Total))
    .FirstOrDefaultAsync();
```

---

## Authentication & Authorization
- Use JWT Bearer tokens or provider-specific integrations (OpenID Connect, OAuth2).
- Protect APIs consistently (controllers and minimal APIs).
- Implement role-based or policy-based authorization as needed.
- Prefer centralized authentication middleware and short-lived tokens with refresh patterns.

Minimal JWT example (ASP.NET Core):
```csharp
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.Authority = "https://login.example.com/";
        options.Audience = "api://default";
    });
```

---

## Validation & Error Handling
- Validate models using data annotations or FluentValidation.
- Normalize validation failure responses (400) and provide useful messages.
- Centralize global exception handling with middleware that returns Problem Details (RFC 7807).
- Avoid leaking sensitive internal details in error responses.

ProblemDetails middleware example:
```csharp
app.UseExceptionHandler("/error"); // map to a handler that returns ProblemDetails
```

---

## API Versioning & Documentation
- Use API versioning strategies (URL segment, header, or media type) and document chosen approach.
- Generate OpenAPI docs with Swagger and keep it accurate.
- Document endpoints, inputs, outputs, error responses, and authentication requirements. Include examples.

Swagger example:
```csharp
services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
```

---

## Logging & Monitoring
- Use structured logging (e.g., Serilog) and include contextual information (request ids, user ids).
- Log at appropriate levels: Trace/Debug for development details, Information for key events, Warning for recoverable issues, Error/Critical for failures.
- Correlate logs using correlation IDs and propagate them through requests.
- Integrate application telemetry (Application Insights, Prometheus) for metrics and traces.

Serilog basic setup:
```csharp
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();
```

---

## Testing
- Cover critical paths with unit and integration tests.
- Use test doubles/mocks for external dependencies.
- Prefer in-memory or test-specific databases for integration tests where appropriate.
- Keep test method names and style consistent with nearby project tests.
- Test authentication/authorization flows and error paths.

Test example (xUnit minimal):
```csharp
[Fact]
public async Task GetUser_ReturnsExpected()
{
    // Arrange
    var service = new UserService(...);
    // Act
    var result = await service.GetUserAsync(id);
    // Assert
    Assert.NotNull(result);
}
```

---

## Performance Optimization
- Cache results where appropriate (in-memory, distributed caches, response caching).
- Use asynchronous programming across I/O boundaries.
- Implement pagination, filtering, and sorting for large queries.
- Use compression and caching to reduce payload sizes.
- Profile and benchmark hotspots before optimizing (measure, don't guess).

---

## Deployment & DevOps
- Containerize apps using `dotnet publish` or a hand-crafted Dockerfile; choose the approach suitable for your pipeline.
- Implement CI/CD for builds, tests, and deployments.
- Use environment-specific configurations and secrets stores in production.
- Expose health checks and readiness probes for orchestrators.
- Automate database migrations or apply them as part of controlled deployment steps.

Docker publish example:
```bash
dotnet publish -c Release -r linux-x64 -p:PublishSingleFile=true
```

---

## Practical Examples (Short Snippets)

File-scoped namespace:
```csharp
namespace Acme.App.Services;

public class EmailService { ... }
```

ConfigureAwait in library code:
```csharp
await httpClient.GetStringAsync(uri).ConfigureAwait(false);
```

StringBuilder usage:
```csharp
var sb = new StringBuilder();
for (int i = 0; i < items.Count; i++) {
    sb.Append(items[i]);
}
var result = sb.ToString();
```

EF Core AsNoTracking:
```csharp
var customers = await context.Customers.AsNoTracking()
    .Where(c => c.IsActive).ToListAsync();
```

ProblemDetails response mapping:
```csharp
app.Map("/error", (HttpContext httpContext) => Results.Problem("An unexpected error occurred."));
```

---

## Checklist (Use during PR reviews / onboarding)
- [ ] .editorconfig applied and formatting checks pass.
- [ ] Public APIs have XML documentation.
- [ ] Naming follows conventions (PascalCase / camelCase / _privateField).
- [ ] Async methods use Async suffix and avoid blocking calls.
- [ ] Input validation for external data is present.
- [ ] Sensitive data not hard-coded; secrets handled by secret store.
- [ ] EF queries use projection and AsNoTracking for read-only queries.
- [ ] Exceptions are caught specifically and meaningful logs are emitted.
- [ ] Logging includes correlation/request id where appropriate.
- [ ] Tests cover core behavior and edge cases.
- [ ] CI pipeline runs build, tests, and static analysis.
- [ ] Swagger/OpenAPI documentation available for public endpoints.
- [ ] Health checks and readiness probes configured for deployment.

---

## Localization & Maintenance
- Maintain this canonical English file as the source of truth.
- Keep localized versions (e.g., Japanese, Korean) in sync; include a short translation note with each localized file referencing this canonical document.
- When updating this file, add a short changelog entry at the top and notify maintainers of localized files.

---

## References & Further Reading
- Microsoft C# and .NET documentation for language and runtime specifics.
- EF Core documentation for data access patterns and performance guidance.
- OpenID Connect / OAuth2 specs and provider docs for authentication integration.
- Serilog and Application Insights docs for logging and telemetry.

---

If you'd like, I can:
- Create this file in your repository at .github/instructions/csharp-canonical-en.instructions.md (non-destructive name) and open a PR.
- Or produce a branch + PR with the new file and optional edits to the existing csharp.instructions.md.

Tell me whether you want me to create the file in the repository and, if so, whether to open a PR or push directly to a branch.  
