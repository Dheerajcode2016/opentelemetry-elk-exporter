using OpenTelemetry.Exporter;
using OpenTelemetry.Exporter.ElasticCache.Options;
using System;

namespace OpenTelemetry.Logs
{
  public static class ElasticCacheExporterLoggingExtentions
  {
    public static OpenTelemetryLoggerOptions AddElasticCacheExporter(this OpenTelemetryLoggerOptions loggerOptions, ElasticCacheExporterHttpOptions options = null)
    {
      if (loggerOptions == null)
      {
        throw new ArgumentNullException(nameof(loggerOptions));
      }

      return loggerOptions.AddProcessor(new SimpleLogRecordExportProcessor(new ElasticCacheLogExporter(options)));
    }
  }
}