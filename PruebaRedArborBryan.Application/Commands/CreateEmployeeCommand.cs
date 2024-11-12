﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaRedArborBryan.Application.Commands
{
    public record CreateEmployeeCommand : IRequest<int>
    {
        public int CompanyId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PortalId { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public string Username { get; set; }
        public string? Fax { get; set; }
        public string Name { get; set; }
        public string? Telephone { get; set; }
    }
}