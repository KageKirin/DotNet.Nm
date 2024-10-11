# Collaboration guide

This guide contains the collaboration guidelines to use
for contributing and maintening this project.

To assure future compatibility and ease of maintenance,
**these are to be observed as strictly as possible**.

## Code Formatting

This project relies on the automatic code formatting
provided by **[CSharpier](https://csharpier.com/)**.

### DO NOT USE ANY OTHER CODE FORMATTING TOOL UNDER ANY CIRCUMSTANCES

This assures consistency.

### ALWAYS RUN `CSHARPIER` OVER THE WHOLE PROJECT BEFORE COMMITTING

This assures that no wrong formatting gets committed.
It also assures that the code is compilable.

NOTE: The format makerule (`make format`) will apply csharpier formatting
and set the correct line endings and BOM format (rather, remove the BOM if present).

### DO NOT CHANGE LINE ENDINGS

CSharpier restores the correct line endings anyway.

### UTF-8 NO BOM

**UTF-8 NO BOM** is set by CSharpier and must not be altered.

## Commit Convention

This project follows the **[Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/)**
guidelines, albeit with more verbosity.

* `file: add <filename>` for adding a new file.
  If the file is a source files (`.cs`), it must be empty or only contain a barebone (empty!) class.
* `file: move <path/to/filename> -> <new/folder>` for moving an existing file.
  No modifications wrt the file contents must be made.
* `file: rename <filename> -> <new filename>` for renaming an existing file.
  No modifications wrt the file contents must be made.
* `file: delete <filename>` for removing an existing file.
  If part of a refactor pass, prefer `refactor: remove <feature>`.
  A short contents line `Reason: <justification for change>` is welcome to add context.
* `feat: implement <feature description>` for implementations of a new feature, class, function.
* `refactor: <change description>` for refactoring changes.
  Refactoring changes MUST BE as atomic as possible, and as complete as feasible.
  Prefer splitting into several commits for clarity if needed.
  A short contents line `Reason: <justification for change>` is welcome to add context.
* `repo: <change description>` for changes affecting the repository.
* `build: <change description>` for changes affecting the build.
  Dependency changes (add/remove/update) go into this category.
  Changes wrt build settings are part of this category as well.
* `ci: <change description>` for changes affecting the CI scripts.
* `doc: <change description>` for changes affecting documentation only.

## Pull request convention

* PRs that contain more than 1 commit must provide a list of their commits in their body.
  NOTE: This is the default when using `gh pr create --fill|--fill-verbose`.
* The PR title should reflect the overall intent of the PR.
* PRs that contain 1 commit ought to use the same title for both the PR and the commit.
* PRs SHALL NOT contain any `fixup!` or `squash!` commits, i.e. always rebase the branch before pushing.
  NOTE: You can still overwrite the existing PR with `git push --force(-with-lease)`.

## ~~Merge~~ Squash convention

PRs will be SQUASHED, and their squashed commit title will be the PR title,
and commit message will be the PR body.

e.g.:

```text
feat (publish-nuget): add publishing to nuget.org (feature/gha-publish-nuget-org) [#7]

    - **feat (publish-nuget): add matrix parameters for publishing to nuget.org**
    - **refactor (publish-nuget): change workflow trigger to on-push-tags**
    - **refactor (publish-nuget): add workflow dispatch trigger**
    - **refactor (publish-nuget): change workflow name to filename**
```

As this might cause information loss for longer commit messages, relevant details
ought to be kept in the PR body, below the commit list.

## Language convention

**Only grammatically and orthographically correct US English is allowed.**
In case of doubt for wording and spelling, check an online thesaurus and dictionary.
