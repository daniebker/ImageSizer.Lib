using ImageSizer.Lib;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSizer.UnitTests
{
    [TestFixture]
    public class BatchImageResizerShould
    {
        private const string SOME_PATH = "SOME_PATH";
        private IDirectoryReader _directoryReader = Substitute.For<IDirectoryReader>();
        private IImageResizer _imageResizer = Substitute.For<IImageResizer>();
        private BatchImageResizer _batchImageResizer;
        private const string SOME_EMPTY_PATH = "SOME_EMPTY_PATH";

        private static readonly ImageFile SOME_IMAGE_FILE = new ImageFile(new ImageSize(800, 800), SOME_PATH, new PropertyItem[0]);
        private readonly ImageFile SOME_RESIZED_IMAGE_FILE = new ImageFile(new ImageSize(400, 400), SOME_PATH, SOME_IMAGE_FILE.PropertyItems);

        private static readonly ImageFile SOME_OTHER_IMAGE_FILE = new ImageFile(new ImageSize(1200, 1200), SOME_PATH, new PropertyItem[0]);
        private readonly ImageFile SOME_OTHER_RESIZED_IMAGE_FILE = new ImageFile(new ImageSize(600, 600), SOME_PATH, SOME_OTHER_IMAGE_FILE.PropertyItems);

        [SetUp]
        public void SetUp()
        {
            _batchImageResizer = new BatchImageResizer(_directoryReader, _imageResizer);
        }

        [Test]
        public void reads_images_on_path()
        {   
            _batchImageResizer.ResizeImagesOnPathByPercent(SOME_PATH, 50);

            _directoryReader.Received().ReadImageFilesFromPath(SOME_PATH);
        }

        [Test]
        public void return_empty_list_when_no_images_on_path()
        {
            _directoryReader
                .ReadImageFilesFromPath(SOME_EMPTY_PATH)
                .Returns(new List<ImageFile>(0));

            Assert.IsFalse(_batchImageResizer.ResizeImagesOnPathByPercent(SOME_EMPTY_PATH, 50).Any());
        }

        [Test]
        public void return_list_of_images_when_path_has_images()
        {
            _directoryReader
                .ReadImageFilesFromPath(SOME_PATH)
                .Returns(new List<ImageFile>()
                {
                    SOME_IMAGE_FILE,
                    SOME_OTHER_IMAGE_FILE
                });

            Assert.IsTrue(_batchImageResizer.ResizeImagesOnPathByPercent(SOME_PATH, 50).Any());
        }

        [Test]
        public void resize_images_by_percent()
        {
            _directoryReader
                .ReadImageFilesFromPath(SOME_PATH)
                .Returns(new List<ImageFile>()
                {
                    SOME_IMAGE_FILE,
                    SOME_OTHER_IMAGE_FILE
                });

            _imageResizer
                .ResizeByPercent(SOME_IMAGE_FILE, 50)
                .Returns(SOME_RESIZED_IMAGE_FILE);

            _imageResizer
                .ResizeByPercent(SOME_OTHER_IMAGE_FILE, 50)
                .Returns(SOME_OTHER_RESIZED_IMAGE_FILE);

            var someResizedImages = new List<ImageFile>()
            {
                SOME_RESIZED_IMAGE_FILE,
                SOME_OTHER_RESIZED_IMAGE_FILE
            };

            CollectionAssert.AreEqual(someResizedImages, _batchImageResizer.ResizeImagesOnPathByPercent(SOME_PATH, 50));
        }
    }
}
