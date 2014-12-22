using System;
using System.IO;

namespace duplicateRemover
{
	class MainClass
	{
		static void Usage ()
		{
			Console.WriteLine ("Usage: ");
			Console.WriteLine ();
			Console.WriteLine ("\t duplicateRemover <working dir>"); 
		}

		static void doSearchAndDelete (Config config)
		{
			if (!Directory.Exists (config.WorkingDir))
				throw new DirectoryNotFoundException (config.WorkingDir);
            var searcher = new FileSearcher(config.WorkingDir);
            searcher.OnFileFound += (filePath) => 
            {
            };

            searcher.Start();
		}

		public static void Main (string[] args)
		{
			if (args.Length <= 0)
            {
				Console.Error.WriteLine ("Working directory should be provided");
				Usage ();
                return;
			}
			var config = new Config ();
			config.WorkingDir = args [0];
			doSearchAndDelete (config);
		}
	}
}
