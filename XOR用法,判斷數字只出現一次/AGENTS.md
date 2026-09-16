# Repository Guidelines

## Project

- Path: XOR用法,判斷數字只出現一次/XOR用法,判斷數字只出現一次/XOR用法,判斷數字只出現一次.csproj
- Target framework: net8.0
- Type: self-contained console project

## Commands

- Restore: `dotnet restore XOR用法,判斷數字只出現一次/XOR用法,判斷數字只出現一次/XOR用法,判斷數字只出現一次.csproj`
- Build: `dotnet build XOR用法,判斷數字只出現一次/XOR用法,判斷數字只出現一次/XOR用法,判斷數字只出現一次.csproj --nologo`
- Strict XML rebuild: `dotnet build XOR用法,判斷數字只出現一次/XOR用法,判斷數字只出現一次/XOR用法,判斷數字只出現一次.csproj --no-restore -t:Rebuild --nologo -warnaserror:CS1570,CS1571`
- Run: `dotnet run --project XOR用法,判斷數字只出現一次/XOR用法,判斷數字只出現一次/XOR用法,判斷數字只出現一次.csproj --no-build --nologo`
- Fallback run on a host without the matching runtime: `DOTNET_ROLL_FORWARD=Major dotnet run --project XOR用法,判斷數字只出現一次/XOR用法,判斷數字只出現一次/XOR用法,判斷數字只出現一次.csproj --no-build --nologo`
- Format check: `dotnet format XOR用法,判斷數字只出現一次/XOR用法,判斷數字只出現一次/XOR用法,判斷數字只出現一次.csproj --verify-no-changes`
- Diff check: `git diff --check`

## Style

- Follow the repository `.editorconfig`; keep four-space indentation and XML documentation valid.
- Keep the fixed smoke-test output contract: `Expected`, `Actual`, `PASS-FAIL`, and `Summary`.

## Delivery

- This refresh is local-only: do not commit or push unless explicitly requested.
- Do not add a solution, shared runner, or independent test project.

## Safety

- Do not edit GUI, old-framework, adjacent test projects, root inventory files, or unrelated projects.
