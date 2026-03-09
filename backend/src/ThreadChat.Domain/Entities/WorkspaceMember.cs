using System;

namespace ThreadChat.Domain.Entities
{
    public class WorkspaceMember
    {
        public Guid Id { get; set; }

        public Guid WorkspaceId { get; set; }

        public Guid UserId { get; set; }

        public GroupRole GroupRole { get; set; }

        public DateTimeOffset JoinedAt { get; set; }

        public Workspace Workspace { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}

