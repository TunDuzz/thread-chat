using System;

namespace ThreadChat.Domain.Entities
{
    public enum SystemRole
    {
        StandardUser = 0,
        SystemAdmin = 1
    }

    public enum GroupRole
    {
        Member = 0,
        Deputy = 1,
        Owner = 2
    }

    public enum ChannelType
    {
        Text = 0,
        Video = 1
    }

    public enum MessageType
    {
        Text = 0,
        Image = 1,
        File = 2,
        System = 3
    }

    public enum CallType
    {
        Voice = 0,
        Video = 1
    }

    public enum CallStatus
    {
        Ongoing = 0,
        Ended = 1,
        Missed = 2
    }
}

