using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kombi.Dashboard.Repository
{

    public class GptwVsStibaRepository : IGptwVsStibaRepository
    {
        private readonly string _dbConfig;

        public GptwVsStibaRepository(string dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<double> GetStibaRespondentPercentageAsync()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();

                var result = await conn.QueryAsync<Stiba2023>(@"
                    SELECT 
                        SUM(Elegíveis) as Elegiveis, 
                        SUM(Respondentes) as Respondentes
                    FROM 
                        stiba_2023
                ");

                if (result.AsList().Count == 0)
                    return 0;

                var data = result.AsList()[0];
                return (data.Respondentes / data.Elegiveis) * 100;
            }
        }

        public async Task<IEnumerable<GptwEngage>> GetAllGptwEngageDataAsync()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<GptwEngage>(@"
                    SELECT 
                        year, 
                        engagement_percent as EngagementPercent
                    FROM 
                        gptw_engage
                    WHERE 
                        year = 2023
                ");
            }
        }

        public async Task<StibaNota> GetNotasStibaAsync()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
                        SELECT 
                            AVG(s3.""Nota Stiba"") as Nota2023, 
                            AVG(s2.""Nota Stiba"") as Nota2022
                        FROM 
                            stiba_2023 s3
                        JOIN 
                            stiba_2022 s2 
                        ON 
                            s2.""Descrição UO"" = s3.""Descrição UO""
                    ";

                var result = await conn.QueryAsync<(double Nota2023, double Nota2022)>(query);

                var notas = result.FirstOrDefault();
                if (notas == default)
                    throw new InvalidOperationException("Não foi possível calcular as notas Stiba.");

                double variacao = (notas.Nota2023 - notas.Nota2022) / notas.Nota2022 * 100;

                return new StibaNota
                {
                    Nota2023 = notas.Nota2023,
                    Nota2022 = notas.Nota2022,
                    VariacaoPercentual = variacao
                };
            }
        }


        public async Task<IEnumerable<GptwCompanyData>> GetGptwDataAsync()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
                    SELECT
                        q.pergunta,
                        q.escala,
                        ct.*
                    FROM 
                        gptw_company_t ct
                    JOIN 
                        gptw_questions q ON CAST(q.""N"" AS text) = ct.""Q""";

                var data = await conn.QueryAsync<dynamic>(query);

                return data.Select(row =>
                {
                    var gptwData = new GptwCompanyData
                    {
                        Pergunta = row.pergunta,
                        Escala = row.escala
                    };

                    for (int i = 1; i <= 6; i++)
                    {
                        gptwData.Valores.Add((double)row[$"Column{i}"]);
                    }

                    return gptwData;
                });
            }
        }

    }
}
