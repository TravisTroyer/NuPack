using System;
using System.Collections.Generic;
using System.IO;

namespace NuPack
{
   internal sealed class Packer
   {
      private readonly NuGetter _nugetter;

      public Packer(NuGetter nugetter)
      {
         _nugetter = nugetter;
      }

      public void Pack(string searchDirectory, bool recursive)
      {
         var absoluteSearch = GetAbsoluteSearchPath(searchDirectory);

         var nuspecs = GetNuSpecs(absoluteSearch, recursive);

         foreach (var nuspec in nuspecs)
         {
            Pack(nuspec);
         }
      }

      private void Pack(string nuspecPath)
      {
         string csprojPath;

         if (TryGetNuSpecCsProj(nuspecPath, out csprojPath))
         {
            _nugetter.PackCsProject(csprojPath);
         }
      }

      private bool TryGetNuSpecCsProj(string nuspecPath, out string csprojPath)
      {
         csprojPath = Path.ChangeExtension(nuspecPath, ".csproj");

         return File.Exists(nuspecPath);
      }

      private IReadOnlyList<string> GetNuSpecs(string searchDirectory, bool recursive)
      {
         var searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

         return Directory.GetFiles(searchDirectory, "*.nuspec", searchOption);
      }

      private string GetAbsoluteSearchPath(string searchDirectory)
      {
         if (string.IsNullOrWhiteSpace(searchDirectory))
         {
            searchDirectory = Environment.CommandLine;
         }
         else if (!Path.IsPathRooted(searchDirectory))
         {
            searchDirectory = Path.Combine(Environment.CommandLine, searchDirectory);
         }

         if (!Directory.Exists(searchDirectory))
         {
            throw new DirectoryNotFoundException($"Search path '{searchDirectory}' not found.");
         }

         return searchDirectory;
      }
   }
}
