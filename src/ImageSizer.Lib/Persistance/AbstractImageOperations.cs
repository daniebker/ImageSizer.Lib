using System.Drawing;
using System.IO;

namespace ImageSizer.Lib.Persistance
{
    public class AbstractImageOperations
    {
        protected void SetPropertyItems(BaseImage baseImage, Bitmap newImage)
        {
            foreach (var propertyItem in baseImage.PropertyItems)
            {
                newImage.SetPropertyItem(propertyItem);
            }
        }

        protected ImageSize GetImageSize(Image image)
        {
            return new ImageSize(image.Width, image.Height);
        }
    }
}
