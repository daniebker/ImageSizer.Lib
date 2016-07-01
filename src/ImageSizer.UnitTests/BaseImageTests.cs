using System.Drawing.Imaging;
using ImageSizer.Lib;
using NUnit.Framework;

namespace ImageSizer.UnitTests
{
    [TestFixture]
    public class BaseImageTests
    {
        [Test]
        public void ctor_GivenValidPath_SetsFileName()
        {
            var baseImage = new BaseImage(new byte[0], new ImageSize(800,600), @"C:\Fake\path\to\filename.jpg", new PropertyItem[0]);

            Assert.AreEqual("filename.jpg", baseImage.FileName);
        }
    }
}
