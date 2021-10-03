using OpenTelemetry.Exporter;
using System;

namespace OpenTelemetry.Logs
{
  public static class ElasticCacheExporterLoggingExtentions
  {
    public static OpenTelemetryLoggerOptions AddElasticCacheExporter(this OpenTelemetryLoggerOptions loggerOptions)
    {
      if (loggerOptions == null)
      {
        throw new ArgumentNullException(nameof(loggerOptions));
      }

      return loggerOptions.AddProcessor(new SimpleLogRecordExportProcessor(new ElasticCacheLogExporter()));
    }
  }
}