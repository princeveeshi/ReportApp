using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using testReportApp.Data;
using testReportApp.IRepo;
using testReportApp.Model;

namespace testReportApp.Repository
{
    public class EmpRepo : IEmpRepo
    {
        private readonly DataContext _context;

        public EmpRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmpData>> GetDatas()
        {
            return await _context.EmpDatas.ToListAsync();
        }
    }
}
