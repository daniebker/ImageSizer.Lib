using System.Drawing;
using System.IO;

namespace ImageSizer.Lib.Persistance
{
    public class BitmapFactory : IBitmapFactory
    {

        public IBitmapFacade CreateBitmap(ImageFile imageFile)
        {
            var newImage = new Bitmap(imageFile.Width, imageFile.Height);
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(Image.FromFile(imageFile.FilePath), 0, 0, imageFile.Width, imageFile.Height);
                SetPropertyItems(imageFile, newImage);
                return new BitmapFacade(newImage, Path.GetFileName(imageFile.FilePath));
            }
        }

        private void SetPropertyItems(ImageFile imageFile, Bitmap newImage)
        {
            foreach (var propertyItem in imageFile.PropertyItems)
            {
                newImage.SetPropertyItem(propertyItem);
            }
        }
    }
}
