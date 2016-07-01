using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageSizer.Lib.Persistance
{
    public class FileSystemImageOperations : AbstractImageOperations
    {
        public BaseImage OpenImage(string filePath)
        {
            ImageSizer.Lib.BaseImage returnBaseImage;

            using (var image = System.Drawing.Image.FromFile(filePath))
            {
                ImageSize imageSize = GetImageSize(image);
                returnBaseImage = new BaseImage(new byte[] {}, imageSize, filePath, image.PropertyItems);
            }

            return returnBaseImage;
        }

        public void PersistImageToFileSystem(ImageSizer.Lib.BaseImage baseImage, string filePath)
        {
            using (var newImage = new Bitmap(baseImage.Width, baseImage.Height))
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(System.Drawing.Image.FromFile(baseImage.FilePath), 0, 0, baseImage.Width, baseImage.Height);
                SetPropertyItems(baseImage, newImage);
                newImage.Save(Path.Combine(new[] { filePath, baseImage.FileName }), ImageFormat.Jpeg);
            }
        }
    }
}
