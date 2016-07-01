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
            _arbitraryLandscapeBaseImage = new BaseImage(new byte[0], new ImageSize(800, 600), "Aribtrary_filename.jpg",
                new PropertyItem[0]);
            _arbitraryPortraitBaseImage = new BaseImage(new byte[0], new ImageSize(600, 1000), "Aribtrary_filename.jpg",
                new PropertyItem[0]);
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
            Assert.Throws<ArgumentOutOfRangeException>(
                () => _imageResizer.ResizeByLongestEdge(_arbitraryLandscapeBaseImage, 1000));
        }

        [Test]
        [TestCase(50, 400)]
        [TestCase(25, 600)]
        [TestCase(75, 200)]
        public void ResizeByPercent_GivenImage_ResizesWidthByPercent(int percent, int expectedWidth)
        {
            BaseImage resizedBaseImage = _imageResizer.ResizeByPercent(_arbitraryLandscapeBaseImage, percent);

            Assert.AreEqual(expectedWidth, resizedBaseImage.Width);
        }

        [Test]
        [TestCase(50, 500)]
        [TestCase(25, 750)]
        [TestCase(75, 250)]
        public void ResizeByPercent_GivenImage_ResizesHeightByPercent(int percent, int expectedHeight)
        {
            BaseImage resizedBaseImage = _imageResizer.ResizeByPercent(_arbitraryPortraitBaseImage, percent);

            Assert.AreEqual(expectedHeight, resizedBaseImage.Height);
        }

        [Test]
        public void ResizeWidthBy_SetWidthTo300_WidthIsResizedButNotHeight()
        {
            BaseImage resizedImage = _imageResizer.ResizeWidthBy(_arbitraryLandscapeBaseImage, 300);

            Assert.AreEqual(_arbitraryLandscapeBaseImage.Height, resizedImage.Height);
        }

        [Test]
        public void ResizeWidthBy_SetWidthTo300_WidthIsResized()
        {
            BaseImage resizedImage = _imageResizer.ResizeWidthBy(_arbitraryLandscapeBaseImage, 300);

            Assert.AreEqual(300, resizedImage.Width);
        }

        [Test]
        public void ResizeWidthBy_GivenTargetSizeIsLargerThanImage_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => _imageResizer.ResizeWidthBy(_arbitraryLandscapeBaseImage, 1000));
        }

        [Test]
        public void ResizeHeightBy_SetHeightTo200_WidthIsNotResized()
        {
            BaseImage resizedBaseImage = _imageResizer.ResizeHeightBy(_arbitraryPortraitBaseImage, 100);

            Assert.AreEqual(_arbitraryPortraitBaseImage.Width, resizedBaseImage.Width);
        }

        [Test]
        public void ResizeHeightBy_SetHeightTo200_HeightIsResized()
        {
            BaseImage resizedImage = _imageResizer.ResizeHeightBy(_arbitraryLandscapeBaseImage, 100);

            Assert.AreEqual(100, resizedImage.Height);
        }

        [Test]
        public void ResizeHeightBy_TargetSizeIsGreaterThanHeight_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _imageResizer.ResizeHeightBy(_arbitraryLandscapeBaseImage, 1000));
        }
    }
}
