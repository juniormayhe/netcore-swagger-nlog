using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using PAC.Entities;

namespace PAC.Repositories
{
    public class VinculadoRepository : IVinculadoRepository
    {
        private readonly string _connectionString;
        private  IDbConnection _connection { get { return new SqlConnection(_connectionString); }}

        public VinculadoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Vinculado> GetAsync(long id)
        {
            using (IDbConnection dbConnection = _connection)
            {
                string query = @"SELECT [IdVinculado]
                                ,[Cedula]
                                ,[Nombre]
                                FROM [dbo].[Vinculado]
                                WHERE [Id] = @Id";

                var vinculado = await dbConnection.QueryFirstOrDefaultAsync<Vinculado>(query, new{ @Id = id });

                return vinculado;
            }
        }
        public async Task<IEnumerable<Vinculado>> GetAllAsync()
        {
            //TODO: Paging...
            using (IDbConnection dbConnection = _connection)
            {
                string query = @"SELECT [IdVinculado]
                                ,[Cedula]
                                ,[Nombre]
                                FROM [dbo].[Vinculado]";

                var vinculado = await dbConnection.QueryAsync<Vinculado>(query);

                return vinculado;
            }
        }

        public async Task<int> AddAsync(Vinculado vinculado)
        {
            using (IDbConnection dbConnection = _connection)
            {
                string query = @"INSERT INTO [dbo].[Vinculado] (
                                [Cedula],
                                [Nombre]) VALUES (
                                @Cedula,
                                @Nombre)";

                int idVinculado = await dbConnection.ExecuteAsync(query, vinculado);
                return idVinculado;
            }
        }
    }
}