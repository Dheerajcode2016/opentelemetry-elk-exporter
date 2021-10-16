using OpenTelemetry.Exporter.ElasticCache.Connector;
using OpenTelemetry.Exporter.ElasticCache.Options;
using OpenTelemetry.Logs;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenTelemetry.Exporter
{
    public class ElasticsearchLogExporter : BaseExporter<LogRecord>
  {
    private ElasticsearchConnector elasticsearchConnector { get; set; }

    public ElasticsearchLogExporter(ElasticsearchExporterHttpOptions options)
    {
      this.elasticsearchConnector = ConnectorFacotry.CreateConnectorInstance(options);
    }

    public override ExportResult Export(in Batch<LogRecord> batch)
    {
      using var scope = SuppressInstrumentationScope.Begin();
      
      Parallel.ForEach(YieldLogRecord(batch), logRecord => 
      {
        this.elasticsearchConnector.Push(logRecord);
      });
      
      return ExportResult.Success;
    }
    private IEnumerable<LogRecord> YieldLogRecord(Batch<LogRecord> logRecordBatch)
    {
      foreach (var item in logRecordBatch)
      {
        yield return item;
      }
    }
  }
}