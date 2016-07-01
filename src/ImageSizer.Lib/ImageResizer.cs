using System;
using System.Web.UI.WebControls;

namespace ImageSizer.Lib
{
    public class ImageResizer
    {
        public BaseImage ResizeByLongestEdge(BaseImage baseImage, int targetLongestEdge)
        {
            if (targetLongestEdge > baseImage.Width
                && targetLongestEdge > baseImage.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(targetLongestEdge), "Target longest edge is greater than width and height of Original Image");
            }

            int longestEdge = Math.Max(baseImage.Width, baseImage.Height);
            float ratio = (float)longestEdge / targetLongestEdge;

            ImageSize newSize = DivideImageSizeByRatio(baseImage.ImageSize, ratio);

            return new BaseImage(baseImage, newSize);
        }

        public BaseImage ResizeByPercent(BaseImage baseImage, int percent)
        {
            float ratio = 100f/(100-percent);
            
            ImageSize newSize = DivideImageSizeByRatio(baseImage.ImageSize, ratio);

            return new BaseImage(baseImage, newSize);
        }

        public BaseImage ResizeWidthBy(BaseImage baseImage, int width)
        {
            if (width > baseImage.Width)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Target width is greater than the width of the Original Image");
            }

            ImageSize newSize = new ImageSize(width, baseImage.Height);

            return new BaseImage(baseImage, newSize);
        }

        public BaseImage ResizeHeightBy(BaseImage baseImage, int height)
        {
            if (height > baseImage.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Target height is greater than the height of the Ori");
            }
            ImageSize newSize = new ImageSize(baseImage.Width, height);

            return new BaseImage(baseImage, newSize);
        }

        private ImageSize DivideImageSizeByRatio(ImageSize imageSize, float ratio)
        {
            int newWidth = (int)(imageSize.Width / ratio);
            int newHeight = (int)(imageSize.Height / ratio);

            return new ImageSize(newWidth, newHeight);

        }
    }
}
