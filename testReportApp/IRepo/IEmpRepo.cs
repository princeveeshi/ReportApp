using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testReportApp.Model;

namespace testReportApp.IRepo
{
    public interface IEmpRepo
    {
        Task<IEnumerable<EmpData>> GetDatas();
    }
}
