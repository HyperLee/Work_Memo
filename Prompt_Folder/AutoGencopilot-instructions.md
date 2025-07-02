Analyze this workspace and create or update `.github/copilot-instructions.md`. The file guides future AI coding agents here.

Add:
- Core commands, especially build, lint, test (incl. single-test run), docs, migrations, etc.
- High-level architecture, including major packages, services, data stores, external APIs, etc.
- Repo-specific style rules, including formatting, imports, typing, naming, error handling, etc.
- Relevant agent rules detected in `.cursor/**`, `.cursorrules`, `AGENTS.md`, `AGENT.md`, `CLAUDE.md`, `.windsurfrules`, existing Copilot file, etc.
- Summarize important parts of README or other docs instead of copying them.

Guidelines (read more at https://aka.ms/vscode-instructions-docs):
- If `.github/copilot-instructions.md` exists, patch/merge. Never overwrite blindly.
- Be concise; skip boilerplate, generic advice, or exhaustive file listings.
- Use Markdown headings + bullets; keep prose minimal and non-repetitive.
- Cite only facts found in the repo (don't invent information).