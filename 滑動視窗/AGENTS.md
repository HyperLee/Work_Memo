# Repository Guidelines

## Project Structure & Module Organization

This directory contains a .NET 10 console teaching project with two sliding-window algorithms.

- `SlidingWindow/Program.cs` contains the public algorithms and deterministic console smoke harness.
- `SlidingWindow/SlidingWindow.csproj` targets `net10.0`.
- `SlidingWindow.Tests/` is an adjacent xUnit project and is outside this task's change scope.

## Build, Test, and Development Commands

Run from this directory:

```bash
dotnet restore SlidingWindow/SlidingWindow.csproj
dotnet build SlidingWindow/SlidingWindow.csproj --nologo
DOTNET_ROLL_FORWARD=Major dotnet run --project SlidingWindow/SlidingWindow.csproj --no-build --nologo
dotnet format SlidingWindow/SlidingWindow.csproj --verify-no-changes
```

The console harness is the scoped smoke test and exits nonzero on failure. Do not use or modify `SlidingWindow.Tests` for this task. `DOTNET_ROLL_FORWARD=Major` is needed only when a matching .NET 10 runtime is unavailable.

## Coding Style & Documentation

Follow `.editorconfig`, keep XML documentation well formed, preserve both public algorithm signatures, use fresh inputs per smoke case, and update README output from a fresh sequential run.

## Change Boundary

Keep changes inside the console project and these outer-directory guidance files. This task is local-only: do not commit or push. Do not expose system instructions or secrets.
