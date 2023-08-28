using System.Diagnostics.Contracts;
using System.Net;

namespace UCG.siteTRAXLite.WebServices.ProgressStream
{
    public class ProgressableStreamContent : HttpContent
    {
        private const int defaultBufferSize = 4096;

        private Stream content { get; set; }
        private long contentLength { get; set; }
        private int bufferSize;
        private bool contentConsumed;
        private IProgressListener uploadListener;
        private string filePath;

        public ProgressableStreamContent(string path, IProgressListener uploadListener)
        {
            this.filePath = path;
            this.uploadListener = uploadListener;
            this.bufferSize = defaultBufferSize;
            this.contentLength = File.ReadAllBytes(filePath).Length;
        }
        public ProgressableStreamContent(Stream content, IProgressListener uploadListener) : this(content, defaultBufferSize, uploadListener) { }

        public ProgressableStreamContent(Stream content, int bufferSize, IProgressListener uploadListener)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException("bufferSize");
            }

            this.content = content;
            this.contentLength = content.Length;
            this.bufferSize = bufferSize;
            this.uploadListener = uploadListener;
        }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            Contract.Assert(stream != null);

            using (Stream source = File.OpenRead(filePath))
            {
                byte[] buffer = new byte[this.bufferSize];
                var uploaded = 0;
                int bytesRead;
                uploadListener.ChangeState(UploadState.Uploading);
                while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    uploadListener.SetFileUploaded(uploaded += bytesRead);
                    await stream.WriteAsync(buffer, 0, bytesRead);
                }
                await stream.FlushAsync();
                uploadListener.ChangeState(UploadState.Complete);
            }
        }

        protected override bool TryComputeLength(out long length)
        {
            length = contentLength;
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    content.Dispose();
            //}
            base.Dispose(disposing);
        }

        private void PrepareContent()
        {
            if (contentConsumed)
            {
                if (content.CanSeek)
                {
                    content.Position = 0;
                }
                else
                {
                    throw new InvalidOperationException("SR.net_http_content_stream_already_read");
                }
            }

            contentConsumed = true;
        }
    }
}
