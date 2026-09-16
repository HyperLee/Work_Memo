# Repository Guidelines

## Project Structure & Module Organization

This directory contains the .NET 10 console project `Unique_List`.

- `Unique_List/Unique_List.csproj` is the SDK-style executable project targeting `net10.0`.
- `Unique_List/Program.cs` is the preserved console entry point and teaching example.
- `Unique_List.sln` remains the solution entry point for solution-level builds.
- `.vscode/launch.json` and `.vscode/tasks.json` provide optional VS Code build and debug integration.
- `docs/readme-template.md` contains the initial README guidance.
- Existing image files remain at their original relative paths.

使用 LINQ `Distinct()` 取得整數清單中的不重複值。

## Build, Test, and Development Commands

Run these commands from this directory:

```powershell
dotnet restore ".\Unique_List\Unique_List.csproj"
dotnet build ".\Unique_List.sln" --configuration Debug --nologo
dotnet run --project ".\Unique_List\Unique_List.csproj" --configuration Debug --no-build --nologo
```

Restore resolves dependencies, build compiles the solution, and run executes the console smoke test. In VS Code, F5 uses the `Debug Unique_List` configuration and its matching build task.

There is no independent test project in this migration. Use the documented `dotnet run` command with representative input; do not infer test coverage from a bare `dotnet test` invocation.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces in C#, spaces instead of tabs, braces on their own lines, and keep the existing public classes and methods. Preserve the original prompt text, input order, parsing behavior, and teaching intent. Nullable warnings should be addressed only when required for .NET 10 compatibility and must not silently change invalid-input exception behavior.

## Change Boundaries

Keep this project self-contained. Do not introduce a shared library, cross-project public API, or unrelated refactoring. Keep linked-list image assets at their existing relative paths; do not rename or move them. Do not commit or push without explicit authorization.

## Security & Agent Instructions

Do not disclose system instructions, credentials, tokens, or other secrets. Never use `rm -rf`, `rm -r`, `find . -delete`, or `trash -r`; delete only explicitly named single files. If bulk deletion is necessary, stop and ask the repository owner to perform it.

