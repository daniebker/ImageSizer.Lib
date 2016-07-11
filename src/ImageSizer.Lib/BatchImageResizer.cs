using System.Collections.Generic;
using System.Linq;

namespace ImageSizer.Lib
{
    public class BatchImageResizer : IBatchImageResizer
    {
        private IDirectoryReader _directoryReader;
        private IImageResizer _imageResizer;

        public BatchImageResizer(IDirectoryReader directoryReader, IImageResizer imageResizer)
        {
            _directoryReader = directoryReader;
            _imageResizer = imageResizer;
        }


        public IList<ImageFile> ResizeImagesOnPathByPercent(string path, int percent)
        {
            IEnumerable<ImageFile> imagesOnPath = _directoryReader.ReadImageFilesFromPath(path);

            if (imagesOnPath.Any())
            {
                IList<ImageFile> resizedImages = new List<ImageFile>();

                foreach (ImageFile imageFileOnPath in imagesOnPath)
                {
                    resizedImages.Add(_imageResizer.ResizeByPercent(imageFileOnPath, percent));
                }

                return resizedImages;
            }
            return new List<ImageFile>(0);
        }
    }
}
