using OpenTelemetry.Exporter.ElasticCache.Connector;
using OpenTelemetry.Exporter.ElasticCache.Options;

using System.Diagnostics;

namespace OpenTelemetry.Trace
{
  public class ElasticsearchTraceExporter : BaseExporter<Activity>
  {
    private ElasticsearchConnector ElasticsearchConnector { get; set; }
    public ElasticsearchTraceExporter(ElasticsearchExporterHttpOptions options)
    {
        ElasticsearchConnector = ConnectorFacotry.CreateConnectorInstance(options);
    }
    public override ExportResult Export(in Batch<Activity> batch)
    {
      using var scope = SuppressInstrumentationScope.Begin();
     
      foreach (var activity in batch)
        {
            this.ElasticsearchConnector.Push(activity);
        }

        return ExportResult.Success;
    }
  }
}