using System;

namespace OTel.Exporter.Elasticsearch.Options
{
  public class ElasticsearchExporterHttpOptions
  {
    public Uri Host { get; set; }
    public string DefaultIndex { get; set; }

    public TimeSpan WaitBeforeNextRetry { get; set; } = TimeSpan.FromSeconds(1);

    public int TotalRetry { get; set; } = 3;

    public ElasticsearchExporterHttpOptions(string DefaultIndex, string HostUrl)
    {
      this.Host = new Uri(HostUrl);
      this.DefaultIndex = DefaultIndex;
    }

    public ElasticsearchExporterHttpOptions(string DefaultIndex, string HostUrl, TimeSpan waitBeforeNextRetry, int totalRetry)
    {
      this.Host = new Uri(HostUrl);
      this.DefaultIndex = DefaultIndex;
      this.WaitBeforeNextRetry = waitBeforeNextRetry;
      this.TotalRetry = totalRetry;
    }
  }
}