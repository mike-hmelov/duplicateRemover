using System;
using System.IO;
using System.Collections.Generic;

namespace duplicateRemover
{
	class FileRemover
	{
        public void deleteFile(string filePath)
        {
            Console.WriteLine("Will delete file: " + filePath);
            File.Delete(filePath);
        }
	}

}
