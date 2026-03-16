using System;
using System.Collections.Generic;

namespace ThreadChat.Domain.Entities
{
    public class DirectMessageConversation
    {
        public Guid Id { get; set; }

        public Guid User1Id { get; set; }

        public Guid User2Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public User User1 { get; set; } = null!;

        public User User2 { get; set; } = null!;

        public ICollection<DirectMessage> Messages { get; set; } = new List<DirectMessage>();
    }
}
