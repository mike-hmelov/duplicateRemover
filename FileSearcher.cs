using System;
using System.IO;
using System.Threading.Tasks;

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
            SearchDir(_baseDir);
        }

        private void SearchDir(string workDir)
        {
            var subDirs = Directory.GetDirectories(workDir);
            Parallel.ForEach(subDirs, (subDir) =>
                {
                    SearchDir(subDir);
                });
            var files = Directory.GetFiles(workDir);
            foreach (var file in files)
                OnFileFound(file);
        }

        public delegate void FileFoundHandler(string filePath);

        public event FileFoundHandler OnFileFound;
    }
}

