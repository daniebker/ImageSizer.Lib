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
        private ImageFile _arbitraryPortraitImageFile;
        private ImageFile _arbitraryLandscapeImageFile;

        [SetUp]
        public void SetUp()
        {
            _imageResizer = new ImageResizer();

            _arbitraryPortraitImageFile = new ImageFile(new ImageSize(600, 1000), "Aribtrary_filename.jpg", new PropertyItem[0]);

            _arbitraryLandscapeImageFile = new ImageFile(new ImageSize(800, 600), "Arbitrary_file_path.jpg", new PropertyItem[0]);

        }

        [Test]
        public void ResizeByLongestEdge_GivenImage_ResizesByWidth()
        {
            ImageFile resizeBaseImage = _imageResizer.ResizeByLongestEdge(_arbitraryLandscapeImageFile, 300);

            Assert.AreEqual(300, resizeBaseImage.Width);
        }


        [Test]
        public void ResizeByLongestEdge_GivenImage_ResizesByHeight()
        {
            ImageFile resizeBaseImage = _imageResizer.ResizeByLongestEdge(_arbitraryPortraitImageFile, 300);

            Assert.AreEqual(300, resizeBaseImage.Height);
        }

        [Test]
        public void ResizeByLongestEdge_GivenTargetSizeIsLargerThanImage_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => _imageResizer.ResizeByLongestEdge(_arbitraryLandscapeImageFile, 1000));
        }

        [Test]
        [TestCase(50, 400, 300)]
        [TestCase(25, 600, 450)]
        [TestCase(75, 200, 150)]
        public void ResizeByPercent_GivenImage_ResizesWidthByPercent(int percent, int expectedWidth, int expectedHeight)
        {
            ImageFile resizedBaseImage = _imageResizer.ResizeByPercent(_arbitraryLandscapeImageFile, percent);

            Assert.AreEqual(expectedWidth, resizedBaseImage.Width);
            Assert.AreEqual(expectedHeight, resizedBaseImage.Height);
        }

        [Test]
        public void ResizeWidthBy_SetWidthTo300_WidthIsResizedButNotHeight()
        {
            ImageFile resizedImage = _imageResizer.ResizeWidthBy(_arbitraryLandscapeImageFile, 300);

            Assert.AreEqual(_arbitraryLandscapeImageFile.Height, resizedImage.Height);
        }

        [Test]
        public void ResizeWidthBy_SetWidthTo300_WidthIsResized()
        {
            ImageFile resizedImage = _imageResizer.ResizeWidthBy(_arbitraryLandscapeImageFile, 300);

            Assert.AreEqual(300, resizedImage.Width);
        }

        [Test]
        public void ResizeWidthBy_GivenTargetSizeIsLargerThanImage_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => _imageResizer.ResizeWidthBy(_arbitraryLandscapeImageFile, 1000));
        }

        [Test]
        public void ResizeHeightBy_SetHeightTo200_WidthIsNotResized()
        {
            ImageFile resizedBaseImage = _imageResizer.ResizeHeightBy(_arbitraryLandscapeImageFile, 100);

            Assert.AreEqual(_arbitraryLandscapeImageFile.Width, resizedBaseImage.Width);
        }

        [Test]
        public void ResizeHeightBy_SetHeightTo200_HeightIsResized()
        {
            ImageFile resizedImage = _imageResizer.ResizeHeightBy(_arbitraryLandscapeImageFile, 100);

            Assert.AreEqual(100, resizedImage.Height);
        }

        [Test]
        public void ResizeHeightBy_TargetSizeIsGreaterThanHeight_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _imageResizer.ResizeHeightBy(_arbitraryLandscapeImageFile, 1000));
        }
    }
}
