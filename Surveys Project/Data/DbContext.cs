using Surveys_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Surveys_Project.Data
{
	public class DDBContext : DbContext
	{
		public DDBContext(DbContextOptions<DDBContext> options) : base(options)
		{
		}

		public DbSet<Survey> Surveys { get; set; }
	}
}
