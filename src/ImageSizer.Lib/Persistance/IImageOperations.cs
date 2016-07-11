using System.Collections.Generic;

namespace ImageSizer.Lib.Persistance
{
    public interface IImageOperations
    {
        ImageFile ReadImageOnPath(string filePath);

        void WriteImagesToDirectory(string path, IList<ImageFile> list);
    }
}