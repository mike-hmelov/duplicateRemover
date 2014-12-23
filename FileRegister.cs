using System;
using System.IO;
using System.Collections.Generic;

namespace duplicateRemover
{

	class FileRegister
	{
        private Dictionary<long, List<string>> fileSizes = new Dictionary<long, List<string>>();

        public bool pushAndCheckDuplicate(string filePath)
        {
            lock (fileSizes)
            {
                var fileInfo = new FileInfo(filePath);

                if (fileSizes.ContainsKey(fileInfo.Length))
                {
                    var previousFiles = fileSizes[fileInfo.Length];
                    if (!HasIdenticalFile(previousFiles, filePath))
                        previousFiles.Add(filePath);
                    else
                        return true;    
                }
                else
                    fileSizes.Add(fileInfo.Length, new List<string>(new string[]{ filePath }));
                return false;
            }
        }

        bool HasIdenticalFile(List<string> previousFiles, string filePath)
        {
            foreach (var prevFile in previousFiles)
            {
                if (CompareFiles(prevFile, filePath))
                    return true;
            }
            return false;
        }

        bool CompareFiles(string prevFile, string filePath)
        {
            var start = DateTime.Now;
            FileStream prevStream = new FileStream(prevFile, FileMode.Open, FileAccess.Read);
            FileStream currentStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                while(prevStream.Position < prevStream.Length)
                {
                    int prevByte = prevStream.ReadByte();
                    int currentByte = currentStream.ReadByte();
                    if(prevByte != currentByte)
                        return false;
                }
                return true;
            }
            finally
            {
                prevStream.Close();
                currentStream.Close();
                Console.WriteLine("Complete check " + prevFile + " against " + filePath + " " + (DateTime.Now - start).TotalMilliseconds);
            }
        }
	}
}
