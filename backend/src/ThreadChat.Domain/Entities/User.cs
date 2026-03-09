using System;
using System.Collections.Generic;

namespace ThreadChat.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public required string Username { get; set; }

        public required string PhoneNumber { get; set; }

        public required string Email { get; set; }

        public string? ImageUrl { get; set; }

        public required string PasswordHash { get; set; }

        public required string FullName { get; set; }

        public required SystemRole SystemRole { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public ICollection<WorkspaceMember> WorkspaceMembers { get; set; } = new List<WorkspaceMember>();

        public ICollection<ChannelMember> ChannelMembers { get; set; } = new List<ChannelMember>();

        public ICollection<Message> MessagesSent { get; set; } = new List<Message>();

        public ICollection<Call> CallsHosted { get; set; } = new List<Call>();

        public ICollection<CallParticipant> CallParticipations { get; set; } = new List<CallParticipant>();
    }
}

