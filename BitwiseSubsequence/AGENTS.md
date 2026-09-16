# Repository Guidelines

## Project

- Path: BitwiseSubsequence/BitwiseSubsequence/BitwiseSubsequence.csproj
- Target framework: net8.0
- Type: self-contained console project

## Commands

- Restore: dotnet restore BitwiseSubsequence/BitwiseSubsequence/BitwiseSubsequence.csproj
- Build: dotnet build BitwiseSubsequence/BitwiseSubsequence/BitwiseSubsequence.csproj --nologo
- Strict XML rebuild: dotnet build BitwiseSubsequence/BitwiseSubsequence/BitwiseSubsequence.csproj --no-restore -t:Rebuild --nologo -warnaserror:CS1570,CS1571
- Run: dotnet run --project BitwiseSubsequence/BitwiseSubsequence/BitwiseSubsequence.csproj --no-build --nologo
- Fallback run: DOTNET_ROLL_FORWARD=Major dotnet run --project BitwiseSubsequence/BitwiseSubsequence/BitwiseSubsequence.csproj --no-build --nologo
- Format check: dotnet format BitwiseSubsequence/BitwiseSubsequence/BitwiseSubsequence.csproj --verify-no-changes
- Diff check: git diff --check

## Style

- Follow the repository .editorconfig and keep XML documentation valid.
- Keep Expected, Actual, PASS-FAIL, and Summary output stable.

## Delivery

- Local-only by default: do not commit or push.
- Do not add a solution, shared runner, or independent test project.

## Safety

- Do not edit GUI, old-framework, test, root inventory, or unrelated projects.
