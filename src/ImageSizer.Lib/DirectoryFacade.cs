using System;
using System.Collections.Generic;
using System.IO;

namespace ImageSizer.Lib
{
    public class DirectoryFacade : IDirectoryFacade
    {
        public IList<string> ReadPath(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}
