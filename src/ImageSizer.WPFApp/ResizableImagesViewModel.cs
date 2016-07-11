using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using ImageSizer.Lib;
using ImageSizer.Lib.Persistance;
using ImageSizer.WPFApp.Models;
using Prism.Commands;

namespace ImageSizer.WPFApp
{
    //TODO Try http://caliburnmicro.com/documentation/
    public class ResizableImagesViewModel : ResourceDictionary
    {
        private IBatchImageResizer _batchImageResizer;
        private DelegateCommand _resizeImagesCommand;
        private List<ImageModel> _images;
        private ImageOperations _imageOperations;

        public ResizeImagesModel ResizeImagesModel { get; private set; }

        //TODO dependency injection
        public ResizableImagesViewModel()
        {
            var directoryFacade = new DirectoryFacade();
            var fileSystemFacade = new FileSystemFacade();
            var bitmapFactory = new BitmapFactory();
            _imageOperations = new ImageOperations(fileSystemFacade, bitmapFactory);

            var directoryReader = new DirectoryReader(directoryFacade, _imageOperations);
            var imageResizer = new ImageResizer();
            _batchImageResizer = new BatchImageResizer(directoryReader, imageResizer);


            _images = new List<ImageModel>();

            ResizeImagesModel = new ResizeImagesModel();
        }

        private DelegateCommand _openFolderCommand;
        public ICommand OpenFolderCommand
        {
            get
            {
                if (_openFolderCommand == null)
                {
                    _openFolderCommand = new DelegateCommand(OpenFolderExecute);
                }
                return _openFolderCommand;
            }
        }

        public ICommand ResizeImagesCommand
        {
            get
            {
                if (_resizeImagesCommand == null)
                {
                    _resizeImagesCommand = new DelegateCommand(ResizeImagesExecute);
                }
                return _resizeImagesCommand;
            }
        }

        //TODO update button to clickable when list is populated.
        private bool CanResizeImagesExecute()
        {
            return _images.Any();
        }

        private void ResizeImagesExecute()
        {
            //TODO refactor this out
            if (ResizeImagesModel.FiftyPercentSmaller)
            {
               IList<ImageFile> iamges = _batchImageResizer.ResizeImagesOnPathByPercent(ResizeImagesModel.FolderPath, 50);
                _imageOperations.WriteImagesToDirectory(@"C:\Users\baker\Desktop", iamges);
            }
        }

        private void OpenFolderExecute()
        {
            var folderBrowserDialog = new FolderBrowserDialog();

            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                ResizeImagesModel.FolderPath = folderBrowserDialog.SelectedPath; ;
            }

            //TODO refactor
            try
            {                               
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}