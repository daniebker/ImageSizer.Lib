namespace ImageSizer.Lib
{
    public interface IImageResizer
    {
        ImageFile ResizeByPercent(ImageFile imageFile, int percent);
    }
}