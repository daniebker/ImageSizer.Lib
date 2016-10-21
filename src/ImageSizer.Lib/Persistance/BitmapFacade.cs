using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageSizer.Lib.Persistance
{
    internal class BitmapFacade : IBitmapFacade
    {
        private Bitmap _bitmapImage;
        private string _fileName;

        public BitmapFacade(Bitmap bitmapImage, string fileName)
        {
            _bitmapImage = bitmapImage;
            _fileName = fileName;
        }

        public void SaveToAs(string path, ImageFormat imageformat)
        {
            _bitmapImage.Save(Path.Combine(new string[] { path, _fileName }), imageformat);
        }
                
        public void Dispose()
        {
            _bitmapImage.Dispose();           
        }
    }
}