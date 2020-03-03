using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
	private readonly EventManagerContext _context;

	public UsersController(EventManagerContext context)
	{
	    _context = context;
	}

	// GET all users that are not coordinators
	[HttpGet]
	public async Task<ActionResult<IEnumerable<User>>> GetFacultyUsers()
	{
	    return await _context.Users
		.Where(x => x.Type == UserType.FACULTY)
		.Select(x => new User
		{
		    Id = x.Id,
		    Name = x.Name,
		    Type = x.Type
		})
		.ToListAsync();
	}

    }
}
