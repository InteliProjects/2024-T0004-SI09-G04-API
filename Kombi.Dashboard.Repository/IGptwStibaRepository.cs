using System.Collections.Generic;
using System.Threading.Tasks;
using Kombi.Dashboard.Repository;

namespace Kombi.Dashboard.Repository
{
    public interface IGptwVsStibaRepository
    {
        Task<double> GetStibaRespondentPercentageAsync();
        Task<IEnumerable<GptwEngage>> GetAllGptwEngageDataAsync();
        Task<StibaNota> GetNotasStibaAsync();
    }
}
