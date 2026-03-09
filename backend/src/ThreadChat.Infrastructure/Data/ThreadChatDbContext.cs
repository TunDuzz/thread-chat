using Microsoft.EntityFrameworkCore;
using ThreadChat.Domain.Entities;

namespace ThreadChat.Infrastructure.Data;

public class ThreadChatDbContext : DbContext
{
    public ThreadChatDbContext(DbContextOptions<ThreadChatDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Workspace> Workspaces => Set<Workspace>();
    public DbSet<WorkspaceMember> WorkspaceMembers => Set<WorkspaceMember>();
    public DbSet<Channel> Channels => Set<Channel>();
    public DbSet<ChannelMember> ChannelMembers => Set<ChannelMember>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Call> Calls => Set<Call>();
    public DbSet<CallParticipant> CallParticipants => Set<CallParticipant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(500);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);

            entity.HasMany(e => e.WorkspaceMembers)
                .WithOne(wm => wm.User)
                .HasForeignKey(wm => wm.UserId);

            entity.HasMany(e => e.ChannelMembers)
                .WithOne(cm => cm.User)
                .HasForeignKey(cm => cm.UserId);

            entity.HasMany(e => e.MessagesSent)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId);

            entity.HasMany(e => e.CallsHosted)
                .WithOne(c => c.Host)
                .HasForeignKey(c => c.HostId);

            entity.HasMany(e => e.CallParticipations)
                .WithOne(cp => cp.User)
                .HasForeignKey(cp => cp.UserId);
        });

        modelBuilder.Entity<Workspace>(entity =>
        {
            entity.ToTable("workspaces");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.AvatarUrl).HasMaxLength(500);

            entity.HasMany(e => e.Members)
                .WithOne(wm => wm.Workspace)
                .HasForeignKey(wm => wm.WorkspaceId);

            entity.HasMany(e => e.Channels)
                .WithOne(c => c.Workspace)
                .HasForeignKey(c => c.WorkspaceId);
        });

        modelBuilder.Entity<WorkspaceMember>(entity =>
        {
            entity.ToTable("workspace_members");
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => new { e.WorkspaceId, e.UserId }).IsUnique();
        });

        modelBuilder.Entity<Channel>(entity =>
        {
            entity.ToTable("channels");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);

            entity.HasMany(e => e.Members)
                .WithOne(cm => cm.Channel)
                .HasForeignKey(cm => cm.ChannelId);

            entity.HasMany(e => e.Messages)
                .WithOne(m => m.Channel)
                .HasForeignKey(m => m.ChannelId);

            entity.HasMany(e => e.Calls)
                .WithOne(c => c.Channel)
                .HasForeignKey(c => c.ChannelId);
        });

        modelBuilder.Entity<ChannelMember>(entity =>
        {
            entity.ToTable("channel_members");
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => new { e.ChannelId, e.UserId }).IsUnique();
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("messages");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Content).HasMaxLength(4000);
            entity.Property(e => e.FileUrl).HasMaxLength(500);

            entity.HasOne(m => m.ReplyTo)
                .WithMany(m => m.Replies)
                .HasForeignKey(m => m.ReplyToId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Call>(entity =>
        {
            entity.ToTable("calls");
            entity.HasKey(e => e.Id);

            entity.HasMany(e => e.Participants)
                .WithOne(cp => cp.Call)
                .HasForeignKey(cp => cp.CallId);
        });

        modelBuilder.Entity<CallParticipant>(entity =>
        {
            entity.ToTable("call_participants");
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => new { e.CallId, e.UserId }).IsUnique();
        });
    }
}

