using System;

namespace ThreadChat.Domain.Entities
{
    public class DirectMessage
    {
        public Guid Id { get; set; }

        public Guid ConversationId { get; set; }

        public Guid SenderId { get; set; }

        public string Content { get; set; } = null!;

        public string? FileUrl { get; set; }

        public DateTimeOffset SentAt { get; set; }

        public DirectMessageConversation Conversation { get; set; } = null!;

        public User Sender { get; set; } = null!;
    }
}
