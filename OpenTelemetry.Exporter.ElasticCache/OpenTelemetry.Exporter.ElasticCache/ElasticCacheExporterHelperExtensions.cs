using OpenTelemetry.Exporter;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OpenTelemetry.Trace
{
  public static class ElasticCacheExporterHelperExtensions
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "The objects should not be disposed.")]
    public static TracerProviderBuilder AddInMemoryExporter(this TracerProviderBuilder builder, ICollection<Activity> exportedItems)
    {
      if (builder == null)
      {
        throw new ArgumentNullException(nameof(builder));
      }

      if (exportedItems == null)
      {
        throw new ArgumentNullException(nameof(exportedItems));
      }

      if (builder is IDeferredTracerProviderBuilder deferredTracerProviderBuilder)
      {
        return deferredTracerProviderBuilder.Configure((sp, builder) =>
        {
          builder.AddProcessor(new SimpleActivityExportProcessor(new ElasticCacheExporter<Activity>(exportedItems)));
        });
      }

      return builder.AddProcessor(new SimpleActivityExportProcessor(new ElasticCacheExporter<Activity>(exportedItems)));
    }
  }
}