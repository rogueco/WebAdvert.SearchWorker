using System;
using Microsoft.Extensions.Configuration;
using Nest;

namespace WebAdvert.SearchWorker
{
    public static class ElasticSearchHelper
    {
        private static IElasticClient _client;

        public static IElasticClient GetInstance(IConfiguration configuration)
        {
            if (_client == null)
            {
                string url = configuration.GetSection("ES").GetValue<string>("url");
                ConnectionSettings settings = new ConnectionSettings(new Uri(url)).DefaultIndex("adverts")
                    .DefaultMappingFor<AdvertType>(m => m.IdProperty(x => x.Id));
                _client = new ElasticClient(settings);
            }

            return _client;
        }
    }
}