using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreadChat.Infrastructure.Data;

namespace ThreadChat.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly ThreadChatDbContext _dbContext;

    public HealthController(ThreadChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var canConnect = await _dbContext.Database.CanConnectAsync(cancellationToken);
        return Ok(new
        {
            status = "ok",
            db = canConnect ? "connected" : "unreachable",
            timestamp = DateTimeOffset.UtcNow
        });
    }
}

