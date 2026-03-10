namespace ThreadChat.Application.Abstractions;

public sealed record PagedRequest(int Page = 1, int PageSize = 20)
{
    public int Skip => (Page <= 1 ? 0 : (Page - 1) * PageSize);
}

