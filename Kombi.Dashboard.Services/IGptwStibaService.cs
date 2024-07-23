using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kombi.Dashboard.Repository;

namespace Kombi.Dashboard.Service
{
    public interface IGptwVsStibaService
    {
        Task<double> GetStibaRespondentPercentageAsync();
        Task<IEnumerable<GptwEngage>> GetAllGptwEngageDataAsync();
        Task<StibaNota> GetNotaGeralStibaAsync();

    }
}