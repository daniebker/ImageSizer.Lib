using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace ImageSizer.Lib.Persistance
{
    public class InMemoryImageOperations : AbstractImageOperations
    {
        public BaseImage OpenImage(HttpPostedFileBase httpPostedFileBase)
        {
            ImageSizer.Lib.BaseImage returnBaseImage;

            using (var image = System.Drawing.Image.FromStream(httpPostedFileBase.InputStream, false, false))
            {
                ImageSize imageSize = GetImageSize(image);
                returnBaseImage = new ImageSizer.Lib.BaseImage(ImageToByteArray(image), imageSize, httpPostedFileBase.FileName, image.PropertyItems);
            }

            return returnBaseImage;
        }
        
        public byte[] PersistImageToByteArray(ImageSizer.Lib.BaseImage baseImage)
        {
            using (var newImage = new Bitmap(baseImage.Width, baseImage.Height))
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(ByteArrayToImage(baseImage.ImageBytes), 0, 0, baseImage.Width, baseImage.Height);
                SetPropertyItems(baseImage, newImage);
                return ImageToByteArray(newImage);
            }
        }

        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                imageIn.Save(memoryStream, ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }

        private System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.Image returnImage;
            using (MemoryStream memoryStream = new MemoryStream(byteArrayIn))
            {
                returnImage = System.Drawing.Image.FromStream(memoryStream);
            }
            return returnImage;
        }
    }
}