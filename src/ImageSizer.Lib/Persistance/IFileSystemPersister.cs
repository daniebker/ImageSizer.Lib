namespace ImageSizer.Lib.Persistance
{
    public interface IFileSystemPersister
    {
        void PersistImageToFile(ImageFile sOME_LANDSCAPE_IMAGE_RESIZED, string sOME_OUTPUT_PATH);
    }
}
