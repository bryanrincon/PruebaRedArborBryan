using Dapper;
using PruebaRedArborBryan.Domain.Entities;
using PruebaRedArborBryan.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaRedArborBryan.Infrastructure.Repository
{
    public class EmployeeReadRepository : IEmployeeReadRepository
    {
        private readonly string _connectionString;

        public EmployeeReadRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Employee>("SELECT * FROM Employees WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Employee>("SELECT * FROM Employees WHERE DeletedOn IS NULL");
            }
        }

        public async Task<Employee> ValidateEmployeeCredentialsAsync(string username, string password)
        {
            using var connection = new SqlConnection(_connectionString);
            var employee = await connection.QueryFirstOrDefaultAsync<Employee>(
                "SELECT * FROM Employees WHERE Username = @Username AND Password = @Password",
                new { Username = username, Password = password }
            );

            return employee;
        }
    }
}
