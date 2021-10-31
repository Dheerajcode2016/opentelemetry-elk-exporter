using System;

namespace OTel.Exporter.Elasticsearch.Options
{
  public class ElasticsearchExporterHttpOptions
  {
    public Uri Host { get; set; }
    public string DefaultIndex { get; set; }

    public ElasticsearchExporterHttpOptions(string DefaultIndex, string HostUrl)
    {
      this.Host = new Uri(HostUrl);
      this.DefaultIndex = DefaultIndex;
    }
  }
}