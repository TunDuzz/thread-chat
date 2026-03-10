using Microsoft.AspNetCore.Mvc;
using ThreadChat.Application.DTOs.Users;
using ThreadChat.Application.Interfaces.Repositories;

namespace ThreadChat.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(CancellationToken cancellationToken)
    {
        var users = await _userRepository.ListAsync(cancellationToken);

        var result = users.Select(u => new UserDto(
            u.Id,
            u.Username,
            u.PhoneNumber,
            u.Email,
            u.FullName,
            u.SystemRole,
            u.CreatedAt));

        return Ok(result);
    }
}

