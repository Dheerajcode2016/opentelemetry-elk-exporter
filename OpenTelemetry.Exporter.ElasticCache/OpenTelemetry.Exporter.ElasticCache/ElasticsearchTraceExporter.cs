using OpenTelemetry.Exporter.ElasticCache.Connector;
using OpenTelemetry.Exporter.ElasticCache.Options;

using System.Diagnostics;

namespace OpenTelemetry.Trace
{
  public class ElasticsearchTraceExporter : BaseExporter<Activity>
  {
    private ElasticsearchConnector elasticCacheConnector { get; set; }
    public ElasticsearchTraceExporter(ElasticsearchExporterHttpOptions options)
    {
        elasticCacheConnector = ConnectorFacotry.CreateConnectorInstance(options);
    }
    public override ExportResult Export(in Batch<Activity> batch)
    {
        foreach (var activity in batch)
        {
            this.elasticCacheConnector.Push(activity);
        }

        return ExportResult.Success;
    }
  }
}