using CommandLine;

namespace NuPack
{
   internal sealed class Options
   {
      [Value(0, Required = true)]
      public string SearchDirectory { get; set; }

      [Option('r', "Recursive", DefaultValue = true, HelpText = "Recursively search directories for .nuspec files.")]
      public bool Recursive { get; set; }

      [Option('s', "Symbols", DefaultValue = true, HelpText = "Generate symbols packages.")]
      public bool Symbols { get; set; }

      [Option('o', "OutputDirectory", HelpText = "Specifies the output directory for the created NuGet package files.")]
      public string OutputDirectory { get; set; }

      [Option('n', "NuGetPath", HelpText = "Full path to nuget.exe; nuget.exe assumed to be in path if not specified.")]
      public string NuGetPath { get; set; }

      [Option('c', "BuildConfiguration", DefaultValue = "Release", HelpText = "Build configuration used for packaging.")]
      public string BuildConfiguration { get; set; }

      [Option('p', "Platform", HelpText = "Target platform (e.g. x64).")]
      public string Platform { get; set; }
   }
}
