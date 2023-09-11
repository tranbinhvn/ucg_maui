namespace UCG.siteTRAXLite.WebServices.ProgressStream
{
    public interface IProgressListener
    {
        void ChangeState(UploadState newState);

        void SetFileSize(long fileSize);
        void SetFileUploaded(long fileUploaded);
        void Complete();
    }

    public enum UploadState
    {
        PendingUpload = 0,
        Uploading = 1,
        PendingResponse = 2,
        Complete = 3
    }
}
