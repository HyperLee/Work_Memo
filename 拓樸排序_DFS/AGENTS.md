# Repository Guidelines

## Project

- Path: 拓樸排序_DFS/拓樸排序_DFS/拓樸排序_DFS.csproj
- Target framework: net8.0
- Type: self-contained console project

## Commands

- Restore: dotnet restore 拓樸排序_DFS/拓樸排序_DFS/拓樸排序_DFS.csproj
- Build: dotnet build 拓樸排序_DFS/拓樸排序_DFS/拓樸排序_DFS.csproj --nologo
- Strict XML rebuild: dotnet build 拓樸排序_DFS/拓樸排序_DFS/拓樸排序_DFS.csproj --no-restore -t:Rebuild --nologo -warnaserror:CS1570,CS1571
- Run: dotnet run --project 拓樸排序_DFS/拓樸排序_DFS/拓樸排序_DFS.csproj --no-build --nologo
- Fallback run: DOTNET_ROLL_FORWARD=Major dotnet run --project 拓樸排序_DFS/拓樸排序_DFS/拓樸排序_DFS.csproj --no-build --nologo
- Format check: dotnet format 拓樸排序_DFS/拓樸排序_DFS/拓樸排序_DFS.csproj --verify-no-changes
- Diff check: git diff --check

## Style

- Follow the repository .editorconfig and keep XML documentation valid.
- Keep Expected, Actual, PASS-FAIL, and Summary output stable.

## Delivery

- Local-only by default: do not commit or push.
- Do not add a solution, shared runner, or independent test project.

## Safety

- Do not edit GUI, old-framework, test, root inventory, or unrelated projects.
