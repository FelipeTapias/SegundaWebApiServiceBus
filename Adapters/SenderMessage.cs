using Adapters.Interface;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using SegundaWebAPI.Helpers;
using System.Text;
using System.Text.Json;

namespace Adapters
{
    public class SenderMessage: ISenderMessage
    {
        private readonly IOptionsMonitor<ServiBusConfiguration> _options;
        public SenderMessage(IOptionsMonitor<ServiBusConfiguration> options)
        {
            _options = options;
        }
        public async Task CreateMessage(string message)
        {
            ServiceBusClientOptions clientOptions = new()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            ServiceBusClient client = new(_options.CurrentValue.defaultServiceBus, clientOptions);
            ServiceBusSender sender = client.CreateSender(_options.CurrentValue.ColaName);
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();
            string json = JsonSerializer.Serialize(message);
            messageBatch.TryAddMessage(new ServiceBusMessage(Encoding.UTF8.GetBytes(json)));
            try
            {
                await sender.SendMessagesAsync(messageBatch);
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}
