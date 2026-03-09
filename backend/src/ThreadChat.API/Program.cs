using Microsoft.EntityFrameworkCore;
using ThreadChat.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ThreadChatDatabase")
                        ?? throw new InvalidOperationException("Connection string 'ThreadChatDatabase' is not configured.");

builder.Services.AddDbContext<ThreadChatDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
