using System;
using System.Net;
using System.Threading.Tasks;

namespace ThrottlingTasks
{
    public interface IApiRequest
    {
        Task<string> Request();
    }

    public class DownloadImage : IApiRequest
    {
        private Uri uri;

        public DownloadImage(Uri uri)
        {
            this.uri = uri;
        }

        public Task<string> Request()
        {
            return new WebClient().DownloadStringTaskAsync(this.uri);
        }
    }
}
