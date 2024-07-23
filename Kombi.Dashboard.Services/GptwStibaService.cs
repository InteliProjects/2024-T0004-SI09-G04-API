using Kombi.Dashboard.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kombi.Dashboard.Service
{
    public class GptwVsStibaService : IGptwVsStibaService
    {
        private readonly IGptwVsStibaRepository _gptwVsStibaRepository;

        public GptwVsStibaService(IGptwVsStibaRepository gptwVsStibaRepository)
        {
            _gptwVsStibaRepository = gptwVsStibaRepository;
        }

        public async Task<double> GetStibaRespondentPercentageAsync()
        {
            return await _gptwVsStibaRepository.GetStibaRespondentPercentageAsync();
        }

        public async Task<IEnumerable<GptwEngage>> GetAllGptwEngageDataAsync()
        {
            return await _gptwVsStibaRepository.GetAllGptwEngageDataAsync();
        }

        public async Task<StibaNota> GetNotaGeralStibaAsync()
        {
            return await _gptwVsStibaRepository.GetNotasStibaAsync();
        }
    }
}
