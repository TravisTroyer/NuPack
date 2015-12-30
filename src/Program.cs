using System;
using System.Data.Odbc;
using System.Linq;
using CommandLine;

namespace NuPack
{
   internal sealed class Program
   {
      static int Main(string[] args)
      {
         var options = Parser.Default.ParseArguments<Options>(args);

         if (!options.Errors.Any())
         {
            return Pack(options.Value);
         }

         return -1;
      }

      private static int Pack(Options options)
      {
         var nugetter = new NuGetter(options.NuGetPath, options.OutputDirectory, options.Symbols, options.BuildConfiguration);
         var packer = new Packer(nugetter);

         packer.Pack(options.SearchDirectory, options.Recursive);

         return 0;
      }
   }
}
