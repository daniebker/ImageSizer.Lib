using System.Collections.Generic;

namespace ImageSizer.Lib
{
    public interface IBatchImageResizer
    {
        IList<ImageFile> ResizeImagesOnPathByPercent(string path, int percent);
    }
}