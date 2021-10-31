using OpenTelemetry.Logs;
using System.Diagnostics;

namespace OTel.Exporter.Elasticsearch.Connector
{
  internal abstract class ElasticsearchConnector
  {
    internal abstract void Push(LogRecord objectToPush);

    internal abstract void Push(Activity objectToPush);

  }
}