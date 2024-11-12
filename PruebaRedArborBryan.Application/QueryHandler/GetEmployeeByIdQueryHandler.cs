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
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IEmployeeReadRepository _readRepository;

        public GetEmployeeByIdQueryHandler(IEmployeeReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository.GetByIdAsync(request.Id);
        }
    }
}
