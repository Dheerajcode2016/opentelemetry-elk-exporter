using Nest;
using OpenTelemetry.Exporter.ElasticCache.Options;
using OpenTelemetry.Exporter.Elasticsearch.Models;
using OpenTelemetry.Logs;
using System.Diagnostics;
using System.Threading.Tasks;

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
           Task.Run(() =>
              {
                  var s = GetClient().IndexDocumentAsync(Transform(objectToPush)).Result;
                  if (s.ServerError != null)
                  {

                  }
              });
        }

    private LogMessage Transform(LogRecord objectToPush)
    {
      LogMessage message = new LogMessage();
      if (objectToPush != null)
      {
        message.TraceId = objectToPush.TraceId;
        message.SpanId = objectToPush.SpanId;
        message.LogLevel = objectToPush.LogLevel;
        message.TraceFlags = objectToPush.TraceFlags;
        message.TraceState = objectToPush.TraceState;
        message.Timestamp = objectToPush.Timestamp;
        message.Exception = objectToPush.Exception;
        message.CategoryName = objectToPush.CategoryName;
        message.EventId = objectToPush.EventId;
        if (objectToPush.FormattedMessage != null)
        {
          message.Message = objectToPush.FormattedMessage;
        }
        if (objectToPush.State != null)
        {
          message.Message = objectToPush.State.ToString();
        }
      }
      return message;
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