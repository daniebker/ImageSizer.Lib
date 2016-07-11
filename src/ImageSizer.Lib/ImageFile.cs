using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;

namespace ImageSizer.Lib
{
    public struct ImageFile
    {
        public ImageFile(ImageSize imageSize, string filePath, IEnumerable<PropertyItem> propertyItems)
        {

            ImageSize = imageSize;
            FilePath = filePath;
            PropertyItems = propertyItems;
        }

        public ImageFile(ImageFile baseImage, ImageSize newSize) 
            : this(newSize, baseImage.FilePath, baseImage.PropertyItems)
        {
        }

        public ImageSize ImageSize { get; private set; }

        public int Height { get { return ImageSize.Height; } }

        public int Width { get { return ImageSize.Width; } }

        public string FilePath { get; private set; }

        public IEnumerable<PropertyItem> PropertyItems { get; private set; }

        public ImageFormat GetImageFormat()
        {
            string fileExtension = Path.GetExtension(FilePath);
            if (fileExtension.Equals(".jpg"))
            {
                return ImageFormat.Jpeg;
            }

            throw new NotImplementedException($@"ImageFormat {fileExtension} not supported");
        }

    }
}