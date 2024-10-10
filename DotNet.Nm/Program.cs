using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Nm;

static class Program
{
    static Argument<FileInfo> inputAssembly = new("assembly", "assembly to list symbols of.");

    static async Task<int> Main(string[] args)
    {
        string productVersion = FileVersionInfo
            .GetVersionInfo(Assembly.GetExecutingAssembly().Location)
            .ProductVersion;
        var rootCommand = new RootCommand($"Print symbols for given assembly.\nv{productVersion}");

        rootCommand.AddArgument(inputAssembly);
        rootCommand.SetHandler((assembly) => PrintSymbols(assembly), inputAssembly);

        var runner = new CommandLineBuilder(rootCommand)
            .UseDefaults()
            .UseTypoCorrections()
            .UseSuggestDirective()
            .UseVersionOption()
            .RegisterWithDotnetSuggest()
            .Build();

        return await runner.InvokeAsync(args);
    }

    static int PrintSymbols(FileInfo fileInfo)
    {
        if (!fileInfo.Exists)
        {
            Console.Error.WriteLine($"{fileInfo.FullName} does not exist.");
            return 1;
        }

        Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
        string assemblyVersion = assembly.GetName().Version.ToString();
        string fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
        string productVersion = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

        Console.WriteLine($"assembly: \"{fileInfo.Name}\"]");
        Console.WriteLine($"name: {assembly.GetName()?.Name ?? "anonymous"}");
        Console.WriteLine($"assemblyVersion: {assemblyVersion}");
        Console.WriteLine($"fileVersion: {fileVersion}");
        Console.WriteLine($"productVersion: {productVersion}");

        foreach(var module in assembly.GetModules())
        {
            Console.WriteLine($"m'{module}'");
        }

        foreach(var reference in assembly.GetReferencedAssemblies())
        {
            Console.WriteLine($"r'{reference}'");
        }

        foreach(var type in assembly.GetTypes())
        {
            Console.WriteLine($"t'{type}'");
            PrintTypeInfo(type);
        }

        foreach(var type in assembly.GetForwardedTypes())
        {
            Console.WriteLine($"ft'{type}'");
            PrintTypeInfo(type);
        }

        foreach(var type in assembly.GetExportedTypes())
        {
            Console.WriteLine($"xt'{type}'");
            PrintTypeInfo(type);
        }

        return 0;
    }

    static void PrintTypeInfo(Type type)
    {
        foreach(var @interface in type.GetInterfaces())
        {
            Console.WriteLine($"i'{type}:{@interface}'");
        }

        foreach(var field in type.GetFields())
        {
            Console.WriteLine($"f'{type}.{field}'");
        }

        foreach(var property in type.GetProperties())
        {
            var canRead = property.CanRead ? "r" : "";
            var canWrite = property.CanWrite ? "w" : "";

            Console.WriteLine($"p:{canRead}{canWrite}'{type}.{property.Name}'");

            foreach(var accessor in property.GetAccessors())
            {
                Console.WriteLine($"a'{type}.{property}.{accessor.Name}'");
            }
        }

        foreach(var ctor in type.GetConstructors())
        {
            Console.WriteLine($"c'{type}.{ctor.Name}'");
        }

        foreach(var method in type.GetMethods())
        {
            var isAbstract = method.IsAbstract ? "a" : "";
            var isPrivate = method.IsPrivate ? "p" : "";
            var isPublic = method.IsPublic ? "P" : "";
            var isStatic = method.IsStatic ? "s" : "";
            var isVirtual = method.IsVirtual ? "v" : "";

            Console.WriteLine($"m:{isAbstract}{isStatic}{isVirtual}{isPrivate}{isPublic}'{type}.{method.Name}() -> {method.ReturnType.Name}'");
        }
    }
}
