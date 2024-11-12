using MediatR;
using PruebaRedArborBryan.Application.Commands;
using PruebaRedArborBryan.Domain.Entities;
using PruebaRedArborBryan.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaRedArborBryan.Application.CommandHandler
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployeeWriteRepository _writeRepository;

        public CreateEmployeeCommandHandler(IEmployeeWriteRepository writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                CompanyId = request.CompanyId,
                Email = request.Email,
                Password = request.Password,
                PortalId = request.PortalId,
                RoleId = request.RoleId,
                StatusId = request.StatusId,
                Username = request.Username,
                Fax = request.Fax,
                Name = request.Name,
                Telephone = request.Telephone,
                CreatedOn = DateTime.UtcNow
            };

            await _writeRepository.AddAsync(employee);
            return employee.Id;
        }
    }
}
