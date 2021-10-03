using System.Collections.Generic;

namespace OpenTelemetry.Exporter
{
  public class ElasticCacheExporter<T> : BaseExporter<T> where T : class
  {
    private readonly ICollection<T> exportedItems;

    public ElasticCacheExporter(ICollection<T> exportedItems)
    {
      this.exportedItems = exportedItems;
    }

    public override ExportResult Export(in Batch<T> batch)
    {
      if (this.exportedItems == null)
      {
        return ExportResult.Failure;
      }
      foreach (var item in batch)
      {
        this.exportedItems.Add(item);
      }
      return ExportResult.Success;
    }
  }
}