using MediatR;
using PruebaRedArborBryan.Application.Commands;
using PruebaRedArborBryan.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaRedArborBryan.Application.CommandHandler
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IEmployeeReadRepository _readRepository;
        private readonly IEmployeeWriteRepository _writeRepository;

        public UpdateEmployeeCommandHandler(IEmployeeReadRepository readRepository, IEmployeeWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _readRepository.GetByIdAsync(request.Id);
            if (employee == null) throw new NotFoundException("Employee not found");

            employee.Username = request.Username;
            employee.Email = request.Email;
            employee.Password = request.Password;
            employee.Fax = request.Fax;
            employee.Name = request.Name;
            employee.Telephone = request.Telephone;
            employee.UpdatedOn = DateTime.UtcNow;

            await _writeRepository.UpdateAsync(employee);

            return Unit.Value;
        }
    }
}
