using System;
using System.Collections.Generic;

namespace ThreadChat.Domain.Entities
{
    public class Channel
    {
        public Guid Id { get; set; }

        public Guid WorkspaceId { get; set; }

        public string Name { get; set; } = null!;

        public ChannelType Type { get; set; }

        public bool IsPrivate { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public Workspace Workspace { get; set; } = null!;

        public ICollection<ChannelMember> Members { get; set; } = new List<ChannelMember>();

        public ICollection<Message> Messages { get; set; } = new List<Message>();

        public ICollection<Call> Calls { get; set; } = new List<Call>();
    }
}

