using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSizer.Lib.Persistance
{
    public class FileSystemFacade : IFileSystemFacade
    {
        public void CreateDirectoryIfNotExists(string path)
        {
            Directory.CreateDirectory(path);
        }

        public ImageFile ImageFromFilePath(string filePath)
        {
            ImageFile returnBaseImage;

            using (var image = Image.FromFile(filePath))
            {
                ImageSize imageSize = GetImageSize(image);
                returnBaseImage = new ImageFile(imageSize, filePath, image.PropertyItems);
            }

            return returnBaseImage;
        }
        
        protected ImageSize GetImageSize(Image image)
        {
            return new ImageSize(image.Width, image.Height);
        }
    }
}
