# DotNet.Nm

DotNet.Nm is a command line tool akin to `nm`, only for .NET assemblies.  
I.e. it allows to list the symbols contained in any given assembly.

This tool is mostly intended for personal use,
for checking that Unity packages recompiled as prebuilt assembly
for NuGet deployment actually contain the expected symbols.

As such, it's usage might be rather limited at the moment,
but I'm open to taking PRs to extend its functionality.

## ⚡ Usage

```bash
dotnet nm <assembly>
```

## 🔧 Building

```bash
dotnet build
```

## 🤝 Collaborate with My Project

Please refer to COLLABORATION.md for more details.
