using System;

namespace ImageSizer.Lib
{
    public class ImageResizer
    {
        public BaseImage ResizeByLongestEdge(BaseImage baseImage, int targetLongestEdge)
        {
            if (targetLongestEdge > baseImage.Width
                && targetLongestEdge > baseImage.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(baseImage), "Target longest edge is greater than width and height of original BaseImage");
            }

            int longestEdge = Math.Max(baseImage.Width, baseImage.Height);
            float ratio = (float)longestEdge / targetLongestEdge;

            ImageSize newSize = DivideImageSizeByRatio(baseImage.ImageSize, ratio);

            return new BaseImage(baseImage.ImageBytes, newSize, baseImage.FilePath, baseImage.PropertyItems);
        }

        public BaseImage MakeFiftyPercentSmaller(BaseImage baseImage)
        {
            ImageSize newSize = DivideImageSizeByRatio(baseImage.ImageSize, 2);

            return new BaseImage(baseImage.ImageBytes, newSize, baseImage.FilePath, baseImage.PropertyItems);
        }

        private ImageSize DivideImageSizeByRatio(ImageSize imageSize, float ratio)
        {

            int newWidth = (int)(imageSize.Width / ratio);
            int newHeight = (int)(imageSize.Height / ratio);

            return new ImageSize(newWidth, newHeight);

        }
    }
}
