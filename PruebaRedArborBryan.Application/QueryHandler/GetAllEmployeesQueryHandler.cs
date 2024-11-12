using MediatR;
using PruebaRedArborBryan.Application.Queries;
using PruebaRedArborBryan.Domain.Entities;
using PruebaRedArborBryan.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaRedArborBryan.Application.QueryHandler
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Employee>>
    {
        private readonly IEmployeeReadRepository _readRepository;

        public GetAllEmployeesQueryHandler(IEmployeeReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository.GetAllAsync();
        }
    }
}
