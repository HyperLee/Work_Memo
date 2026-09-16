# Repository Guidelines

## Project

- Path: list內含二維陣列輸出範例/list內含二維陣列輸出範例/list內含二維陣列輸出範例.csproj
- Target framework: net8.0
- Type: self-contained console project

## Commands

- Restore: `dotnet restore list內含二維陣列輸出範例/list內含二維陣列輸出範例/list內含二維陣列輸出範例.csproj`
- Build: `dotnet build list內含二維陣列輸出範例/list內含二維陣列輸出範例/list內含二維陣列輸出範例.csproj --nologo`
- Strict XML rebuild: `dotnet build list內含二維陣列輸出範例/list內含二維陣列輸出範例/list內含二維陣列輸出範例.csproj --no-restore -t:Rebuild --nologo -warnaserror:CS1570,CS1571`
- Run: `dotnet run --project list內含二維陣列輸出範例/list內含二維陣列輸出範例/list內含二維陣列輸出範例.csproj --no-build --nologo`
- Fallback run on a host without the matching runtime: `DOTNET_ROLL_FORWARD=Major dotnet run --project list內含二維陣列輸出範例/list內含二維陣列輸出範例/list內含二維陣列輸出範例.csproj --no-build --nologo`
- Format check: `dotnet format list內含二維陣列輸出範例/list內含二維陣列輸出範例/list內含二維陣列輸出範例.csproj --verify-no-changes`
- Diff check: `git diff --check`

## Style

- Follow the repository `.editorconfig`; keep four-space indentation and XML documentation valid.
- Keep the fixed smoke-test output contract: `Expected`, `Actual`, `PASS-FAIL`, and `Summary`.

## Delivery

- This refresh is local-only: do not commit or push unless explicitly requested.
- Do not add a solution, shared runner, or independent test project.

## Safety

- Do not edit GUI, old-framework, adjacent test projects, root inventory files, or unrelated projects.
