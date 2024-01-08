using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Events.EventModels
{
    public record UserCreatedEvent
    {
        public long Id { get; init; }
        public string Email { get; init; } = string.Empty;
        public string Content { get; init; } = string.Empty;
    }
}
