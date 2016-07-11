using ImageSizer.Lib.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageSizer.Lib
{
    public class DirectoryReader : IDirectoryReader
    {
        private IList<string> endings;
        private IDirectoryFacade _directoryFacade;
        private IImageOperations _imageOperations;

        public DirectoryReader(IDirectoryFacade directoryFacade, IImageOperations imageOperations)
        {
            endings = new List<string>()
            {
                "jpg"
                ,"jpeg"
                ,"png"
            };

            _directoryFacade = directoryFacade;
            _imageOperations = imageOperations;
        }

        public IEnumerable<ImageFile> ReadImageFilesFromPath(string path)
        {
            IList<string> filesOnPath = _directoryFacade.ReadPath(path);

            var imagesOnPath = filesOnPath
                .Where(
                    file => endings
                    .Any(x => file.EndsWith(x, StringComparison.CurrentCultureIgnoreCase)))
                .ToList();

            var imageFiles = new List<ImageFile>();
            foreach(string imageOnPath in imagesOnPath)
            {
                imageFiles.Add(_imageOperations.ReadImageOnPath(imageOnPath));
            }
            return imageFiles;
        }        
    }
}
