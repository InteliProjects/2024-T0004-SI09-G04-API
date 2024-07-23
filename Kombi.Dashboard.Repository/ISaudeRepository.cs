using System.Collections.Generic;
using System.Threading.Tasks;
using Kombi.Dashboard.Model;

namespace Kombi.Dashboard.Repository
{
    public interface ISaudeRepository
    {
        Task<CertificatesCountbyMonth?> GetCertificatesCountForCurrentMonth(string month);
        Task<IEnumerable<CertificatesDaysAggregatedModel>> GetAggregatedCertificatesDays(string? month);
        Task<ZenklubMetricAverageModel> CalculateZenklubAverage();
        Task<IEnumerable<MonthlyAverageDaysOffModel>> GetAvgDays();
        Task<IEnumerable<MonthlyCertificatesLocationModel>> GetCertificatesDaysByLocation();
        Task<IEnumerable<TopDiseasesModel>> GetTopDiseases();
        Task<IEnumerable<TopDiseasesCauseModel>> GetTopDiseasesCause();
        Task<IEnumerable<CidsRoleModel>> GetCidsByRole();
        Task<IEnumerable<CidsByDirectoryModel>> GetCidsByDirectory();
        Task<IEnumerable<ZenklubEmployeeSessionModel>> GetZenklubEmployeeSessions();
        Task<Dictionary<string, Dictionary<string, int>>> GetCidsTrendsByMonth();
        Task<IEnumerable<SessionCertificatesModel>> GetSessionCertificateModel();

    }
}
