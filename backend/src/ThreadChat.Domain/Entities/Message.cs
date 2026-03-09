using System;
using System.Collections.Generic;

namespace ThreadChat.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }

        public Guid ChannelId { get; set; }

        public Guid SenderId { get; set; }

        public MessageType MessageType { get; set; }

        public string? Content { get; set; }

        public string? FileUrl { get; set; }

        public Guid? ReplyToId { get; set; }

        public DateTimeOffset SentAt { get; set; }

        public Channel Channel { get; set; } = null!;

        public User Sender { get; set; } = null!;

        public Message? ReplyTo { get; set; }

        public ICollection<Message> Replies { get; set; } = new List<Message>();
    }
}

