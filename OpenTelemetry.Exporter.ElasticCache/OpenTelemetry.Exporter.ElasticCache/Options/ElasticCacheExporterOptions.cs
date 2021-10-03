using System;

namespace OpenTelemetry.Exporter.ElasticCache.Options
{
  public class ElasticCacheExporterHttpOptions
  {
    public Uri Host { get; set; }
    public string DefaultIndex { get; set; }

    public ElasticCacheExporterHttpOptions(string DefaultIndex, string HostUrl)
    {
      this.Host = new Uri(HostUrl);
      this.DefaultIndex = DefaultIndex;
    }
  }
}