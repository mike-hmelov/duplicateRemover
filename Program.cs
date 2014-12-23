using System;
using System.IO;
using System.Collections.Generic;

namespace duplicateRemover
{
    class MainClass
    {
        static void Usage()
        {
            Console.WriteLine("Usage: ");
            Console.WriteLine();
            Console.WriteLine("\t duplicateRemover <working dir>"); 
        }

        static void doSearchAndDelete(Config config)
        {
            var searcher = new FileSearcher(config.WorkingDir);
            var register = new FileRegister();
            var remover = new FileRemover();

            searcher.OnFileFound += (filePath) =>
                {
                    if(register.pushAndCheckDuplicate(filePath))
                    {
                        remover.deleteFile(filePath);
                    }
                };
            searcher.Start();
        }

        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("Working directory should be provided");
                Usage();
                return;
            }
            var config = new Config();
            config.WorkingDir = args[0];

            if (!Directory.Exists(config.WorkingDir))
                throw new DirectoryNotFoundException(config.WorkingDir);

            doSearchAndDelete(config);
        }
    }
}
