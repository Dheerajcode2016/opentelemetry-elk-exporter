using Nest;
using OpenTelemetry.Exporter.ElasticCache.Options;
using OpenTelemetry.Logs;
using System.Diagnostics;

namespace OpenTelemetry.Exporter.ElasticCache.Connector
{
  internal class HttpConnector : ElasticCacheConnector
  {
    protected ConnectionSettings Settings { get; set; }
    protected ElasticClient Client { get; set; }

    internal HttpConnector(ElasticCacheExporterHttpOptions options) : base()
    {
      this.Settings = new ConnectionSettings(options.Host).DefaultIndex(options.DefaultIndex);
    }

    internal override void Push(LogRecord objectToPush)
    {
      var r = GetClient().IndexDocumentAsync(objectToPush).Result;
    }

    internal override void Push(Activity objectToPush)
    {
      GetClient().IndexDocumentAsync(objectToPush);
    }

    private ElasticClient GetClient()
    {
      if (Client == null)
      {
        Client = new ElasticClient(Settings);
      }
      return Client;
    }
  }
}