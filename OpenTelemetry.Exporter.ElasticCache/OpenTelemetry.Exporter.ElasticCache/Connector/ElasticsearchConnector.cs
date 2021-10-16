using OpenTelemetry.Logs;
using System.Diagnostics;

namespace OpenTelemetry.Exporter.ElasticCache.Connector
{
  internal abstract class ElasticsearchConnector
  {
    internal abstract void Push(LogRecord objectToPush);

    internal abstract void Push(Activity objectToPush);

  }
}