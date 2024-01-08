using MassTransit;
using Microsoft.Extensions.Logging;
using Services.Events.EventModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Events.EventConsumer
{
    public sealed class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly ILogger<UserCreatedEventConsumer> _logger;

        public UserCreatedEventConsumer(ILogger<UserCreatedEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            EmailSender.SendEmail(context.Message.Email, context.Message.Content);
            return Task.CompletedTask;
        }
    }
}
