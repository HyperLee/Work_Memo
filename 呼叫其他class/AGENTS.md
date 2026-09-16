# Repository Guidelines

## Project

- Path: 呼叫其他class/呼叫其他class/呼叫其他class.csproj
- Target framework: net10.0
- Type: self-contained console project

## Commands

- Restore: `dotnet restore 呼叫其他class/呼叫其他class/呼叫其他class.csproj`
- Build: `dotnet build 呼叫其他class/呼叫其他class/呼叫其他class.csproj --nologo`
- Strict XML rebuild: `dotnet build 呼叫其他class/呼叫其他class/呼叫其他class.csproj --no-restore -t:Rebuild --nologo -warnaserror:CS1570,CS1571`
- Run: `dotnet run --project 呼叫其他class/呼叫其他class/呼叫其他class.csproj --no-build --nologo`
- Fallback run on a host without the matching runtime: `DOTNET_ROLL_FORWARD=Major dotnet run --project 呼叫其他class/呼叫其他class/呼叫其他class.csproj --no-build --nologo`
- Format check: `dotnet format 呼叫其他class/呼叫其他class/呼叫其他class.csproj --verify-no-changes`
- Diff check: `git diff --check`

## Style

- Follow the repository `.editorconfig`; keep four-space indentation and XML documentation valid.
- Keep the fixed smoke-test output contract: `Expected`, `Actual`, `PASS-FAIL`, and `Summary`.

## Delivery

- This refresh is local-only: do not commit or push unless explicitly requested.
- Do not add a solution, shared runner, or independent test project.

## Safety

- Do not edit GUI, old-framework, adjacent test projects, root inventory files, or unrelated projects.
