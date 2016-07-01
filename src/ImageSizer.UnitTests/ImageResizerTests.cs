using System;
using System.Drawing.Imaging;
using ImageSizer.Lib;
using NUnit.Framework;

namespace ImageSizer.UnitTests
{
    [TestFixture]
    public class ImageResizerTests
    {
        private ImageResizer _imageResizer;
        private BaseImage _arbitraryLandscapeBaseImage;
        private BaseImage _arbitraryPortraitBaseImage;

        [SetUp]
        public void SetUp()
        {
            _imageResizer = new ImageResizer();
            _arbitraryLandscapeBaseImage = new BaseImage(new byte[0], new ImageSize(800, 600), "Aribtrary_filename.jpg", new PropertyItem[0]);
            _arbitraryPortraitBaseImage = new BaseImage(new byte[0], new ImageSize(600, 800), "Aribtrary_filename.jpg", new PropertyItem[0]);
        }

        [Test]
        public void ResizeByLongestEdge_GivenImage_ResizesByWidth()
        {
            BaseImage resizeBaseImage = _imageResizer.ResizeByLongestEdge(_arbitraryLandscapeBaseImage, 300);

            Assert.AreEqual(300, resizeBaseImage.Width);
        }


        [Test]
        public void ResizeByLongestEdge_GivenImage_ResizesByHeight()
        {
            BaseImage resizeBaseImage = _imageResizer.ResizeByLongestEdge(_arbitraryPortraitBaseImage, 300);

            Assert.AreEqual(300, resizeBaseImage.Height);
        }

        [Test]
        public void ResizeByLongestEdge_GivenTargetSizeIsLargerThanImage_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _imageResizer.ResizeByLongestEdge(_arbitraryLandscapeBaseImage, 1000));
        }

        [Test]
        public void MakeFiftyPercentSmaller_GivenImage_ResizesWidthByFiftyPercent()
        {
            BaseImage resizedBaseImage = _imageResizer.MakeFiftyPercentSmaller(_arbitraryLandscapeBaseImage);

            Assert.AreEqual(400, resizedBaseImage.Width);
        }

        [Test]
        public void MakeFiftyPercentSmaller_GivenImage_ResizesHeightByFiftyPercent()
        {
            BaseImage resizedBaseImage = _imageResizer.MakeFiftyPercentSmaller(_arbitraryPortraitBaseImage);

            Assert.AreEqual(400, resizedBaseImage.Height);
        }

        [Test]
        public void ResizeWidth_SetWidthTo300_WidthIsResizedButNotHeight()
        {
            BaseImage resizedImage = _imageResizer.ResizeWidthBy(_arbitraryLandscapeBaseImage, 300);

            Assert.AreEqual(_arbitraryLandscapeBaseImage.Height, resizedImage.Height);
        }

        [Test]
        public void ResizeWidth_SetWidthTo300_WidthIsResized()
        {
            BaseImage resizedImage = _imageResizer.ResizeWidthBy(_arbitraryLandscapeBaseImage, 300);

            Assert.AreEqual(300, resizedImage.Width);
        }

        [Test]
        public void ResizeWidth_GivenTargetSizeIsLargerThanImage_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _imageResizer.ResizeWidthBy(_arbitraryLandscapeBaseImage, 1000));
        }

        [Test]
        public void ResizeHeight_SetHeightTo200_WidthIsNotResized()
        {
            BaseImage resizedBaseImage = _imageResizer.ResizeHeight(_arbitraryPortraitBaseImage, 100);

            Assert.AreEqual(_arbitraryPortraitBaseImage.Width, resizedBaseImage.Width);
        }
    }
}
