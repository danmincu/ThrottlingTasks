using System;
using System.Collections.Generic;

namespace ThrottlingTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new ThrottledRunner();
            var requests = new IApiRequest[200];
            
            // create 100 IApiRequest  requests
            for (int i = 0; i < 100; i++)
            {
                var number = ((Int16)i).ToBase4();
                // download some random tiles from Bing maps
                requests[i] = new DownloadImage(new Uri($"http://ecn.t0.tiles.virtualearth.net/tiles/h0302303{number}.jpeg?g=400"));
            }

            for (int i = 100; i < 200; i++)
            {
                requests[i] = new PostRequest();
            }

            // shuffle the elements
            (new Random()).Shuffle<IApiRequest>(requests);


            runner.RunAsync(requests, 4).GetAwaiter().GetResult();
           
            Console.WriteLine("All done!");
        }

    }
}
