namespace ImageSizer.Lib.Persistance
{
    public interface IFileSystemFacade
    {
        void CreateDirectoryIfNotExists(string path);
        ImageFile ImageFromFilePath(string sOME_IMAGE_FILE_PATH);
    }
}
