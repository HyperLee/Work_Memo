# Repository Guidelines

## Project

- Path: 計算兩數總和且不使用加減法/計算兩數總和且不使用加減法/計算兩數總和且不使用加減法.csproj
- Target framework: net10.0
- Type: self-contained console project

## Commands

- Restore: `dotnet restore 計算兩數總和且不使用加減法/計算兩數總和且不使用加減法/計算兩數總和且不使用加減法.csproj`
- Build: `dotnet build 計算兩數總和且不使用加減法/計算兩數總和且不使用加減法/計算兩數總和且不使用加減法.csproj --nologo`
- Strict XML rebuild: `dotnet build 計算兩數總和且不使用加減法/計算兩數總和且不使用加減法/計算兩數總和且不使用加減法.csproj --no-restore -t:Rebuild --nologo -warnaserror:CS1570,CS1571`
- Run: `dotnet run --project 計算兩數總和且不使用加減法/計算兩數總和且不使用加減法/計算兩數總和且不使用加減法.csproj --no-build --nologo`
- Fallback run on a host without the matching runtime: `DOTNET_ROLL_FORWARD=Major dotnet run --project 計算兩數總和且不使用加減法/計算兩數總和且不使用加減法/計算兩數總和且不使用加減法.csproj --no-build --nologo`
- Format check: `dotnet format 計算兩數總和且不使用加減法/計算兩數總和且不使用加減法/計算兩數總和且不使用加減法.csproj --verify-no-changes`
- Diff check: `git diff --check`

## Style

- Follow the repository `.editorconfig`; keep four-space indentation and XML documentation valid.
- Keep the fixed smoke-test output contract: `Expected`, `Actual`, `PASS-FAIL`, and `Summary`.

## Delivery

- This refresh is local-only: do not commit or push unless explicitly requested.
- Do not add a solution, shared runner, or independent test project.

## Safety

- Do not edit GUI, old-framework, adjacent test projects, root inventory files, or unrelated projects.
