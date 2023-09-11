using System.Collections.Concurrent;
using UCG.siteTRAXLite.Extensions;

namespace UCG.siteTRAXLite.Helpers
{
    public class FileUploadExceptionHelper
    {
        public FileUploadExceptionHelper()
        {
            if (FileNames == null)
            {
                FileNames = new List<string>();
            }
        }
        private List<string> FileNames { get; set; }

        public void AddFileName(string fileName)
        {
            FileNames.Add(fileName);
        }

        public void Clear()
        {
            if (FileNames != null)
                FileNames.Clear();
        }

        public void ShowMessageError()
        {
            if (FileNames != null && FileNames.Count > 0)
                //FuncEx.ExcuteAsync<string, string, string, CancellationToken?>(UserDialogs.Instance.AlertAsync, $"Upload Error: {string.Join(", ", FileNames)}", null, null, null);
            return;
        }
    }

    public class ItemWithAction<T> where T : class
    {
        public Func<T, Task> UploadSingleAction { get; set; }
        public T Item { get; set; }
    }

    public class MultiUploadAction<T> where T : class
    {
        private int MaxConcurrency { get; set; }
        private CancellationTokenSource TokenSource { get; set; }
        private bool IsUploading;
        private ConcurrentQueue<ItemWithAction<T>> jobQueue;
        public MultiUploadAction(int maxConcurrency = 1)
        {
            this.MaxConcurrency = maxConcurrency;
            this.TokenSource = new CancellationTokenSource();
            jobQueue = new ConcurrentQueue<ItemWithAction<T>>();
        }

        public void Enqueue(ItemWithAction<T> item)
        {
            jobQueue.Enqueue(item);
        }

        public async Task<bool> Upload()
        {
            if (IsUploading)
                return true;
            IsUploading = true;

            // Waiting process
            while (jobQueue.Count > 0)
            {
                await FuncEx.ExcuteAsync(RunDeque);
            }
            IsUploading = false;
            return false;
        }

        private async Task RunDeque()
        {
            using (SemaphoreSlim concurrencySemaphore = new SemaphoreSlim(MaxConcurrency))
            {
                var token = TokenSource.Token;
                var tasks = new List<Task>();
                var uploadErr = new FileUploadExceptionHelper();
                while (jobQueue.TryDequeue(out var item))
                {
                    await FuncEx.ExcuteAsync(concurrencySemaphore.WaitAsync);
                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            await FuncEx.ExcuteAsync(item.UploadSingleAction, item.Item);
                        }
                        catch (FileUploadException ex)
                        {
                            var fileName = item.Item.GetType().GetProperty("FileName").GetValue(item.Item, null);
                            uploadErr.AddFileName(fileName == null ? "" : fileName.ToString());
                        }
                        finally
                        {
                            concurrencySemaphore.Release();
                        }
                    }, token));
                }
                await FuncEx.ExcuteAsync(Task.WhenAll, tasks.ToArray());
                uploadErr.ShowMessageError();
            }
        }

        public void CancelUpload()
        {
            TokenSource.Cancel();
        }
    }

    [Serializable]
    public class FileUploadException : Exception
    {
        public FileUploadException()
        {

        }

        public FileUploadException(string name)
            : base($"Upload error: {name}")
        {

        }

        protected FileUploadException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }

        public FileUploadException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
