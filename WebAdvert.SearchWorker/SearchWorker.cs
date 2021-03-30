using System;
using System.Threading.Tasks;
using AdvertApi.Models.Messages;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Nest;
using Newtonsoft.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace WebAdvert.SearchWorker
{
    public class SearchWorker
    {
        public SearchWorker() : this(ElasticSearchHelper.GetInstance(ConfigurationHelper.Instance))
        {
        }

        private readonly IElasticClient _elasticClient;

        public SearchWorker(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task Function(SNSEvent snsEvent, ILambdaContext context)
        {
            foreach (var record in snsEvent.Records)
            {
                context.Logger.LogLine(record.Sns.Message);

                AdvertConfirmedMessage message = JsonConvert.DeserializeObject<AdvertConfirmedMessage>(record.Sns.Message);
                AdvertType advertDocument = MappingHelper.Map(message);
                await _elasticClient.IndexDocumentAsync(advertDocument);
            }
        }
    }
}