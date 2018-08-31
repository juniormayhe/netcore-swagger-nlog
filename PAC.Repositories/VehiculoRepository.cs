using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using PAC.Entities;

namespace PAC.Repositories
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly string _connectionString;
        private  IDbConnection _connection { get { return new SqlConnection(_connectionString); }}

        public VehiculoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Vehiculo> GetAsync(int id)
        {
            using (IDbConnection dbConnection = _connection)
            {
                string query = @"SELECT [IdVehiculo]
                                ,[NumeroOrden]
                                ,[Placa]
                                FROM [dbo].[Vehiculo]
                                WHERE [IdVehiculo] = @IdVehiculo";

                var vehiculo = await dbConnection.QueryFirstOrDefaultAsync<Vehiculo>(query, new{ @IdVehiculo = id });
                
                return vehiculo;
            }
        }
        
        public async Task<int> AddAsync(Vehiculo vehiculo)
        {
            using (IDbConnection dbConnection = _connection)
            {
                string query = @"INSERT INTO [dbo].[Vehiculo] (
                                [NumeroOrden],
                                [Placa]) VALUES (
                                @NumeroOrden,
                                @Placa)";

                int id = await dbConnection.ExecuteAsync(query, vehiculo);
                return id;
            }
        }

        public async Task<int> DeleteAsync(int idVehiculo)
        {
            using (IDbConnection dbConnection = _connection)
            {
                string query = @"DELETE FROM [dbo].[Vehiculo] WHERE IdVehiculo = @IdVehiculo";

                int processed = await dbConnection.ExecuteAsync(query, new { IdVehiculo = idVehiculo });
                return processed;
            }
        }

        /// <summary>
        /// returns paged collection and total records without paging
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="rowsPerPage"></param>
        /// <returns>A paged list and the total records</returns>
        public async Task<KeyValuePair<int, IEnumerable<Vehiculo>>> GetAllPagedAsync(int? pageNumber = 1, int? rowsPerPage = 10)
        {
            using (IDbConnection dbConnection = _connection)
            {
                string query = @"SELECT *
                            FROM Vehiculo
                            ORDER BY Placa";

                //check if paging is needed
                object param = null;
                if (pageNumber.HasValue) {
                    param = new { page = pageNumber.Value-1, rows = rowsPerPage.Value  };

                    query = $@"{query} OFFSET @page * @rows 
                            ROWS FETCH NEXT @rows ROWS ONLY";
                }

                //count the whole total of records without paging
                query = $"{query}; SELECT COUNT(*) FROM Vehiculo;";
                
                using (var resultSets = dbConnection.QueryMultipleAsync(query, param).Result)
                {
                    IEnumerable<Vehiculo> list = await resultSets.ReadAsync<Vehiculo>();
                    int totalRecords = await resultSets.ReadFirstAsync<int>();
                    return new KeyValuePair<int, IEnumerable<Vehiculo>>(totalRecords, list);
                }
    
            }
        }

        public async Task<int> UpdateAsync(Vehiculo vehiculo)
        {
            using (IDbConnection dbConnection = _connection)
            {
                string query = @"UPDATE [dbo].[Vehiculo] SET 
                                NumeroOrden = @NumeroOrden,
                                Placa = @Placa
                                WHERE IdVehiculo = @IdVehiculo";

                int processed = await dbConnection.ExecuteAsync(query, new { IdVehiculo = vehiculo.IdVehiculo, NumeroOrden = vehiculo.NumeroOrden, Placa= vehiculo.Placa });
                return processed;
            }
        }
    }
}