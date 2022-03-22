using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ThrottlingTasks
{
    public class ThrottledRunner
    {
         public async Task RunAsync(IList<IApiRequest> tasks, int maxConcurrencyLevel = 4)
        {
            int nextIndex = 0;
            var downloadTasks = new List<Task<RestResponse>>();
            while (nextIndex < maxConcurrencyLevel && nextIndex < tasks.Count)
            {
                Console.WriteLine("Queuing up initial ApiRequest #{0}.", nextIndex + 1);
                downloadTasks.Add(tasks[nextIndex].Request());
                nextIndex++;
            }

            while (downloadTasks.Count > 0)
            {
                object lastTask = null;
                try
                {
                    Task<RestResponse> downloadTask = await Task.WhenAny(downloadTasks);
                    lastTask = downloadTask.AsyncState;

                    downloadTasks.Remove(downloadTask);
                    RestResponse requestResult = await downloadTask;
                    Console.WriteLine("* Received {0}-byte ", requestResult.RawBytes.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error when executing {lastTask} Exception: {ex}");
                }

                if (nextIndex < tasks.Count)
                {
                    Console.WriteLine("New execution slot available.  Queuing up ApiRequest #{0}.", nextIndex + 1);
                    downloadTasks.Add(tasks[nextIndex].Request());
                    nextIndex++;
                }
            }
        }
    }
}

