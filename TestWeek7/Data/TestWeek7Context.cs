using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestWeek7.Models;

namespace TestWeek7.Data
{
    public class TestWeek7Context : DbContext
    {
        public TestWeek7Context (DbContextOptions<TestWeek7Context> options)
            : base(options)
        {
        }

        public DbSet<TestWeek7.Models.Todos> Todos { get; set; } = default!;
    }
}
