# opentelemetry-elk-exporter
Open Telemetry Exporter for Elasticsearch using http protocol.

How to use?

	1. Install OTel.Exporter.Elasticsearch.Http package using Nuget Package Manager in .NET 5.0 above project
	2. add Below code in programe.cs or startup.cs 
	
	using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddOpenTelemetry(options => options
                .AddElasticsearchExporter(new ElasticsearchExporterHttpOptions("telemetry", "http://localhost:9200"))
            );
        });

        "telemetry":  this is index name in Elasticsearch for which data will be pushed
        "http://localhost:9200": This is url of Elasticsearch server.

    Example:
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
