using OpenTelemetry.Exporter;
using OpenTelemetry.Exporter.ElasticCache.Options;
using System;

namespace OpenTelemetry.Logs
{
  public static class ElasticsearchExporterLoggingExtentions
  {
    public static OpenTelemetryLoggerOptions AddElasticsearchExporter(this OpenTelemetryLoggerOptions loggerOptions, ElasticsearchExporterHttpOptions options = null)
    {
      if (loggerOptions == null)
      {
        throw new ArgumentNullException(nameof(loggerOptions));
      }
      if (options == null)
      {
        options = new ElasticsearchExporterHttpOptions("telemetry", "http://localhost:9200"); 
      }

      return loggerOptions.AddProcessor(new SimpleLogRecordExportProcessor(new ElasticsearchLogExporter(options)));
    }
  }
}