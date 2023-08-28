namespace UCG.siteTRAXLite.Entities.SorEforms
{
    public class QuestionAttachmentEntity : EntityBase
    {
        private string source;
        public string Source
        {
            get
            {
                return source;
            }
            set 
            {
                SetProperty(ref source, value);
            }
        }

        private string fileName;
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                SetProperty(ref fileName, value);
            }
        }

        private long fileSize;
        public long FileSize
        {
            get
            {
                return fileSize;
            }
            set
            {
                SetProperty(ref fileSize, value);
            }
        }

        public string FileSizeInKB
        {
            get
            {
                var sizeInKb = Math.Round((double)FileSize / 1024, 2);
                return $"{sizeInKb}KB";
            }
        }

        private string _textProgress = "0%";

        public string TextProgress
        {
            get { return _textProgress; }
            set { SetProperty(ref _textProgress, value); }
        }

        private double _progress;
        public double Progress
        {
            get { return _progress; }
            set { SetProperty(ref _progress, value); }
        }

        private string contentType;
        public string ContentType
        {
            get { return contentType; }
            set { SetProperty(ref contentType, value); }
        }

        private bool isPendingUpload;
        public bool IsPendingUpload
        {
            get { return isPendingUpload; }
            set { SetProperty(ref isPendingUpload, value); }
        }

        private bool isUploading;
        public bool IsUploading
        {
            get { return isUploading; }
            set { SetProperty(ref isUploading, value); }
        }

        private bool isReadyUploading;
        public bool IsReadyUploading
        {
            get { return isReadyUploading; }
            set { SetProperty(ref isReadyUploading, value); }
        }

        private bool isPendingResponse;
        public bool IsPendingResponse
        {
            get { return isPendingResponse; }
            set { SetProperty(ref isPendingResponse, value); }
        }

        private bool isComplete;
        public bool IsComplete
        {
            get { return isComplete; }
            set { SetProperty(ref isComplete, value); }
        }
    }
}
