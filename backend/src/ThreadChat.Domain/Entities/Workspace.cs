using System;
using System.Collections.Generic;

namespace ThreadChat.Domain.Entities
{
    public class Workspace
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? AvatarUrl { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public ICollection<WorkspaceMember> Members { get; set; } = new List<WorkspaceMember>();

        public ICollection<Channel> Channels { get; set; } = new List<Channel>();
    }
}

