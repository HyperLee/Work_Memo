# Repository Guidelines

## Project

- Path: Backtracking 回溯法/Backtracking 回溯法/Backtracking 回溯法.csproj
- Target framework: net10.0
- Type: self-contained console project

## Commands

- Restore: dotnet restore Backtracking 回溯法/Backtracking 回溯法/Backtracking 回溯法.csproj
- Build: dotnet build Backtracking 回溯法/Backtracking 回溯法/Backtracking 回溯法.csproj --nologo
- Strict XML rebuild: dotnet build Backtracking 回溯法/Backtracking 回溯法/Backtracking 回溯法.csproj --no-restore -t:Rebuild --nologo -warnaserror:CS1570,CS1571
- Run: dotnet run --project Backtracking 回溯法/Backtracking 回溯法/Backtracking 回溯法.csproj --no-build --nologo
- Fallback run: DOTNET_ROLL_FORWARD=Major dotnet run --project Backtracking 回溯法/Backtracking 回溯法/Backtracking 回溯法.csproj --no-build --nologo
- Format check: dotnet format Backtracking 回溯法/Backtracking 回溯法/Backtracking 回溯法.csproj --verify-no-changes
- Diff check: git diff --check

## Style

- Follow the repository .editorconfig and keep XML documentation valid.
- Keep Expected, Actual, PASS-FAIL, and Summary output stable.

## Delivery

- Local-only by default: do not commit or push.
- Do not add a solution, shared runner, or independent test project.

## Safety

- Do not edit GUI, old-framework, test, root inventory, or unrelated projects.
