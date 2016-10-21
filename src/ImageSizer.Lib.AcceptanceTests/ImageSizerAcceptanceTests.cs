using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using ImageSizer.Lib.Persistance;
using System.Drawing.Imaging;

namespace ImageSizer.Lib.AcceptanceTests
{
    [TestFixture]
    public class ImageSizerAcceptanceTests
    {
        private readonly IImageResizer imageResizer = new ImageResizer();
        private readonly IDirectoryReader directoryReader = Substitute.For<IDirectoryReader>();
        
        private const string SOME_PATH = "SOME_PATH";
        private const string SOME_OTHER_PATH = "SOME_OTHER_PATH";
        private const string SOME_OUTPUT_PATH = "SOME_OUTPUT_PATH";
        private const string SOME_IMAGE_PATH = "SOME_IMAGE_PATH.jpg";
        
        private static readonly ImageFile SOME_LANDSCAPE_IMAGE = new ImageFile(new ImageSize(800, 600), SOME_IMAGE_PATH, new PropertyItem[0]);
        private readonly ImageFile SOME_LANDSCAPE_IMAGE_RESIZED = new ImageFile(new ImageSize(400, 300), SOME_IMAGE_PATH, SOME_LANDSCAPE_IMAGE.PropertyItems);
        private static readonly ImageFile SOME_PORTRAIT_IMAGE = new ImageFile(new ImageSize(800, 1200), SOME_IMAGE_PATH, new PropertyItem[0]);
        private readonly ImageFile SOME_PORTRAIT_IMAGE_RESIZED = new ImageFile(new ImageSize(400, 600), SOME_IMAGE_PATH, SOME_PORTRAIT_IMAGE.PropertyItems);

        [Test]
        public void Given_a_folder_of_images_resizes_images_by_a_percent()
        {
            directoryReader
                .ReadImageFilesFromPath(SOME_PATH)
                .Returns(new List<ImageFile>
                {
                    SOME_LANDSCAPE_IMAGE,
                    SOME_PORTRAIT_IMAGE
                });

            var expectedResizedImages = new List<ImageFile>()
            {
                SOME_LANDSCAPE_IMAGE_RESIZED
                ,SOME_PORTRAIT_IMAGE_RESIZED
            };

            BatchImageResizer batchImageResizer = new BatchImageResizer(directoryReader, imageResizer);

            CollectionAssert.AreEqual(expectedResizedImages, batchImageResizer.ResizeImagesOnPathByPercent(SOME_PATH, 50));
        }

        //AS a user of the library
        //I want to supply a folder containing images
        //So that I can resize all images in a Folder
        //And write them to a destination folder.
        [Test]
        public void Resized_images_are_written_to_a_destination_path()
        {
            directoryReader
                  .ReadImageFilesFromPath(SOME_PATH)
                  .Returns(new List<ImageFile>
                  {
                    SOME_LANDSCAPE_IMAGE,
                    SOME_PORTRAIT_IMAGE
                  });

            BatchImageResizer batchImageResizer = new BatchImageResizer(directoryReader, imageResizer);
            IFileSystemFacade fileSystemFacadeMock = Substitute.For<IFileSystemFacade>();
            IBitmapFacade firstBitmapFacade = Substitute.For<IBitmapFacade>();
            IBitmapFacade secondBitmapFacade = Substitute.For<IBitmapFacade>();
            IBitmapFactory bitmapFactoryMock = Substitute.For<IBitmapFactory>();

            bitmapFactoryMock
                .CreateBitmap(SOME_LANDSCAPE_IMAGE_RESIZED)
                .Returns(firstBitmapFacade);

            bitmapFactoryMock
                .CreateBitmap(SOME_PORTRAIT_IMAGE_RESIZED)
                .Returns(secondBitmapFacade);

            ImageOperations imageOperations = new ImageOperations(fileSystemFacadeMock, bitmapFactoryMock);

            imageOperations.WriteImagesToDirectory(SOME_OUTPUT_PATH, batchImageResizer.ResizeImagesOnPathByPercent(SOME_PATH, 50));
            
            firstBitmapFacade
                .Received()
                .SaveToAs(SOME_OUTPUT_PATH, ImageFormat.Jpeg);

            secondBitmapFacade
                .Received()
                .SaveToAs(SOME_OUTPUT_PATH, ImageFormat.Jpeg);
        }
    }
}
