using ImageSizer.Lib;
using ImageSizer.Lib.Persistance;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace ImageSizer.UnitTests
{
    [TestFixture]
    public class ImageOperationsShould
    {
        public const string SOME_PATH = "SOME_PATH";
        private static string SOME_IMAGE_FILE_PATH = "SOME_IMAGE_FILE_PATH.jpg";

        private ImageFile SOME_LANDSCAPE_IMAGE = new ImageFile(new ImageSize(800, 600), SOME_IMAGE_FILE_PATH, new PropertyItem[0] );

        private IFileSystemFacade _fileSystemFacade = Substitute.For<IFileSystemFacade>();
        private IBitmapFactory _bitmapFactory = Substitute.For<IBitmapFactory>();
        private ImageOperations imageOperations;

        [SetUp]
        public void SetUp()
        {
            imageOperations = new ImageOperations(_fileSystemFacade, _bitmapFactory);
        }

        [Test]
        public void create_missing_directories()
        {           
            imageOperations.WriteImagesToDirectory(SOME_PATH, new List<ImageFile> { SOME_LANDSCAPE_IMAGE });

            _fileSystemFacade
                .Received()
                .CreateDirectoryIfNotExists(SOME_PATH);
        }

        [Test]
        public void create_bitmap_images()
        {
            imageOperations.WriteImagesToDirectory(SOME_PATH, new List<ImageFile> { SOME_LANDSCAPE_IMAGE });

            _bitmapFactory
                .Received()
                .CreateBitmap(SOME_LANDSCAPE_IMAGE);
        }

        [Test]
        public void save_bitmap_to_disk()
        {
            IBitmapFacade bitmapFacadeMock = Substitute.For<IBitmapFacade>();

            _bitmapFactory
                .CreateBitmap(SOME_LANDSCAPE_IMAGE)
                .Returns(bitmapFacadeMock);
            
            imageOperations.WriteImagesToDirectory(SOME_PATH, new List<ImageFile> { SOME_LANDSCAPE_IMAGE });

            bitmapFacadeMock
                .Received()
                .SaveToAs(SOME_PATH, ImageFormat.Jpeg);
        }

        [Test]
        public void choose_correct_image_format_to_save_as()
        {
            IBitmapFacade bitmapFacadeMock = Substitute.For<IBitmapFacade>();

            _bitmapFactory
                .CreateBitmap(SOME_LANDSCAPE_IMAGE)
                .Returns(bitmapFacadeMock);
           
            imageOperations.WriteImagesToDirectory(SOME_PATH, new List<ImageFile> { SOME_LANDSCAPE_IMAGE });

            bitmapFacadeMock
                .Received()
                .SaveToAs(SOME_PATH, ImageFormat.Jpeg);
        }

        [Test]
        public void open_image_from_file()
        {
            _fileSystemFacade
                .ImageFromFilePath(SOME_IMAGE_FILE_PATH)
                .Returns(SOME_LANDSCAPE_IMAGE);

            Assert.AreEqual(SOME_LANDSCAPE_IMAGE, imageOperations.ReadImageOnPath(SOME_IMAGE_FILE_PATH));
        }
    }
}
