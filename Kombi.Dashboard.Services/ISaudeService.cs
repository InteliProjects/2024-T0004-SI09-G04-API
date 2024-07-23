using System.Collections.Generic;
using System.Threading.Tasks;
using Kombi.Dashboard.Model;

namespace Kombi.Dashboard.Services
{
    public interface ISaudeService
    {
        Task<CertificatesCountbyMonth?> CalculateCertificatesForCurrentMonth(string month);
        Task<IEnumerable<CertificatesDaysAggregatedModel>> GetAggregatedCertificatesDays(string? month = null);
        Task<ZenklubMetricAverageModel> GetZenklubAverage();
        Task<IEnumerable<MonthlyAverageDaysOffModel>> CalculateMonthlyAverageDaysOff();
        Task<IEnumerable<MonthlyCertificatesLocationModel>> CalculateAverageDaysOffByLocation();
        Task<IEnumerable<TopDiseasesModel>> GetTopDiseases();
        Task<IEnumerable<TopDiseasesCauseModel>> GetTopDiseasesCause();
        Task<IEnumerable<CidsRoleModel>> GetTopAffectedRolesAsync();
        Task<IEnumerable<CidsByDirectoryModel>> GetAtestadosByDirectorateAsync();
        Task<IEnumerable<ZenklubEmployeeSessionModel>> GetZenklubSessionsPerEmployeeAsync();
        Task<Dictionary<string, Dictionary<string, int>>> GetCidsTrendsByMonth();
        Task<IEnumerable<SessionCertificatesModel>> CalculateSessionCertificates();
    }

}
