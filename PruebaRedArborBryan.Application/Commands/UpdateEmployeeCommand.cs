using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaRedArborBryan.Application.Commands
{
    public record UpdateEmployeeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Fax { get; set; }
        public string Name { get; set; }
        public string? Telephone { get; set; }
    }
}
