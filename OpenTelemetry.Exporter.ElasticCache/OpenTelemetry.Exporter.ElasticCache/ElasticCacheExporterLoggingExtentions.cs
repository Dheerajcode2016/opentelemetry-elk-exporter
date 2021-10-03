using OpenTelemetry.Exporter;
using System;
using System.Collections.Generic;

namespace OpenTelemetry.Logs
{
  public static class ElasticCacheExporterLoggingExtentions
  {
    public static OpenTelemetryLoggerOptions AddElasticCacheExporter(this OpenTelemetryLoggerOptions loggerOptions, ICollection<LogRecord> exportedItems)
    {
      if (loggerOptions == null)
      {
        throw new ArgumentNullException(nameof(loggerOptions));
      }

      if (exportedItems == null)
      {
        throw new ArgumentNullException(nameof(exportedItems));
      }

      return loggerOptions.AddProcessor(new SimpleLogRecordExportProcessor(new ElasticCacheExporter<LogRecord>(exportedItems)));
    }
  }
}