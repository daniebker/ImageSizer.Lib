using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ImageSizer.WPFApp.Annotations;

namespace ImageSizer.WPFApp.Models
{
    public class ResizeImagesModel : System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _folderPath;
        public string FolderPath
        {
            get {return _folderPath; }
            set
            {
                _folderPath = value;
                OnPropertyChanged(nameof(FolderPath));
            }
        }

        private IList<ImageModel> _imageModels;

        public IList<ImageModel> ImageModels
        {
            get
            {
                if (_imageModels == null)
                {
                    _imageModels = new List<ImageModel>();
                }
                return _imageModels;
            }
            set
            {
                _imageModels = value;
                OnPropertyChanged(nameof(ImageModels));
            }
        }

        private bool _fiftyPercentSmaller;
        public bool FiftyPercentSmaller
        {
            get
            {
                return _fiftyPercentSmaller;
            }
            set
            {
                _fiftyPercentSmaller = value;
                OnPropertyChanged(nameof(FiftyPercentSmaller));
            }
        }
       

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
