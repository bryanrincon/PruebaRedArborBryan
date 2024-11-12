using MediatR;
using PruebaRedArborBryan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaRedArborBryan.Application.Queries
{
    public record GetEmployeeByIdQuery(int Id) : IRequest<Employee>;
}
