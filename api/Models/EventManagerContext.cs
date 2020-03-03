using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class EventManagerContext : DbContext
    {
	public EventManagerContext() : base()
	{
	}

	//public DbSet<TodoItem> TodoItems { get; set; }
	public DbSet<Event> Events { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Invitation> Invitations { get; set; }

	// we override the OnModelCreating method here.
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	    modelBuilder.Entity<Invitation>().HasKey(inv=> new {inv.EventId, inv.AttendeeId});
	}

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
	    optionsBuilder.UseSqlite("Data Source=events.db");
        }
    }

    public class Event
    {
	public int Id { get; set; }
	public string Title { get; set; }
	public int CoordinatorId { get; set; }
	public string DateTime { get; set; }
    }

    public class User
    {
	public int Id { get; set; }
	public string Name { get; set; }
	public UserType Type { get; set; }
    }

    public enum UserType
    {
	COORDINATOR,
	INVITED, // remove
	ATTENDED, // remove
	REQUEST,
	FACULTY
    }

    // Invitation
    public class Invitation
    {
	public int EventId { get; set; }
	public int AttendeeId { get; set; }
	public UserType State { get; set; }

	public string Response { get; set; }
    }


    public class EventVM : Event
    {
        public int?[] Users { get; set; }
        public int? UserState { get; set; }
        public IEnumerable<Invitation> Invitations { get; set; }
    }
}
