using System;
using System.Drawing;
using System.IO;

namespace ImageSizer.Lib.Persistance
{
    public interface IBitmapFactory
    {
        IBitmapFacade CreateBitmap(ImageFile imageFile);
    }    
}
