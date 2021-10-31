using OTel.Exporter.Elasticsearch.Options;
using System;
using System.Collections.Concurrent;

namespace OTel.Exporter.Elasticsearch.Connector
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