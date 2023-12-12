using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using testReportApp.Model;

namespace testReportApp.Data
{
    public class DataContext : DbContext
    {
        public DbSet<EmpData> EmpDatas { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
