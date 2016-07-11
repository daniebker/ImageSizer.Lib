using System.Collections.Generic;
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

            ResizeImagesModel = new ResizeImagesModel();
        }

        private DelegateCommand _openInputFolderCommand;
        public ICommand OpenInputFolderCommand
        {
            get
            {
                if (_openInputFolderCommand == null)
                {
                    _openInputFolderCommand = new DelegateCommand(OpenInputFolderExecute);
                }
                return _openInputFolderCommand;
            }
        }

        private DelegateCommand _openOutputFolderCommand;
        public ICommand OpenOutputFolderCommand
        {
            get
            {
                if (_openOutputFolderCommand == null)
                {
                    _openOutputFolderCommand = new DelegateCommand(OpenOutputFolderExecute);
                }
                return _openOutputFolderCommand;
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
            return true;
        }

        private void ResizeImagesExecute()
        {
            if (ResizeImagesModel.FiftyPercentSmaller)
            {
               IList<ImageFile> iamges = _batchImageResizer.ResizeImagesOnPathByPercent(ResizeImagesModel.InputFolderPath, 50);
                _imageOperations.WriteImagesToDirectory(ResizeImagesModel.OutputFolderPath, iamges);
            }
        }

        private void OpenInputFolderExecute()
        {
            DialogResult result;
            FolderBrowserDialog folderBrowserDialog = TryOpenFolderBrowserDialog(out result);

            if (result.Equals(DialogResult.OK))
            {
                ResizeImagesModel.InputFolderPath = folderBrowserDialog.SelectedPath; ;
            }
        }

        private void OpenOutputFolderExecute()
        {
            DialogResult result;
            FolderBrowserDialog folderBrowserDialog = TryOpenFolderBrowserDialog(out result);

            if (result.Equals(DialogResult.OK))
            {
                ResizeImagesModel.OutputFolderPath = folderBrowserDialog.SelectedPath; ;
            }
        }

        private FolderBrowserDialog TryOpenFolderBrowserDialog(out DialogResult result)
        {

            var folderBrowserDialog = new FolderBrowserDialog();

            result = folderBrowserDialog.ShowDialog();

            return folderBrowserDialog;
        }
    }
}