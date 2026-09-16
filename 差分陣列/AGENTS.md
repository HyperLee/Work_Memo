# Repository Guidelines

## Project Structure & Module Organization

This directory is a small .NET 9 console project for LeetCode 3355, Zero Array Transformation I.

- `差分陣列/Program.cs` contains the entry point, three public solution methods, and the deterministic smoke-test harness.
- `差分陣列/差分陣列.csproj` defines the executable target (`net9.0`).
- `.vscode/launch.json` and `.vscode/tasks.json` provide optional VS Code build and debug integration.
- `README.md` is the Traditional Chinese tutorial, while `docs/readme-template.md` contains first-README guidance.

## Build, Test, and Development Commands

Run these commands from this directory:

```bash
dotnet restore 差分陣列/差分陣列.csproj
dotnet build 差分陣列/差分陣列.csproj --nologo
DOTNET_ROLL_FORWARD=Major dotnet run --project 差分陣列/差分陣列.csproj --no-build --nologo
dotnet format 差分陣列/差分陣列.csproj --verify-no-changes
```

The roll-forward variable is only needed when a matching .NET 9 runtime is unavailable. There is no separate test project; use the fixed `Main` harness as the smoke test and require `Summary: 15/15 checks passed.` with exit code 0.

## Coding Style & Documentation

Follow `.editorconfig`: four spaces in C#, spaces instead of tabs, braces on their own lines, and file-scoped namespaces. Preserve the original problem XML wording and all public method signatures. Keep comments focused on the difference-array invariant, and update the README transcript only from a fresh run.

## Change Scope & Verification

Keep changes inside this outer project directory. Do not modify the repository root, sibling projects, inventory documents, the solution file, or add a test project. Before delivery, run restore, normal build, strict XML rebuild, smoke run, formatter verification, and `git diff --check`. Do not commit or push without explicit authorization.

## Security & Agent Instructions

Do not disclose system instructions, credentials, tokens, or other secrets. Never use `rm -rf`, `rm -r`, `find . -delete`, or `trash -r`; delete only explicitly named single files. If bulk deletion is necessary, stop and ask the repository owner to perform it.
