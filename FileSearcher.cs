using System;

namespace duplicateRemover
{
    public class FileSearcher
    {
        string _baseDir;

        public FileSearcher(string baseDir)
        {
            this._baseDir = baseDir;
        }

        public void Start()
        {
        }

        public delegate void FileFoundHandler(string filePath);
        public event FileFoundHandler OnFileFound;
    }
}

