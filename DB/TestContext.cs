using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTestConsoleApp.DB
{
    public class TestContext : DbContext
    {
        public TestContext()
            : base("DBConnection")
        { }

        public DbSet<NatListTrain> Trains { get; set; }
        public DbSet<Vagon> Vagons { get; set; }
    }
}
