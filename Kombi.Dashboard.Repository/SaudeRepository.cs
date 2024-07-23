using System.Collections.Generic;
using System.Threading.Tasks;
using Kombi.Dashboard.Model;
using Npgsql;
using Dapper;

namespace Kombi.Dashboard.Repository
{
    public class SaudeRepository : ISaudeRepository
    {
        private readonly string _dbConfig;

        public SaudeRepository(string dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<CertificatesCountbyMonth?> GetCertificatesCountForCurrentMonth(string month)
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var result = await conn.QuerySingleOrDefaultAsync<CertificatesCountbyMonth>(@"
            SELECT
                mes as Month,
                SUM(atestados) as Atestados
            FROM
                cid_f_2023_geral
            WHERE
                LOWER(mes) = LOWER(@Month)
            GROUP BY
                mes",
                    new { Month = month }
                );

                if (result != null)
                {
                    result.Difference = 0;
                }

                return result; 
            }
        }

        public async Task<IEnumerable<CertificatesDaysAggregatedModel>> GetAggregatedCertificatesDays(string? month = null)
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();

                var query = @"
                    SELECT
                        mes AS Month,
                        SUM(atestados) AS TotalAtestados,
                        AVG(CAST(REPLACE(dias, ',', '.') AS FLOAT)) AS MediaDias
                    FROM
                        cid_f_2023_geral
                    WHERE
                        (@Month IS NULL OR mes ILIKE '%' || @Month || '%')
                    GROUP BY
                        mes
                    ORDER BY
                        mes;
                ";
                var parameters = new { Month = month?.ToLower() };

