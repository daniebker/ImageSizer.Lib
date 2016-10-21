using System.Collections.Generic;

namespace ImageSizer.Lib
{
    public interface IDirectoryReader
    {
        IEnumerable<ImageFile> ReadImageFilesFromPath(string path);
    }
}