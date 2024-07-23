using Dapper;
using Npgsql;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kombi.Dashboard.Repository
{
    public class StibaRepository : IStibaRepository
    {
        private readonly string _dbConfig;

        public StibaRepository(string dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<IEnumerable<StibaModel>> GetStibaAnswers()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                Console.WriteLine("cheguei no repository");
                return await conn.QueryAsync<StibaModel>(@"
                   SELECT 
                        descricao_uo as DescricaoUO,
                        elegiveis as Elegiveis,
                        repondentes as Respondentes,
                        perc_part as PecPart,
                        nota_stiba as NotaStiba,
                        q01,
                        q02,
                        q03,
                        q04,
                        q05,
                        q06,
                        q07,
                        q08,
                        q09,
                        q10,
                        q11,
                        q12,
                        q13,
                        q14,
                        q15,
                        q16,
                        q17,
                        q18,
                        q19,
                        q20,
                        q21,
                        q22,
                        q23,
                        q24,
                        (REPLACE(q01, ',', '.')::numeric + REPLACE(q02, ',', '.')::numeric + REPLACE(q03, ',', '.')::numeric + REPLACE(q04, ',', '.')::numeric + REPLACE(q05, ',', '.')::numeric + REPLACE(q06, ',', '.')::numeric + REPLACE(q07, ',', '.')::numeric + REPLACE(q08, ',', '.')::numeric + REPLACE(q09, ',', '.')::numeric + REPLACE(q10, ',', '.')::numeric + REPLACE(q11, ',', '.')::numeric + REPLACE(q12, ',', '.')::numeric + REPLACE(q13, ',', '.')::numeric + REPLACE(q14, ',', '.')::numeric + REPLACE(q15, ',', '.')::numeric + REPLACE(q16, ',', '.')::numeric + REPLACE(q17, ',', '.')::numeric + REPLACE(q18, ',', '.')::numeric + REPLACE(q19, ',', '.')::numeric + REPLACE(q20, ',', '.')::numeric + REPLACE(q21, ',', '.')::numeric + REPLACE(q22, ',', '.')::numeric + REPLACE(q23, ',', '.')::numeric + REPLACE(q24, ',', '.')::numeric) / 24 as MediaPesquisa,
                        (repondentes::numeric / elegiveis::numeric) * 100 as PorcentagemRespondentes,
                        elegiveis as NumeroTotalColaboradores
                    FROM 
                        stiba_2022_t
                    ");
            }
        }
        public async Task<IEnumerable<StibaModel>> GetStibaAnswers23()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                Console.WriteLine("cheguei no repository");
                return await conn.QueryAsync<StibaModel>(@"
                    SELECT 
                        DescricaoUO AS ""Questão"",
                        AVG(NOTA) AS ""Média da Nota""
                    FROM (
                        SELECT
                            descricao_uo AS DescricaoUO,
                            elegiveis AS Elegiveis,
                            respondentes AS Respondentes,
                            perc_part AS PecPart,
                            nota_stiba AS NotaStiba,
                            q1 AS q01,
                            q2 AS q02,
                            q3 AS q03,
                            q4 AS q04,
                            q5 AS q05,
                            q6 AS q06,
                            q7 AS q07,
                            q8 AS q08,
                            q9 AS q09,
                            q10 AS q10,
                            q11 AS q11,
                            q12 AS q12,
                            q13 AS q13,
                            q14 AS q14,
                            q15 AS q15,
                            q16 AS q16,
                            q17 AS q17,
                            q18 AS q18,
                            q19 AS q19,
                            q20 AS q20,
                            q21 AS q21,
                            q22 AS q22,
                            q23 AS q23,
                            q24 AS q24
                        FROM 
                            stiba_2023_t
                    ) AS SourceTable
                    UNPIVOT (
                        NOTA FOR Questao IN (
                            q01, q02, q03, q04, q05, q06, q07, q08, q09, q10, 
                            q11, q12, q13, q14, q15, q16, q17, q18, q19, q20, 
                            q21, q22, q23, q24
                        )
                    ) AS UnpivotedData
                    GROUP BY DescricaoUO

                    ");


            }
        }
    }
}

        /*public async Task<IEnumerable<CultureScore>> GetSortedCultureScoresAsync()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
            SELECT
                COALESCE(s2.Descricao_UO, s3.Descricao_UO) AS Question,
                (AVG(s2.Q01)::NUMERIC(10,2) + AVG(s3.Q01)::NUMERIC(10,2)) / 2 AS Score
            FROM
                stiba_2023 s3
            FULL OUTER JOIN
                stiba_2022 s2 ON s2.Descricao_UO = s3.Descricao_UO
            GROUP BY
                Question
            ORDER BY
                Score ASC"; 
                return await conn.QueryAsync<CultureScore>(query);
            }
        }


        public async Task<IEnumerable<StibaModel>> GetDirectoryScore()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
                    SELECT 
                        Descricao_UO,
                        s2.Nota_Stiba AS Nota_Stiba_2022,
                        s3.Nota_Stiba AS Nota_Stiba_2023
                    FROM
                        stiba_2023 s3
                    LEFT JOIN
                        stiba_2022 s2 ON s2.Descricao_UO = s3.Descricao_UO";
                return await conn.QueryAsync<StibaModel>(query);
            }
        }

        public async Task<IEnumerable<StibaModel>> GetStibaScore()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
                    SELECT 
                        s2.Nota_Stiba AS Nota_Stiba_2022,
                        s3.Nota_Stiba AS Nota_Stiba_2023
                    FROM
                        stiba_2023 s3
                    LEFT JOIN
                        stiba_2022 s2 ON s2.Descricao_UO = s3.Descricao_UO";
                return await conn.QueryAsync<StibaModel>(query);
            }
        }

        public async Task<IEnumerable<StibaModel>> GetCultureScore()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
                    SELECT
                        COALESCE(s2.Descricao_UO, s3.Descricao_UO) AS Descricao_UO,
                        s2.Q01 AS Q01_2022, s3.Q01 AS Q01_2023,
                        s2.Q02 AS Q02_2022, s3.Q02 AS Q02_2023,
                        s2.Q03 AS Q03_2022, s3.Q03 AS Q03_2023,
                        s2.Q04 AS Q04_2022, s3.Q04 AS Q04_2023,
                        s2.Q05 AS Q05_2022, s3.Q05 AS Q05_2023,
                        s2.Q06 AS Q06_2022, s3.Q06 AS Q06_2023,
                        s2.Q07 AS Q07_2022, s3.Q07 AS Q07_2023,
                        s2.Q08 AS Q08_2022, s3.Q08 AS Q08_2023,
                        s2.Q09 AS Q09_2022, s3.Q09 AS Q09_2023,
                        s2.Q10 AS Q10_2022, s3.Q10 AS Q10_2023,
                        s2.Q11 AS Q11_2022, s3.Q11 AS Q11_2023,
                        s2.Q12 AS Q12_2022, s3.Q12 AS Q12_2023,
                        s2.Q13 AS Q13_2022, s3.Q13 AS Q13_2023,
                        s2.Q14 AS Q14_2022, s3.Q14 AS Q14_2023,
                        s2.Q15 AS Q15_2022, s3.Q15 AS Q15_2023,
                        s2.Q16 AS Q16_2022, s3.Q16 AS Q16_2023,
                        s2.Q17 AS Q17_2022, s3.Q17 AS Q17_2023,
                        s2.Q18 AS Q18_2022, s3.Q18 AS Q18_2023,
                        s2.Q19 AS Q19_2022, s3.Q19 AS Q19_2023,
                        s2.Q20 AS Q20_2022, s3.Q20 AS Q20_2023
                    FROM
                        stiba_2023 s3
                    FULL OUTER JOIN
                        stiba_2022 s2 ON s2.Descricao_UO = s3.Descricao_UO";
                return await conn.QueryAsync<StibaModel>(query);
            }
        }

        public async Task<IEnumerable<StibaModel>> GetDirectoryAnswerScore()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
                    SELECT
                        COALESCE(s2.Descricao_UO, s3.Descricao_UO) AS Descricao_UO,
                        s3.Descricao_UO,
                        s2.Q01 AS Q01_2022, s3.Q01 AS Q01_2023,
                        s2.Q02 AS Q02_2022, s3.Q02 AS Q02_2023,
                        s2.Q03 AS Q03_2022, s3.Q03 AS Q03_2023,
                        s2.Q04 AS Q04_2022, s3.Q04 AS Q04_2023,
                        s2.Q05 AS Q05_2022, s3.Q05 AS Q05_2023,
                        s2.Q06 AS Q06_2022, s3.Q06 AS Q06_2023,
                        s2.Q07 AS Q07_2022, s3.Q07 AS Q07_2023,
                        s2.Q08 AS Q08_2022, s3.Q08 AS Q08_2023,
                        s2.Q09 AS Q09_2022, s3.Q09 AS Q09_2023,
                        s2.Q10 AS Q10_2022, s3.Q10 AS Q10_2023,
                        s2.Q11 AS Q11_2022, s3.Q11 AS Q11_2023,
                        s2.Q12 AS Q12_2022, s3.Q12 AS Q12_2023,
                        s2.Q13 AS Q13_2022, s3.Q13 AS Q13_2023,
                        s2.Q14 AS Q14_2022, s3.Q14 AS Q14_2023,
                        s2.Q15 AS Q15_2022, s3.Q15 AS Q15_2023,
                        s2.Q16 AS Q16_2022, s3.Q16 AS Q16_2023,
                        s2.Q17 AS Q17_2022, s3.Q17 AS Q17_2023,
                        s2.Q18 AS Q18_2022, s3.Q18 AS Q18_2023,
                        s2.Q19 AS Q19_2022, s3.Q19 AS Q19_2023,
                        s2.Q20 AS Q20_2022, s3.Q20 AS Q20_2023
                    FROM
                        stiba_2023 s3
                    FULL OUTER JOIN
                        stiba_2022 s2 ON s2.Descricao_UO = s3.Descricao_UO";
                return await conn.QueryAsync<StibaModel>(query);
            }
        }

        public async Task<IEnumerable<StibaModel>> GetQuestionScore()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
                    SELECT
                        COALESCE(s2.Descricao_UO, s3.Descricao_UO) AS Descricao_UO,
                        AVG(s2.Q01) AS Avg_Q01_2022, AVG(s3.Q01) AS Avg_Q01_2023,
                        AVG(s2.Q02) AS Avg_Q02_2022, AVG(s3.Q02) AS Avg_Q02_2023,
                        AVG(s2.Q03) AS Avg_Q03_2022, AVG(s3.Q03) AS Avg_Q03_2023,
                        AVG(s2.Q04) AS Avg_Q04_2022, AVG(s3.Q04) AS Avg_Q04_2023,
                        AVG(s2.Q05) AS Avg_Q05_2022, AVG(s3.Q05) AS Avg_Q05_2023,
                        AVG(s2.Q06) AS Avg_Q06_2022, AVG(s3.Q06) AS Avg_Q06_2023,
                        AVG(s2.Q07) AS Avg_Q07_2022, AVG(s3.Q07) AS Avg_Q07_2023,
                        AVG(s2.Q08) AS Avg_Q08_2022, AVG(s3.Q08) AS Avg_Q08_2023,
                        AVG(s2.Q09) AS Avg_Q09_2022, AVG(s3.Q09) AS Avg_Q09_2023,
                        AVG(s2.Q10) AS Avg_Q10_2022, AVG(s3.Q10) AS Avg_Q10_2023,
                        AVG(s2.Q11) AS Avg_Q11_2022, AVG(s3.Q11) AS Avg_Q11_2023,
                        AVG(s2.Q12) AS Avg_Q12_2022, AVG(s3.Q12) AS Avg_Q12_2023,
                        AVG(s2.Q13) AS Avg_Q13_2022, AVG(s3.Q13) AS Avg_Q13_2023,
                        AVG(s2.Q14) AS Avg_Q14_2022, AVG(s3.Q14) AS Avg_Q14_2023,
                        AVG(s2.Q15) AS Avg_Q15_2022, AVG(s3.Q15) AS Avg_Q15_2023,
                        AVG(s2.Q16) AS Avg_Q16_2022, AVG(s3.Q16) AS Avg_Q16_2023,
                        AVG(s2.Q17) AS Avg_Q17_2022, AVG(s3.Q17) AS Avg_Q17_2023,
                        AVG(s2.Q18) AS Avg_Q18_2022, AVG(s3.Q18) AS Avg_Q18_2023,
                        AVG(s2.Q19) AS Avg_Q19_2022, AVG(s3.Q19) AS Avg_Q19_2023,
                        AVG(s2.Q20) AS Avg_Q20_2022, AVG(s3.Q20) AS Avg_Q20_2023
                    FROM
                        stiba_2022 s2
                    FULL OUTER JOIN
                        stiba_2023 s3 ON s2.Descricao_UO = s3.Descricao_UO
                    GROUP BY
                        COALESCE(s2.Descricao_UO, s3.Descricao_UO)";
                return await conn.QueryAsync<StibaModel>(query);
            }
        }

        public async Task<IEnumerable<QuestionYearlyAverage>> GetQuestionYearlyAveragesAsync()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
            SELECT
                Q01 AS QuestionId, AVG(s2.Q01) AS AverageScore2022, AVG(s3.Q01) AS AverageScore2023,
                Q02 AS QuestionId, AVG(s2.Q02) AS AverageScore2022, AVG(s3.Q02) AS AverageScore2023,
                Q03 AS QuestionId, AVG(s2.Q03) AS AverageScore2022, AVG(s3.Q03) AS AverageScore2023,
                Q04 AS QuestionId, AVG(s2.Q04) AS AverageScore2022, AVG(s3.Q04) AS AverageScore2023,
                Q05 AS QuestionId, AVG(s2.Q05) AS AverageScore2022, AVG(s3.Q05) AS AverageScore2023,
                Q06 AS QuestionId, AVG(s2.Q06) AS AverageScore2022, AVG(s3.Q06) AS AverageScore2023,
                Q07 AS QuestionId, AVG(s2.Q07) AS AverageScore2022, AVG(s3.Q07) AS AverageScore2023,
                Q08 AS QuestionId, AVG(s2.Q08) AS AverageScore2022, AVG(s3.Q08) AS AverageScore2023,
                Q09 AS QuestionId, AVG(s2.Q09) AS AverageScore2022, AVG(s3.Q09) AS AverageScore2023,
                Q10 AS QuestionId, AVG(s2.Q10) AS AverageScore2022, AVG(s3.Q10) AS AverageScore2023,
                Q11 AS QuestionId, AVG(s2.Q11) AS AverageScore2022, AVG(s3.Q11) AS AverageScore2023,
                Q12 AS QuestionId, AVG(s2.Q12) AS AverageScore2022, AVG(s3.Q12) AS AverageScore2023,
                Q13 AS QuestionId, AVG(s2.Q13) AS AverageScore2022, AVG(s3.Q13) AS AverageScore2023,
                Q14 AS QuestionId, AVG(s2.Q14) AS AverageScore2022, AVG(s3.Q14) AS AverageScore2023,
                Q15 AS QuestionId, AVG(s2.Q15) AS AverageScore2022, AVG(s3.Q15) AS AverageScore2023,
                Q16 AS QuestionId, AVG(s2.Q16) AS AverageScore2022, AVG(s3.Q16) AS AverageScore2023,
                Q17 AS QuestionId, AVG(s2.Q17) AS AverageScore2022, AVG(s3.Q17) AS AverageScore2023,
                Q18 AS QuestionId, AVG(s2.Q18) AS AverageScore2022, AVG(s3.Q18) AS AverageScore2023,
                Q19 AS QuestionId, AVG(s2.Q19) AS AverageScore2022, AVG(s3.Q19) AS AverageScore2023,
                Q10 AS QuestionId, AVG(s2.Q10) AS AverageScore2022, AVG(s3.Q10) AS AverageScore2023,
                Q20 AS QuestionId, AVG(s2.Q20) AS AverageScore2022, AVG(s3.Q20) AS AverageScore2023
            FROM
                stiba_2022 s2
            FULL OUTER JOIN
                stiba_2023 s3 ON s2.Descricao_UO = s3.Descricao_UO";
                var queryResult = await conn.QueryAsync(query);
                var yearlyAverages = queryResult.Select(x => new QuestionYearlyAverage
                {
                    QuestionId = x.QuestionId,
                    AverageScore2022 = x.AverageScore2022,
                    AverageScore2023 = x.AverageScore2023
                });

                return yearlyAverages;
            }
        }

        public async Task<IEnumerable<DirectoryAnswerScore>> GetDirectoryAnswerScoreAsync()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                var query = @"
                SELECT
                    s2.Descricao_UO,
                    s2.Q01 AS Score_Q01_2022, s3.Q01 AS Score_Q01_2023,
                    s2.Q02 AS Score_Q02_2022, s3.Q02 AS Score_Q02_2023,
                    s2.Q03 AS Score_Q03_2022, s3.Q03 AS Score_Q03_2023,
                    s2.Q04 AS Score_Q04_2022, s3.Q04 AS Score_Q04_2023,
                    s2.Q05 AS Score_Q05_2022, s3.Q05 AS Score_Q05_2023,
                    s2.Q06 AS Score_Q06_2022, s3.Q06 AS Score_Q06_2023,
                    s2.Q07 AS Score_Q07_2022, s3.Q07 AS Score_Q07_2023,
                    s2.Q08 AS Score_Q08_2022, s3.Q08 AS Score_Q08_2023,
                    s2.Q09 AS Score_Q09_2022, s3.Q09 AS Score_Q09_2023,
                    s2.Q10 AS Score_Q10_2022, s3.Q10 AS Score_Q10_2023,
                    s2.Q11 AS Score_Q11_2022, s3.Q11 AS Score_Q11_2023,
                    s2.Q12 AS Score_Q12_2022, s3.Q12 AS Score_Q12_2023,
                    s2.Q13 AS Score_Q13_2022, s3.Q13 AS Score_Q13_2023,
                    s2.Q14 AS Score_Q14_2022, s3.Q14 AS Score_Q14_2023,
                    s2.Q15 AS Score_Q15_2022, s3.Q15 AS Score_Q15_2023,
                    s2.Q16 AS Score_Q16_2022, s3.Q16 AS Score_Q16_2023,
                    s2.Q17 AS Score_Q17_2022, s3.Q17 AS Score_Q17_2023,
                    s2.Q18 AS Score_Q18_2022, s3.Q18 AS Score_Q18_2023,
                    s2.Q19 AS Score_Q19_2022, s3.Q19 AS Score_Q19_2023,
                    s2.Q20 AS Score_Q20_2022, s3.Q20 AS Score_Q20_2023
                FROM
                    stiba_2022 s2
                LEFT JOIN
                    stiba_2023 s3 ON s2.Descricao_UO = s3.Descricao_UO
                GROUP BY";
                return await conn.QueryAsync<DirectoryAnswerScore>(query);
            }
        }

        Task<StibaModel> IStibaRepository.GetStibaScore()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StibaModel>> GetDirectoryAnswerScoreYears()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<object>> GetDirectoryScoreYearsAsync()
        {
            throw new NotImplementedException();
        }
    }

*/
    