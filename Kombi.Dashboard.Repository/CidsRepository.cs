using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kombi.Dashboard.Repository
{
    public class CidsRepository : ICidsRepository
    {
        private readonly string _dbConfig;

        public CidsRepository(string dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<IEnumerable<CidsModel>> GetCids()
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<CidsModel>(@"
                    SELECT 
                        unidade as Unidade,
                        diretoria as Diretoria,
                        dias as Dias,
                        atestados as Atestados,
                        n_pessoal as N_Pessoal,
                        jornada as Jornada,
                        outros as Outros,
                        causa_raiz as Causa_Raiz,
                        diagnostico_atestado_inicial as Diagnostico_Atestado_Inicial,
                        descricao_resumida as Descricao_Resumida,
                        descricao_detalhada as Descricao_Detalhada ,
                        cid as Cid,
                        categoria as Categoria,
                        genero as Genero,
                        mes as Mes
                    FROM 
                        cid_f_2023_geral
                ");
            }
        }

        /*public async Task<CidsModel> GetCidsById(int id)
        {
            using (var conn = new NpgsqlConnection(_dbConfig))
            {
                await conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<CidsModel>(@"
                    SELECT 
                        unidade,
                        diretoria,
                        dias,
                        atestados,
                        n_pessoal,
                        jornada,
                        outros,
                        causa_raiz,
                        diagnostico_atestado_inicial,
                        descricao_resumida,
                        descricao_detalhada,
                        cid,
                        categoria,
                        genero,
                        mes
                    FROM 
                        cid_f_2023_geral
                    WHERE
                        id = @IdCids
                ", new { IdCids = id });
            }
        }*/


    }
}
