using System.Diagnostics;
using System.IO;
using System.Text;

namespace NuPack
{
   internal sealed class NuGetter
   {
      private readonly string _nuGetPath;
      private readonly string _outputDirectory;
      private readonly bool _withSymbols;
      private readonly string _buildConfiguration;
      private readonly string _platform;

      public NuGetter(string nuGetPath, string outputDirectory, bool withSymbols, string buildConfiguration, string platform)
      {
         _nuGetPath = nuGetPath;
         _outputDirectory = outputDirectory;
         _withSymbols = withSymbols;
         _buildConfiguration = buildConfiguration;
         _platform = platform;
      }

      public void PackCsProject(string csProjPath)
      {
         var info = GetStartInfo();
         
         info.Arguments = GetPackArgs(csProjPath);

         Process.Start(info);
      }

      private string GetPackArgs(string csProjPath)
      {
         var args = new StringBuilder();
         args.AppendFormat("pack \"{0}\"", csProjPath);

         if (!string.IsNullOrWhiteSpace(_outputDirectory))
         {
            Directory.CreateDirectory(_outputDirectory);

            args.AppendFormat(" -OutputDirectory \"{0}\"", _outputDirectory);
         }

         if (_withSymbols)
         {
            args.Append(" -Symbols");
         }

         if (!string.IsNullOrWhiteSpace(_buildConfiguration))
         {
            args.Append(" -Prop Configuration=" + _buildConfiguration);
         }

         if (!string.IsNullOrWhiteSpace(_platform))
         {
            args.Append(" -Prop Platform=" + _platform);
         }

         return args.ToString();
      }

      private ProcessStartInfo GetStartInfo()
      {
         var path = string.IsNullOrWhiteSpace(_nuGetPath) ? "nuget.exe" : _nuGetPath;

         var info = new ProcessStartInfo();
         info.FileName = path;
         info.UseShellExecute = false;

         return info;
      }
   }
}