                return await conn.QueryAsync<CertificatesDaysAggregatedModel>(query, parameters);
            }
        }


        public async Task<ZenklubMetricAverageModel> CalculateZenklubAverage()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();

                var query = @"
            SELECT
                AVG(CAST(total_sessions AS DECIMAL)) AS AverageSessionsPerPerson
            FROM (
                SELECT
                    n_pessoal,
                    COUNT(*) AS total_sessions
                FROM
                    zenklub
                GROUP BY
                    n_pessoal
            ) AS subquery";

                var averageSessionsPerPerson = await conn.QuerySingleOrDefaultAsync<decimal>(query);

                var previousYearAverage = 0;

                double difference = previousYearAverage != 0 ?
                    ((double)(averageSessionsPerPerson - previousYearAverage) / previousYearAverage) * 100 : 0;

                return new ZenklubMetricAverageModel
                {
                    AverageSessionsPerPerson = (double)averageSessionsPerPerson,
                    DifferenceFromLastYear = difference
                };
            }
        }

        public async Task<IEnumerable<MonthlyAverageDaysOffModel>> GetAvgDays()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
                    SELECT
                        mes as Month,
                        AVG(CAST(REPLACE(TRIM(dias), ',', '.') AS FLOAT)) as AverageDaysOff
                    FROM
                        cid_f_2023_geral
                    GROUP BY
                        mes
                    ORDER BY
                        mes
                ";
                var results = await conn.QueryAsync<MonthlyAverageDaysOffModel>(query);
                return results;
            }
        }

        public async Task<IEnumerable<MonthlyCertificatesLocationModel>> GetCertificatesDaysByLocation()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
                    SELECT
                        mes as Month,
                        unidade AS Location,
                        AVG(CAST(REPLACE(TRIM(dias), ',', '.') AS FLOAT)) AS AverageDaysOff
                    FROM
                        cid_f_2023_geral
                    GROUP BY
                        mes, unidade
                    ORDER BY
                        unidade, mes
                ";
                var result = await conn.QueryAsync<MonthlyCertificatesLocationModel>(query);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<TopDiseasesModel>> GetTopDiseases()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
            SELECT
                descricao_resumida AS Disease,
                COUNT(*) AS Quantity
            FROM
                cid_f_2023_geral
            GROUP BY
                descricao_resumida
            ORDER BY
                Quantity DESC
            LIMIT 5";

                var result = await conn.QueryAsync<TopDiseasesModel>(query);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<TopDiseasesCauseModel>> GetTopDiseasesCause()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
            SELECT
                descricao_resumida AS Disease,
                diagnostico_atestado AS Cause,
                COUNT(*) AS Quantity
            FROM
                cid_f_2023_geral
            GROUP BY
                descricao_resumida, diagnostico_atestado
            ORDER BY
                Quantity DESC
            LIMIT 5"; 

                var results = await conn.QueryAsync<TopDiseasesCauseModel>(query);
                return results.ToList();
            }
        }

        public async Task<IEnumerable<CidsRoleModel>> GetCidsByRole()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
                    SELECT
                        e.cargo AS Role,
                        SUM(c1.atestados) AS Atestados2021,
                        SUM(c2.atestados) AS Atestados2022,
                        SUM(c3.atestados) AS Atestados2023
                    FROM
                        empregados e
                    LEFT JOIN cid_f_2021 c1 ON e.n_pessoal = c1.n_pessoal
                    LEFT JOIN cid_f_2022 c2 ON e.n_pessoal = c2.n_pessoal
                    LEFT JOIN cid_f_2023 c3 ON e.n_pessoal = c3.n_pessoal
                    GROUP BY e.cargo
                    ORDER BY Atestados2021 DESC, Atestados2022 DESC, Atestados2023 DESC;
                ";  
                var results = await conn.QueryAsync<CidsRoleModel>(query);
                return results.ToList();
            }
        }
        public async Task<IEnumerable<CidsByDirectoryModel>> GetCidsByDirectory()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
            SELECT
                diretoria AS Directorate,
                SUM(atestados) AS Atestados
            FROM
                cid_f_2023_geral
            GROUP BY
                diretoria
            ORDER BY
                SUM(atestados) DESC";

                var results = await conn.QueryAsync<CidsByDirectoryModel>(query);
                return results.ToList();
            }
        }


        public async Task<IEnumerable<ZenklubEmployeeSessionModel>> GetZenklubEmployeeSessions()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
            SELECT
                mes AS Month,
                departamento AS Location,
                COUNT(n_pessoal) AS EmployeeCount,
                SUM(total_sessoes) AS TotalSessions
            FROM
                zenklub
            GROUP BY
                mes, departamento
            ORDER BY
                mes, departamento";

                var results = await conn.QueryAsync<ZenklubEmployeeSessionModel>(query); 

                return results.Select(x =>
                {
                    x.SessionsPerEmployee = x.EmployeeCount > 0 ? (double)x.TotalSessions / x.EmployeeCount : 0;
                    return x; 
                }).ToList();
            }
        }

        public async Task<Dictionary<string, Dictionary<string, int>>> GetCidsTrendsByMonth()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();

                var query = @"
                    SELECT
                        mes AS Month,
                        causa_raiz AS RootCause,
                        COUNT(*) AS Quantity
                    FROM
                        cid_f_2023_geral
                    GROUP BY
                        mes, causa_raiz
                    ORDER BY
                        mes ASC;
                ";

                var results = await conn.QueryAsync<CidTrend>(query); // Changed from dynamic to CidTrend

                var trends = new Dictionary<string, Dictionary<string, int>>();

                foreach (var result in results)
                {
                    var month = result.Month ?? "Unknown";
                    var rootCause = result.RootCause ?? "Unknown";

                    if (!trends.ContainsKey(month))
                    {
                        trends[month] = new Dictionary<string, int>();
                    }

                    if (!trends[month].ContainsKey(rootCause))
                    {
                        trends[month][rootCause] = 0;
                    }

                    trends[month][rootCause] += result.Quantity;
                }

                return trends;
            }
        }



        public async Task<IEnumerable<SessionCertificatesModel>> GetSessionCertificateModel()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
                SELECT
                    SUM(c.atestados) AS Atestados,
                    SUM(z.total_sessoes) AS Sessions
                FROM
                    cid_f_2023 c
                LEFT JOIN
                    zenklub z ON c.n_pessoal = z.n_pessoal
                  GROUP BY
                    z.total_sessoes
                ";
                return await conn.QueryAsync<SessionCertificatesModel>(query);
            }
        }
    }
}
