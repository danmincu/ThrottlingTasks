using RestSharp;
using System;
using System.Threading.Tasks;

namespace ThrottlingTasks
{
    public interface IApiRequest
    {
        Task<RestResponse> Request();
    }

    public class DownloadImage : IApiRequest
    {
        private Uri uri;

        public DownloadImage(Uri uri)
        {
            this.uri = uri;
        }

        public Task<RestResponse> Request()
        {
            var client = new RestClient();
            var request = new RestRequest(this.uri,Method.Get);
            return client.ExecuteAsync(request);
        }
    }

    public class PostRequest : IApiRequest
    {
        public Task<RestResponse> Request()
        {
            var client = new RestClient();
            var request = new RestRequest("https://httpbin.org/post", Method.Post);
            return client.ExecuteAsync(request);
        }
    }
}
