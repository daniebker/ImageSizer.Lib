using System.Drawing.Imaging;
using System.IO;

namespace ImageSizer.Lib
{
    public struct BaseImage
    {
        public BaseImage(byte[] imageBytes, ImageSize imageSize, string filePath, PropertyItem[] propertyItems)
        {
            ImageBytes = imageBytes;
            ImageSize = imageSize;
            FilePath = filePath;
            PropertyItems = propertyItems;
            FileName = Path.GetFileName(FilePath);
        }
        
        public BaseImage(BaseImage baseImage, ImageSize newSize)
            : this(baseImage.ImageBytes, newSize, baseImage.FilePath, baseImage.PropertyItems)
        {

        }

        public byte[] ImageBytes { get; set; }
        
        public PropertyItem[] PropertyItems { get; set; }

        public string FilePath { get; set; }
        
        public ImageSize ImageSize { get; set; }

        public string FileName { get; set; }
        
        public int Width => ImageSize.Width;

        public int Height => ImageSize.Height;
    }
}