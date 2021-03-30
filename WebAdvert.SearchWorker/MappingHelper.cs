using System;
using AdvertApi.Models.Messages;

namespace WebAdvert.SearchWorker
{
    public static class MappingHelper
    {
        public static AdvertType Map(AdvertConfirmedMessage message)
        {
            AdvertType doc = new AdvertType()
            {
                Id = message.Id,
                Title = message.Title,
                CreationDateTime = DateTime.UtcNow
            };
            return doc;
        }
    }
}