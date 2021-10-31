using OTel.Exporter.Elasticsearch.Options;
using System;

namespace OpenTelemetry.Trace
{
  public static class ElasticsearchExporterHelperExtensions
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "The objects should not be disposed.")]
    public static TracerProviderBuilder AddElasticsearchTraceExporter(this TracerProviderBuilder builder, ElasticsearchExporterHttpOptions options = null, Action<ElasticsearchExporterHttpOptions> configure=null)
    {
      if (builder == null)
      {
        throw new ArgumentNullException(nameof(builder));
      }
      
      configure?.Invoke(options);

      if (builder is IDeferredTracerProviderBuilder deferredTracerProviderBuilder)
      {
        return deferredTracerProviderBuilder.Configure((sp, builder) =>
        {
          builder.AddProcessor(new SimpleActivityExportProcessor(new ElasticsearchTraceExporter(options)));
        });
      }
      return builder.AddProcessor(new SimpleActivityExportProcessor(new ElasticsearchTraceExporter(options)));
    }
  }
}