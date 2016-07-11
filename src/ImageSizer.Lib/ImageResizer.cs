using System;

namespace ImageSizer.Lib
{
    public class ImageResizer : IImageResizer
    {
        public ImageFile ResizeByLongestEdge(ImageFile imageFile, int targetLongestEdge)
        {
            if (targetLongestEdge > imageFile.Width
                && targetLongestEdge > imageFile.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(targetLongestEdge), "Target longest edge is greater than width and height of Original Image");
            }

            int longestEdge = Math.Max(imageFile.Width, imageFile.Height);
            float ratio = (float)longestEdge / targetLongestEdge;

            ImageSize newSize = DivideImageSizeByRatio(imageFile.ImageSize, ratio);

            return new ImageFile(imageFile, newSize);
        }

        public ImageFile ResizeByPercent(ImageFile imageFile, int percent)
        {
            float ratio = 100f/(100-percent);
            
            ImageSize newSize = DivideImageSizeByRatio(imageFile.ImageSize, ratio);

            return new ImageFile(imageFile, newSize);
        }

        public ImageFile ResizeWidthBy(ImageFile imageFile, int width)
        {
            if (width > imageFile.Width)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Target width is greater than the width of the Original Image");
            }

            ImageSize newSize = new ImageSize(width, imageFile.Height);

            return new ImageFile(imageFile, newSize);
        }

        public ImageFile ResizeHeightBy(ImageFile imageFile, int height)
        {
            if (height > imageFile.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Target height is greater than the height of the Ori");
            }
            ImageSize newSize = new ImageSize(imageFile.Width, height);

            return new ImageFile(imageFile, newSize);
        }

        private ImageSize DivideImageSizeByRatio(ImageSize imageSize, float ratio)
        {
            int newWidth = (int)Math.Round(imageSize.Width / ratio, MidpointRounding.AwayFromZero);
            int newHeight = (int)Math.Round(imageSize.Height / ratio, MidpointRounding.AwayFromZero);

            return new ImageSize(newWidth, newHeight);

        }
    }
}
