using System;

namespace ThreadChat.Domain.Entities
{
    public class ChannelMember
    {
        public Guid Id { get; set; }

        public Guid ChannelId { get; set; }

        public Guid UserId { get; set; }

        public DateTimeOffset AddedAt { get; set; }

        public Channel Channel { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}

