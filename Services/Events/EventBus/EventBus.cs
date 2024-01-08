using MassTransit;
using MassTransit.Transports;
using Microsoft.Extensions.Logging;

namespace Services.Events.EventBus
{
    public sealed class EventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<EventBus> _logger;

        public EventBus(IPublishEndpoint publishEndpoint, ILogger<EventBus> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task PublishAsync<T>(T message) where T : class
        {
            await _publishEndpoint.Publish(message);
        }
    }
}
