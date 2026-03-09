using System;

namespace ThreadChat.Domain.Entities
{
    public class CallParticipant
    {
        public Guid Id { get; set; }

        public Guid CallId { get; set; }

        public Guid UserId { get; set; }

        public DateTimeOffset JoinedAt { get; set; }

        public DateTimeOffset? LeftAt { get; set; }

        public Call Call { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}

