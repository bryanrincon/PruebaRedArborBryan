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
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
    {
        private readonly IEmployeeWriteRepository _writeRepository;
        private readonly IEmployeeReadRepository _readRepository;

        public DeleteEmployeeCommandHandler(IEmployeeWriteRepository writeRepository, IEmployeeReadRepository readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Usa el repositorio de lectura para obtener el empleado
            var employee = await _readRepository.GetByIdAsync(request.Id);
            if (employee == null) throw new NotFoundException("Employee not found");

            employee.DeletedOn = DateTime.UtcNow;
            employee.UpdatedOn = DateTime.UtcNow;
            await _writeRepository.UpdateAsync(employee);

            return Unit.Value;
        }
    }
}
