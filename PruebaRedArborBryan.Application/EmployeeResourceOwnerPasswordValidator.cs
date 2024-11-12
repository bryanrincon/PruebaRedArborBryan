using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using PruebaRedArborBryan.Infrastructure.Interfaces;
using PruebaRedArborBryan.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PruebaRedArborBryan.Application
{
    public class EmployeeResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IEmployeeReadRepository _employeeReadRepository;
        private readonly IEmployeeWriteRepository _employeeWriteRepository;

        public EmployeeResourceOwnerPasswordValidator(IEmployeeReadRepository employeeReadRepository, IEmployeeWriteRepository employeeWriteRepository)
        {
            _employeeReadRepository = employeeReadRepository;
            _employeeWriteRepository = employeeWriteRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var employee = await _employeeReadRepository.ValidateEmployeeCredentialsAsync(context.UserName, context.Password);

            if (employee != null)
            {
                employee.LastLogin = DateTime.UtcNow;
                await _employeeWriteRepository.UpdateAsync(employee);

                // Autenticación exitosa
                context.Result = new GrantValidationResult(
                    subject: employee.Id.ToString(),
                    authenticationMethod: "custom",
                    claims: new List<Claim> {
                    new Claim("name", employee.Name),
                    new Claim("email", employee.Email),
                    new Claim("role", employee.RoleId.ToString())
                    }
                );

            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid credentials");
            }
        }
    }
}
