using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTel.Exporter.Elasticsearch.Models
{
  public sealed class LogMessage
  {
    public DateTime Timestamp { get; set; }
    public ActivityTraceId TraceId { get; set; }
    public ActivitySpanId SpanId { get; set; }
    public ActivityTraceFlags TraceFlags { get; set; }
    public string TraceState { get; set; }
    public string CategoryName { get; set; }
    public LogLevel LogLevel { get; set; }
    public EventId EventId { get; set; }
    public string FormattedMessage { get; set; }
    public Exception Exception { get; set; }
    public string Message { get; set; }
  }
}