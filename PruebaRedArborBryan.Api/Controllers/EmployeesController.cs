using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaRedArborBryan.Application.Commands;
using PruebaRedArborBryan.Application.Queries;
using PruebaRedArborBryan.Domain.Entities;

namespace PruebaRedArborBryan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var query = new GetAllEmployeesQuery();
            var employees = await _mediator.Send(query);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var query = new GetEmployeeByIdQuery(id);
            var employee = await _mediator.Send(query);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<int>> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var employeeId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeId }, employeeId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
            if (id != command.Id)
                return BadRequest("Employee ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var command = new DeleteEmployeeCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
