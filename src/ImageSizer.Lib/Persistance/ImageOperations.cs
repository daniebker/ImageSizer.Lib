using System;
using System.Collections.Generic;

namespace ImageSizer.Lib.Persistance
{
    public class ImageOperations : IImageOperations
    {
        private IBitmapFactory _bitmapFactory;
        private IFileSystemFacade _fileSystemFacade;

        public ImageOperations(IFileSystemFacade fileSystemFacade, IBitmapFactory bitmapFactory)
        {
            _fileSystemFacade = fileSystemFacade;
            _bitmapFactory = bitmapFactory;
        }

        public void WriteImagesToDirectory(string path, IList<ImageFile> list)
        {
            _fileSystemFacade.CreateDirectoryIfNotExists(path);

            foreach(ImageFile imageFile in list)
            {
                using (var bitmapFacade = _bitmapFactory.CreateBitmap(imageFile))
                {
                    bitmapFacade.SaveToAs(path, imageFile.GetImageFormat());
                }

                GC.Collect();
            }
        }

        public ImageFile ReadImageOnPath(string filePath)
        {
           return _fileSystemFacade.ImageFromFilePath(filePath);
        }
    }
}
