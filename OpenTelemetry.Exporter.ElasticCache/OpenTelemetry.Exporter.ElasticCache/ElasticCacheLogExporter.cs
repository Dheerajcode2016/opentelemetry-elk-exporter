using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using System;
using System.Collections.Generic;

namespace OpenTelemetry.Exporter
{
  public class ElasticCacheLogExporter : BaseExporter<LogRecord>
  {
    private const int RightPaddingLength = 30;

    public ElasticCacheLogExporter()
    {
    }

    public override ExportResult Export(in Batch<LogRecord> batch)
    {
      foreach (var logRecord in batch)
      {
        Console.WriteLine($"{"LogRecord.TraceId:".PadRight(RightPaddingLength)}{logRecord.TraceId}");
        Console.WriteLine($"{"LogRecord.SpanId:".PadRight(RightPaddingLength)}{logRecord.SpanId}");
        Console.WriteLine($"{"LogRecord.Timestamp:".PadRight(RightPaddingLength)}{logRecord.Timestamp:yyyy-MM-ddTHH:mm:ss.fffffffZ}");
        Console.WriteLine($"{"LogRecord.EventId:".PadRight(RightPaddingLength)}{logRecord.EventId}");
        Console.WriteLine($"{"LogRecord.CategoryName:".PadRight(RightPaddingLength)}{logRecord.CategoryName}");
        Console.WriteLine($"{"LogRecord.LogLevel:".PadRight(RightPaddingLength)}{logRecord.LogLevel}");
        Console.WriteLine($"{"LogRecord.TraceFlags:".PadRight(RightPaddingLength)}{logRecord.TraceFlags}");
        if (logRecord.FormattedMessage != null)
        {
          Console.WriteLine($"{"LogRecord.FormattedMessage:".PadRight(RightPaddingLength)}{logRecord.FormattedMessage}");
        }

        if (logRecord.State != null)
        {
          Console.WriteLine($"{"LogRecord.State:".PadRight(RightPaddingLength)}{logRecord.State}");
        }
        else if (logRecord.StateValues != null)
        {
          Console.WriteLine("LogRecord.StateValues (Key:Value):");
          for (int i = 0; i < logRecord.StateValues.Count; i++)
          {
            Console.WriteLine($"{logRecord.StateValues[i].Key.PadRight(RightPaddingLength)}{logRecord.StateValues[i].Value}");
          }
        }

        if (logRecord.Exception is { })
        {
          Console.WriteLine($"{"LogRecord.Exception:".PadRight(RightPaddingLength)}{logRecord.Exception?.Message}");
        }

        int scopeDepth = -1;

        logRecord.ForEachScope(ProcessScope, this);

        void ProcessScope(LogRecordScope scope, ElasticCacheLogExporter exporter)
        {
          if (++scopeDepth == 0)
          {
            Console.WriteLine("LogRecord.ScopeValues (Key:Value):");
          }

          foreach (KeyValuePair<string, object> scopeItem in scope)
          {
            Console.WriteLine($"[Scope.{scopeDepth}]:{scopeItem.Key.PadRight(RightPaddingLength)}{scopeItem.Value}");
          }
        }

        var resource = this.ParentProvider.GetResource();
        if (resource != Resource.Empty)
        {
          Console.WriteLine("Resource associated with LogRecord:");
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