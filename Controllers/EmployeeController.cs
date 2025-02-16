using Microsoft.AspNetCore.Mvc;

// http://localhost/api/employee (the class name without the suffix).
// We have defined the base route for this books controller.
[Route("api/[Controller]")]
// We attached the ApiController that marks the class as a Web API controller, enabling built-in behaviors like checking if there are server side errors.
[ApiController]

public class EmployeeController : ControllerBase {

    private readonly IEmployeeRepository _repository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _repository = employeeRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAllAsync() {
        try {
            var employees = await _repository.GetAllAsync();
            return Ok(employees);
        } catch (Exception err) {
            return StatusCode(500, $"There was an error: {err}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetByIdAsync(int id) {
        try {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null) 
            return NotFound($"Employee with ID {id} not found.");

        return Ok(employee);
    } catch (Exception err) {
        return StatusCode(500, $"There was an error: {err.Message}");
    }
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> AddEmployeeAsync(Employee employee) {

        // If any of the validations in the Employee model fails, returns 400;
        if(!ModelState.IsValid) {
            return BadRequest();
        }

        try {
            await _repository.AddEmployeeAsync(employee);
            // Returns 201 Created status code with:
            // 1. The newly created employee in the response body.
            // 2. A Location header with path /api/Employee/{id} for accessing the new resource.
            return Created($"/api/Employee/{employee.Id}", employee);
        } catch (Exception err) {
            return StatusCode(500, $"There was an error: {err}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEmployeeAsync(int id) {
        try {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return NotFound($"Employee with ID {id} not found.");

            await _repository.DeleteEmployeeAsync(id);
            return NoContent();
        } catch (Exception err) {
            return StatusCode(500, $"There was an error: {err.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Employee>> UpdateEmployeeAsync(int id, Employee employee) {
        try {
            if(id != employee.Id) {
                return BadRequest();
            }

            // If any of the validations in the Employee model fails, returns 400;
            if(!ModelState.IsValid) {
            return BadRequest();
            }

            await _repository.UpdateEmployeeAsync(employee);
            // Returns 201 Created status code with:
            // 1. The newly created employee in the response body.
            // 2. A Location header with path /api/Employee/{id} for accessing the new resource.
            return Created($"/api/Employee/{employee.Id}", employee);
        } catch (Exception err) {
            return StatusCode(500, $"There was an error: {err}");
        }
    }

}