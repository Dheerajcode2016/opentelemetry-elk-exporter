using OpenTelemetry.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTelemetry.Trace
{
  public class ElasticCacheTraceExporter : BaseExporter<Activity>
  {
    public override ExportResult Export(in Batch<Activity> batch)
    {
      foreach (var activity in batch)
      {
        Console.WriteLine($"Activity.Id:          {activity.Id}");
        if (!string.IsNullOrEmpty(activity.ParentId))
        {
          Console.WriteLine($"Activity.ParentId:    {activity.ParentId}");
        }

        Console.WriteLine($"Activity.ActivitySourceName: {activity.Source.Name}");
        Console.WriteLine($"Activity.DisplayName: {activity.DisplayName}");
        Console.WriteLine($"Activity.Kind:        {activity.Kind}");
        Console.WriteLine($"Activity.StartTime:   {activity.StartTimeUtc:yyyy-MM-ddTHH:mm:ss.fffffffZ}");
        Console.WriteLine($"Activity.Duration:    {activity.Duration}");
        if (activity.TagObjects.Any())
        {
          Console.WriteLine("Activity.TagObjects:");
          foreach (var tag in activity.TagObjects)
          {
            var array = tag.Value as Array;

            if (array == null)
            {
              Console.WriteLine($"    {tag.Key}: {tag.Value}");
              continue;
            }

            Console.WriteLine($"    {tag.Key}: [{string.Join(", ", array.Cast<object>())}]");
          }
        }

        if (activity.Events.Any())
        {
          Console.WriteLine("Activity.Events:");
          foreach (var activityEvent in activity.Events)
          {
            Console.WriteLine($"    {activityEvent.Name} [{activityEvent.Timestamp}]");
            foreach (var attribute in activityEvent.Tags)
            {
              Console.WriteLine($"        {attribute.Key}: {attribute.Value}");
            }
          }
        }

        var resource = this.ParentProvider.GetResource();
        if (resource != Resource.Empty)
        {
          Console.WriteLine("Resource associated with Activity:");
          foreach (var resourceAttribute in resource.Attributes)
          {
            Console.WriteLine($"    {resourceAttribute.Key}: {resourceAttribute.Value}");
          }
        }

        Console.WriteLine(string.Empty);
      }

      return ExportResult.Success;
    }
  }
}