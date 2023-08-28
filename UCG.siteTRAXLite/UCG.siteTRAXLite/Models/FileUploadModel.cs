using System.Globalization;
using UCG.siteTRAXLite.Common.Utils;
using UCG.siteTRAXLite.ViewModels;
using UCG.siteTRAXLite.WebServices.ProgressStream;

namespace UCG.siteTRAXLite.Models
{
    public class FileUploadModel : BindableBase, IProgressListener
    {
        public FileUploadModel() { }
        public FileUploadModel(byte[] content) { _Content = content; }

        private string _FileName;
        public string FileName
        {
            get { return _FileName; }
            set
            {
                SetProperty(ref _FileName, value);
                OnPropertyChanged(nameof(Extension));
                OnPropertyChanged(nameof(MineType));
                OnPropertyChanged(nameof(IsImage));
                OnPropertyChanged(nameof(IsNotImage));
                OnPropertyChanged(nameof(ImageSourceStr));
            }
        }

        private byte[] _fileData;
        public byte[] FileData
        {
            get { return _fileData; }
            set { SetProperty(ref _fileData, value); }
        }

        public string Extension
        {
            get
            {
                return Path.GetExtension(FileName);
            }

        }

        public string MineType
        {
            get
            {
                return MimeTypeMap.GetMimeType(Extension);
            }
        }

        public bool IsImage
        {
            get
            {
                return MineType.Contains("image/");
            }
        }

        public bool IsNotImage
        {
            get
            {
                return !IsImage;
            }
        }

        public bool IsUWPImage
        {
            get
            {
                return IsImage && Device.RuntimePlatform == Device.UWP;
            }
        }

        public long FileSize { get; set; }
        public long FileUploaded { get; set; }
        private ImageSource _ImageSource;
        public ImageSource ImageSource
        {
            get { return _ImageSource; }
            set
            {
                SetProperty(ref _ImageSource, value);
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        private string _imageSourceStr;
        public string ImageSourceStr
        {
            get { return _imageSourceStr; }
            set
            {
                SetProperty(ref _imageSourceStr, value);
            }
        }

        private byte[] _Content;
        public byte[] GetContent() { return _Content; }
        public void SetContent(byte[] content) { _Content = content; }

        private string _ServerFilePath;
        public string ServerFilePath
        {
            get { return _ServerFilePath; }
            set
            {
                SetProperty(ref _ServerFilePath, value);
                OnPropertyChanged(nameof(ServerFilePath));
            }
        }

        private string _ServerFileName;
        public string ServerFileName
        {
            get { return _ServerFileName; }
            set
            {
                SetProperty(ref _ServerFileName, value);
                OnPropertyChanged(nameof(ServerFileName));
            }
        }

        private string _TextButton;
        public string TextButton
        {
            get
            {
                return _TextButton;
            }
            set
            {
                _TextButton = value;
                OnPropertyChanged(nameof(TextButton));
            }
        }
        public bool Uploaded { get; set; }

        public object Data { get; set; }

        public string FileSizeInKB
        {
            get
            {
                var sizeInKb = FileUtils.ByteToKB(FileSize);
                return $"{sizeInKb}KB";
            }
        }

        public string FileUploadProgress
        {
            get
            {
                var sizeInKb = FileUtils.ByteToKB(FileSize);
                var uploadInKb = FileUtils.ByteToKB(FileUploaded);
                return $"{uploadInKb} Of {sizeInKb}KB";
            }
        }

        public DateTime DateCreate { get; set; }
        public string DateCreateFormat { get { return DateCreate.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture); } }

        private float _FileUploadPercent;
        public float FileUploadPercent
        {
            get { return _FileUploadPercent; }
            set
            {
                SetProperty(ref _FileUploadPercent, value);
                OnPropertyChanged(nameof(FileUploadProgress));
            }
        }

        private UploadState _State;
        public UploadState State
        {
            get { return _State; }
            set
            {
                SetProperty(ref _State, value);
                OnPropertyChanged(nameof(IsPendingUpload));
                OnPropertyChanged(nameof(IsUploading));
                OnPropertyChanged(nameof(IsPendingResponse));
                OnPropertyChanged(nameof(IsComplete));
                OnPropertyChanged(nameof(IsReadyUploading));
            }
        }

        public void ChangeState(UploadState newState)
        {
            State = newState;
        }

        public void Complete()
        {
            State = UploadState.Complete;
        }

        public void SetFileSize(long fileSize)
        {
            FileSize = fileSize;
        }

        public void SetFileUploaded(long fileUploaded)
        {
            FileUploaded = fileUploaded;
            FileUploadPercent = ((float)this.FileUploaded) / FileSize;
        }

        private string _ServerFile;

        public string ServerFile
        {
            get { return _ServerFile; }
            set
            {
                SetProperty(ref _ServerFile, value);
            }
        }

        public bool IsPendingUpload
        {
            get { return State == UploadState.PendingUpload; }
        }
        public bool IsUploading
        {
            get { return State == UploadState.Uploading; }
        }

        public bool IsReadyUploading
        {
            get { return State == UploadState.Uploading || State == UploadState.PendingUpload; }
        }

        public bool IsPendingResponse
        {
            get { return State == UploadState.PendingResponse; }
        }

        public bool IsComplete
        {
            get { return State == UploadState.Complete; }
        }
    }
}
