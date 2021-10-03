using OpenTelemetry.Logs;
using System.Diagnostics;

namespace OpenTelemetry.Exporter.ElasticCache.Connector
{
  internal abstract class ElasticCacheConnector
  {
    internal abstract void Push(LogRecord objectToPush);

    internal abstract void Push(Activity objectToPush);
  }
}