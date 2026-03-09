using System;
using System.Collections.Generic;

namespace ThreadChat.Domain.Entities
{
    public class Call
    {
        public Guid Id { get; set; }

        public Guid ChannelId { get; set; }

        public Guid HostId { get; set; }

        public CallType CallType { get; set; }

        public CallStatus Status { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }

        public Channel Channel { get; set; } = null!;

        public User Host { get; set; } = null!;

        public ICollection<CallParticipant> Participants { get; set; } = new List<CallParticipant>();
    }
}

