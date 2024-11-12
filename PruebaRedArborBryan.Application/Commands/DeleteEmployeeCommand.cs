using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaRedArborBryan.Application.Commands
{
    public record DeleteEmployeeCommand(int Id) : IRequest<Unit>;
}
