using Kombi.Dashboard.Model;
using Kombi.Dashboard.Repository;

namespace Kombi.Dashboard.Services
{
    public class SaudeService : ISaudeService
    {
        private readonly ISaudeRepository _saudeRepository;

        public SaudeService(ISaudeRepository saudeRepository)
        {
            _saudeRepository = saudeRepository;
        }

        public async Task<CertificatesCountbyMonth?> CalculateCertificatesForCurrentMonth(string month)
        {
            var certificateCountForMonth = await _saudeRepository.GetCertificatesCountForCurrentMonth(month);
            return certificateCountForMonth;
        }

        public async Task<IEnumerable<CertificatesDaysAggregatedModel>> GetAggregatedCertificatesDays(string? month = null)
        {
            return await _saudeRepository.GetAggregatedCertificatesDays(month);
        }


        public async Task<ZenklubMetricAverageModel> GetZenklubAverage()
        {
            return await _saudeRepository.CalculateZenklubAverage();
        }

        public async Task<IEnumerable<MonthlyAverageDaysOffModel>> CalculateMonthlyAverageDaysOff()
        {
            return await _saudeRepository.GetAvgDays();
        }

        public async Task<IEnumerable<MonthlyCertificatesLocationModel>> CalculateAverageDaysOffByLocation()
        {
            return await _saudeRepository.GetCertificatesDaysByLocation();
        }

        public async Task<IEnumerable<TopDiseasesModel>> GetTopDiseases()
        {
            return await _saudeRepository.GetTopDiseases();
        }

        public async Task<IEnumerable<TopDiseasesCauseModel>> GetTopDiseasesCause()
        {
            return await _saudeRepository.GetTopDiseasesCause();
        }

        public async Task<IEnumerable<CidsRoleModel>> GetTopAffectedRolesAsync()
        {
            return await _saudeRepository.GetCidsByRole();
        }

        public async Task<IEnumerable<CidsByDirectoryModel>> GetAtestadosByDirectorateAsync()
        {
            return await _saudeRepository.GetCidsByDirectory();
        }

        public async Task<IEnumerable<ZenklubEmployeeSessionModel>> GetZenklubSessionsPerEmployeeAsync()
        {
            return await _saudeRepository.GetZenklubEmployeeSessions();
        }

        public async Task<Dictionary<string, Dictionary<string, int>>> GetCidsTrendsByMonth()
        {
            return await _saudeRepository.GetCidsTrendsByMonth();
        }

        public async Task<IEnumerable<SessionCertificatesModel>> CalculateSessionCertificates()
        {
            return await _saudeRepository.GetSessionCertificateModel();
        }

    }
}
