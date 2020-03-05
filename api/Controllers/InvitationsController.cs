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
    public class InvitationsController : ControllerBase
    {
	private readonly EventManagerContext _context;

	public InvitationsController(EventManagerContext context)
	{
	    _context = context;
	}

	// all invitations
	[HttpGet]
	public async Task<ActionResult<List<Invitation>>> GetInvitations()
	{
	    return await _context.Invitations
		.Select(x => new Invitation
		{
		    EventId = x.EventId,
		    AttendeeId = x.AttendeeId,
		    Response = x.Response
		})
		.ToListAsync();
	}

	// invitations by faculty / Users
	//[HttpGet("{id}")]
	[HttpGet("user")]
	//public async Task<ActionResult<IEnumerable<Invitation>>> GetInvitationsByCoordinator(long id)
	public async Task<ActionResult<IEnumerable<Invitation>>> GetInvitationsByUser()
	{
	    /* TODO pull the user from identity provider */
	    var id = 1;

	    return await _context.Invitations
		.Where(x => x.AttendeeId == id)
		.Select(x => new Invitation
		{
		    EventId = x.EventId,
		    AttendeeId = x.AttendeeId,
		    Response = x.Response
		})
		.ToListAsync();
	}

	// coordinator creates Invitation
	[HttpPost]
	public async Task<IActionResult> CreateInvitation(Invitation vm)
	{
	    var invitation = new Invitation
	    {
		EventId = vm.EventId,
		AttendeeId = vm.AttendeeId
	    };

	    _context.Invitations.Add(invitation);
	    await _context.SaveChangesAsync();

	    // we have not GET endpoint for individual Invitations so no CreatedAtAction()
	    return NoContent();
	}

	// coordinator updates request w/ response
	[HttpPut("{eventId}/{attendeeId}")]
	public async Task<IActionResult> UpdateInvitation(long eventId, long attendeeId, Invitation vm)
	{
	    if (eventId != vm.EventId || attendeeId != vm.AttendeeId) {
		return BadRequest();
	    }
	    
	    var invitation = await _context.Invitations.FindAsync();
	    invitation.Response = vm.Response;

	    try
	    {
		await _context.SaveChangesAsync();
	    }
	    catch (DbUpdateConcurrencyException) when (DoesNotExist(vm.EventId, vm.AttendeeId))
	    {
		return NotFound();
	    }

	    // we have not GET endpoint for individual Invitations so no CreatedAtAction()
	    return NoContent();
	}

	private bool DoesNotExist(long eventId, long attendeeId) =>
	    !_context.Invitations.Any(x => eventId == x.EventId && attendeeId == x.AttendeeId);
    }
}
