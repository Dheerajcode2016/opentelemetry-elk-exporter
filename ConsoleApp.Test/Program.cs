using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using System;
using System.Diagnostics;
using System.Linq;
using OTel.Exporter.Elasticsearch.Options;

namespace ConsoleApp.Test
{
  internal class Program
  {
    private static ActivitySource activitySource = new ActivitySource("OTel.POC.Web", "ASP.NET Core 5.0");

    private static void Main(string[] args)
    {
      Activity.DefaultIdFormat = ActivityIdFormat.W3C;
      Activity.ForceDefaultIdFormat = true;
      using var loggerFactory = LoggerFactory.Create(builder =>
      {
        builder.AddOpenTelemetry(options => options
            .AddElasticsearchExporter(new ElasticsearchExporterHttpOptions("telemetry", "http://localhost:9200"))
            );
      });
      using (var activity = activitySource.StartActivity("ActivityName"))
      {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogInformation("Hello from {name} {price}.", "tomato", 2.99);
        Stopwatch s = new Stopwatch();
        s.Start();
        Stopwatch s1 = new Stopwatch();
        long[] timekeeping = new long[1000];   
        for (int i = 0; i < 1000; i++)
        {

                    s1.Reset();
                    s1.Start();
                    logger.LogInformation($"Counting from {i} to 100");
                    s1.Stop();
                    timekeeping[i] = s1.ElapsedMilliseconds;
        }
        s.Stop();
        Console.WriteLine($"Time Elapsed - {s.ElapsedMilliseconds} ms");
        Console.WriteLine("Average time takemn:"+ timekeeping.Average()+" ms");
      }
      Console.ReadLine();
    }
  }
}