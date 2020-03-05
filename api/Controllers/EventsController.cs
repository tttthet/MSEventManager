using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cors;
using Api.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventManagerContext _context;

        public EventsController(EventManagerContext context)
        {
            _context = context;
        }

        // GET: api/events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events
		.Select(x => new Event
		{
		    Id = x.Id,
		    Title = x.Title,
		    CoordinatorId = x.CoordinatorId,
		    DateTime = x.DateTime
		}).ToListAsync();
        }

        // GET: api/events
        [HttpGet("coordinator")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByCoordinator()
        {
	    /* get user from identity provider */
	    var id = 0;
	    
            return await _context.Events
		.Where(x => x.CoordinatorId == id)
		.Select(x => new Event
		{
		    Id = x.Id,
		    Title = x.Title,
		    CoordinatorId = x.CoordinatorId,
		    DateTime = x.DateTime
		}).ToListAsync();
        }

	// get events & attendees
        [HttpGet("attendees")]
        //public async Task<ActionResult<List<EventVM>>> GetEventsWithAttendees()
        public List<EventVM> GetEventsWithAttendees()
        {
	    /*
	    DataLoadOptions ds = new DataLoadOptions();
	    ds.LoadWith<Invitation>(i => i.EventId);
	    //ds.LoadWith<Order>(o => o.OrderDetails);
	    db.LoadOptions = ds;

	    var query = from e in _context.Events select e;
	    */ 
	    return (from e in _context.Events
		    from i in _context.Set<Invitation>().Where(i => i.EventId == e.Id).GroupBy(i => i.EventId)
//		join i in _context.Invitations on e.Id equals i.EventId into x
//		    group i by i.EventId into x//).ToList();
		    select new EventVM
		    {
			Id = e.Id,
			Title = e.Title,
			CoordinatorId = e.CoordinatorId,
			DateTime = e.DateTime,
			//AttendeeId = i.AttendeeId
			Invitations = i
		    }).ToList();

            //return await _context.TodoItems.ToListAsync();
            /*return _context.Events
		.GroupJoin(_context.Invitations,
			   e => e.Id,
			   i => i.EventId,
			   (e, i) => new EventVM
			   {
			       Id = e.Id,
			       Title = e.Title,
			       CoordinatorId = e.CoordinatorId,
			       DateTime = e.DateTime,
			       //AttendeeId = i.AttendeeId
			       Invitations = i
			   })
		.Select(x => x)
		.ToList();*/
		//.GroupBy(x => x.Id)
		/*.SelectMany(x => new
			//(e, i) => new EventVM
		      {
			  Id = x.Id,
			  Title = x.Title,
			  CoordinatorId = e.CoordinatorId,
			  DateTime = e.DateTime,
			  Users = string.join(',', x.Select(ii => ii.AttendeeId).ToList())*/
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        public async Task<ActionResult<List<Event>>> GetEvent(long id)
        {
            //var todoItem = await _context.TodoItems.FindAsync(id);
            var _event = await _context.Events.ToListAsync();
	    foreach (var e in _event) {
		Console.WriteLine($"event: {e}");
	    }		
		
            if (_event == null)
            {
                return NotFound();
            }

            return _event;
        }

        // PUT: api/events/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(long id, EventVM vm)
        {
            if (id != vm.Id)
            {
                return BadRequest();
            }

	    var item = await _context.Events.FindAsync();
	    if (item == null) {
		return NotFound();
	    }

	    item.Title = vm.Title;
	    item.DateTime = vm.DateTime;

            try
            {
                await _context.SaveChangesAsync();
            }
	    catch (DbUpdateConcurrencyException) when (!EventExists(id))
            {
		return NotFound();
            }

            return NoContent();
        }

        // POST: api/events
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(EventVM vm)
        {
	    Console.WriteLine(JsonSerializer.Serialize(vm));
	    var _event = new Event
	    {
		Title = vm.Title,
		CoordinatorId = vm.CoordinatorId,
		DateTime = vm.DateTime
	    };
	    
            _context.Events.Add(_event);

	    // add the invitees
	    /*_context.Invitations.AddRange(
		vm.Users.Select(userId => new Invitation
		{
		    EventId = _event.Id,
		    AttendeeId = userId,
		    State = UserType.INVITED
		})
	    );
	    */
            await _context.SaveChangesAsync();
	    
            return CreatedAtAction(
		nameof(GetEvent),
		new { id = vm.Id },
		_event
	   );
        }

        // DELETE: api/events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(long id)
        {
            var _event = await _context.Events.FindAsync(id);
            if (_event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(_event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(long id) => _context.Events.Any(e => e.Id == id);

    }
}
