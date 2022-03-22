using System;
using System.Collections.Generic;

namespace ThrottlingTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new ThrottledRunner();
            var requests = new List<IApiRequest>();
            
            // create 100 IApiRequest  requests
            for (int i = 0; i < 300; i++)
            {
                var number = ((Int16)i).ToBase4();
                // download some random tiles from Bing maps
                requests.Add(new DownloadImage(new Uri($"http://ecn.t0.tiles.virtualearth.net/tiles/h0302303{number}.jpeg?g=400")));
            }

            
            runner.RunAsync(requests, 4).GetAwaiter().GetResult();
           
            Console.WriteLine("All done!");
        }

    }
}
