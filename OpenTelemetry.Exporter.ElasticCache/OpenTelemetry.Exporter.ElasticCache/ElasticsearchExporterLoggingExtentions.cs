using OpenTelemetry.Exporter;
using OTel.Exporter.Elasticsearch.Options;
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