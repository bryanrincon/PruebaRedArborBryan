using PruebaRedArborBryan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaRedArborBryan.Infrastructure.Interfaces
{
    public interface IEmployeeReadRepository
    {
        Task<Employee> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> ValidateEmployeeCredentialsAsync(string username, string password);
    }
}
