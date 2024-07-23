using Kombi.Dashboard.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kombi.Dashboard.Services
{
    public interface ICidsService
    {
        public string GetModel();
        Task<IEnumerable<CidsModel>> GetCids();
       /* Task<CidsModel> GetCidsById(int id);*/

        Task<int> MethodToTest(int parameter);
        Task<int> MethodToTest(CidsModel cidsModel);
    }
}
