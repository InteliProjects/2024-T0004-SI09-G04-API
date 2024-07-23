using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kombi.Dashboard.Repository
{
    public interface ICidsRepository
    {
        Task<IEnumerable<CidsModel>> GetCids();
        

    }
}
