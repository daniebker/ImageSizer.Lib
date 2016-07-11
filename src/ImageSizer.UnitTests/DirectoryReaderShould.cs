
using ImageSizer.Lib.Persistance;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace ImageSizer.Lib.Tests
{
    [TestFixture]
    public class DirectoryReaderShould
    {
        private static IDirectoryFacade _directoryFacadeMock;
        private static IImageOperations _imageOperationsMock;
        private IDirectoryReader directoryReader;

        private readonly string SOME_PATH = "SOME_PATH";
        private const string SOME_JPG_PATH = @"C:\Path\To\Jpg.jpg";
        private const string SOME_JPEG_PATH = @"C:\Path\To\Jpeg.jpeg";
        private const string SOME_OTHER_JPG_PATH = @"C:\Path\To\Jpg1.jpg";
        private const string SOME_TXT_FILE_PATH = @"C:\Path\To\file.txt";
        private const string SOME_PNG_PATH = @"C:\Path\To\Jpg.png";
        private const string SOME_OTHER_PNG_PATH = @"C:\Path\To\Jpg1.png";
        private const string SOME_CAPS_PNG_PATH = @"C:\Path\To\Jpg2.PNG";

        [SetUp]
        public void SetUp()
        {
            _directoryFacadeMock = Substitute.For<IDirectoryFacade>();
            _imageOperationsMock  = Substitute.For<IImageOperations>();
            directoryReader = new DirectoryReader(_directoryFacadeMock, _imageOperationsMock);
        }

        [Test]
        public void Read_jpg_image_files_from_directory_path()
        {
            _directoryFacadeMock
                .ReadPath(SOME_PATH)
                .Returns(new List<string>()
                {
                    SOME_TXT_FILE_PATH,
                    SOME_JPEG_PATH,
                    SOME_JPEG_PATH,
                    SOME_OTHER_JPG_PATH
                });
                        
            directoryReader.ReadImageFilesFromPath(SOME_PATH);
            
            _imageOperationsMock
                .ReceivedWithAnyArgs(3)
                .ReadImageOnPath(Arg.Any<string>());
        }

        [Test]
        public void Read_png_files_from_directory_path()
        {
            _directoryFacadeMock
               .ReadPath(SOME_PATH)
               .Returns(new List<string>()
               {
                    SOME_PNG_PATH,
                    SOME_OTHER_PNG_PATH,
                    SOME_CAPS_PNG_PATH
               });

            var directoryReader = new DirectoryReader(_directoryFacadeMock, _imageOperationsMock);

            directoryReader.ReadImageFilesFromPath(SOME_PATH);

            _imageOperationsMock
                .ReceivedWithAnyArgs(3)
                .ReadImageOnPath(Arg.Any<string>());

        }
    }
}
