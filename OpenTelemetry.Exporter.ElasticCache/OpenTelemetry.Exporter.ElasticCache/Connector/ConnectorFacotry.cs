using OpenTelemetry.Exporter.ElasticCache.Options;
using System;
using System.Collections.Concurrent;

namespace OpenTelemetry.Exporter.ElasticCache.Connector
{
  internal static class ConnectorFacotry
  {
    internal static ConcurrentDictionary<Type, ElasticCacheConnector> ConnectorDictionary { get; set; }

    internal static ElasticCacheConnector CreateConnectorInstance(ElasticCacheExporterHttpOptions httpOptions)
    {
      if (ConnectorDictionary== null)
      {
        ConnectorDictionary = new ConcurrentDictionary<Type, ElasticCacheConnector>();
          
      }
      return ConnectorDictionary.GetOrAdd(httpOptions.GetType(), new HttpConnector(httpOptions));
    }
  }
}