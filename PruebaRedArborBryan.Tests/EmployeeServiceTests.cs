using Moq;
using PruebaRedArborBryan.Domain.Entities;
using PruebaRedArborBryan.Infrastructure.Interfaces;
using PruebaRedArborBryan.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaRedArborBryan.Application.Commands;
using PruebaRedArborBryan.Application.Queries;
using PruebaRedArborBryan.Api.Controllers;

namespace PruebaRedArborBryan.Tests
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly EmployeesController _controller;

        public EmployeeControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new EmployeesController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllEmployees_ReturnsOkResult_WithEmployees()
        {
            var result = await _controller.GetAllEmployees();

            var okResult = Assert.IsType<ActionResult<IEnumerable<Employee>>>(result);
        }

        [Fact]
        public async Task GetEmployeeById_ReturnsOkResult_WithEmployee()
        {
            var result = await _controller.GetEmployeeById(1);

            var okResult = Assert.IsType<ActionResult<Employee>>(result);
        }

        [Fact]
        public async Task CreateEmployee_ReturnsCreatedAtAction_WithEmployee()
        {
            var employee = new CreateEmployeeCommand
            {
                Name = "Bryan Rincon",
                Email = "bryan.rincon@hotmail.com",
                CompanyId = 1,
                Fax = "123456",
                Password = "123",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "+573226648369",
                Username = "bryan"
            };

            var result = await _controller.CreateEmployee(employee);

            var createdResult = Assert.IsType<ActionResult<int>>(result);
            var returnValue = Assert.IsType<int>(createdResult.Value);
        }

        [Fact]
        public async Task UpdateEmployee_ReturnsOkResult_WithUpdatedEmployee()
        {
            var employee = new UpdateEmployeeCommand
            {
                Name = "Bryan Rincon",
                Email = "bryan.rincon@hotmail.com",
                Fax = "123456",
                Password = "123",
                Telephone = "+573226648369",
                Username = "bryan",
                Id = 1
            };            

            var result = await _controller.UpdateEmployee(1,employee);

            var okResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteEmployee_ReturnsNoContent()
        {
            var employeeId = 1;

            var result = await _controller.DeleteEmployee(employeeId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
