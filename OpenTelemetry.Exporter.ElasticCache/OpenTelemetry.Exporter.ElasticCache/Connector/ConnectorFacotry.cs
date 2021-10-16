using OpenTelemetry.Exporter.ElasticCache.Options;
using System;
using System.Collections.Concurrent;

namespace OpenTelemetry.Exporter.ElasticCache.Connector
{
  internal static class ConnectorFacotry
  {
    internal static ConcurrentDictionary<Type, ElasticsearchConnector> ConnectorDictionary { get; set; }

    internal static ElasticsearchConnector CreateConnectorInstance(ElasticsearchExporterHttpOptions httpOptions)
    {
      if (ConnectorDictionary== null)
      {
        ConnectorDictionary = new ConcurrentDictionary<Type, ElasticsearchConnector>();
      }

      return ConnectorDictionary.GetOrAdd(httpOptions.GetType(), new HttpConnector(httpOptions));
    }
  }
}