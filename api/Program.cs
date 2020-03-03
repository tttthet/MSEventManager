using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Api.Models;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

	    using (var db = new EventManagerContext())
            {
                // Create
		/*                Console.WriteLine("Inserting a new user");
                db.Add(new User { Name = "John Smith", Type = UserType.COORDINATOR });
                db.Add(new User { Name = "Bill Sutton", Type = UserType.COORDINATOR });
		db.Add(new User { Name = "Jane Doe", Type = UserType.FACULTY });
		db.Add(new User { Name = "Sally Jones", Type = UserType.FACULTY });
		db.Add(new Event {
		    Title = "Program generated Event 1",
		    CoordinatorId = 0,
		    DateTime = "3/4/2020"
		});
		db.Add(new Event {
		    Title = "Program generated Event 2",
		    CoordinatorId = 0,
		    DateTime = "4/4/2020"
		});
		db.Add(new Event {
		    Title = "Program generated Event 3",
		    CoordinatorId = 0,
		    DateTime = "5/4/2020"
		});
		db.Add(new Invitation {
		    EventId = 1,
		    AttendeeId = 1,
		    State = UserType.ATTENDED
		});
		db.Add(new Invitation {
		    EventId = 1,
		    AttendeeId = 2,
		    State = UserType.ATTENDED
		});
		db.Add(new Invitation {
		    EventId = 1,
		    AttendeeId = 3,
		    State = UserType.ATTENDED
		});
                db.SaveChanges();*/
	    }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
