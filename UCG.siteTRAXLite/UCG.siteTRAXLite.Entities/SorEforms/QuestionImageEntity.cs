namespace UCG.siteTRAXLite.Entities.SorEforms
{
    public class QuestionImageEntity : EntityBase
    {
        private string imageSoruce;
        public string ImageSource
        {
            get
            {
                return imageSoruce;
            }
            set 
            {
                SetProperty(ref imageSoruce, value);
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
    }
}
