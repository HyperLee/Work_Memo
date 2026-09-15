# Repository Guidelines

## Project Structure & Module Organization

This repository contains one .NET 10 console application:

- `3乘5陣列取總和.sln` is the solution file.
- `3乘5陣列取總和/3乘5陣列取總和.csproj` defines the executable project with nullable reference types enabled.
- `3乘5陣列取總和/Program.cs` contains `Main` plus the array-sum, random-array, and matrix-printing helpers.
- `README.md` documents the algorithm and expected console output. `.vscode/` provides the Debug build task and F5 launch profile; `.github/` contains repository prompts, agents, and C# guidance.

No test or asset directories exist. `bin/`, `obj/`, and other build artifacts are generated locally and ignored by Git.

## Build, Test, and Development Commands

Run these commands from the repository root with the .NET 10 SDK installed:

```powershell
dotnet restore ".\3乘5陣列取總和\3乘5陣列取總和.csproj"
dotnet build ".\3乘5陣列取總和.sln" --configuration Debug
dotnet run --project ".\3乘5陣列取總和\3乘5陣列取總和.csproj"
```

`dotnet build` compiles the solution, while `dotnet run` prints both the fixed and random 3×5 arrays. In VS Code, F5 runs the configured Debug build task first.

## Coding Style & Naming Conventions

Follow `.editorconfig`: use four spaces for C# indentation, braces on new lines, file-scoped namespaces, and no tabs. Use PascalCase for types and new public methods, camelCase for locals and parameters, and an `I` prefix for interfaces. Existing helper names such as `cal` and `printarray` are part of the sample; do not rename them without an explicit scope. Add XML documentation for public APIs, and use comments to explain non-obvious decisions. Keep nullable annotations enabled and avoid unrelated formatting changes.

## Testing Guidelines

No automated test project, framework, or coverage gate is configured yet. For every change, run the application and verify the deterministic line `fixed array sum: 120`; the random-array sum is expected to vary. If tests are added, place them in a `[ProjectName].Tests` project, name them after observable behavior, and document the exact test command in the pull request.

## Commit & Pull Request Guidelines

Recent history uses concise imperative messages, with `feat:` for new work and plain imperative messages for maintenance. Prefer `<type>: <imperative summary>`, such as `docs: add contributor guidelines`. Pull requests should state the purpose, summarize changed files, include commands and relevant output used for verification, and link a related issue when one exists. Screenshots are unnecessary for this console-only project; include representative output when it clarifies behavior.

## Security & Configuration

Do not commit secrets, API keys, `.env` files, or local IDE settings. Keep generated build output untracked, and review `.gitignore` before adding new tooling or configuration.
