using System.Diagnostics;
using System.Text;

namespace NuPack
{
   internal sealed class NuGetter
   {
      private readonly string _nuGetPath;
      private readonly string _outputDirectory;
      private readonly bool _withSymbols;
      private readonly string _buildConfiguration;

      public NuGetter(string nuGetPath, string outputDirectory, bool withSymbols, string buildConfiguration)
      {
         _nuGetPath = nuGetPath;
         _outputDirectory = outputDirectory;
         _withSymbols = withSymbols;
         _buildConfiguration = buildConfiguration;
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
