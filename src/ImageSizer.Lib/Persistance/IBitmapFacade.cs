using System;
using System.Drawing.Imaging;

namespace ImageSizer.Lib.Persistance
{
    public interface IBitmapFacade : IDisposable
    {
        void SaveToAs(string path, ImageFormat imageformat);
    }
}