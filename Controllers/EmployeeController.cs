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
            return Ok(employee);
        } catch (Exception err) {
            return StatusCode(500, $"There was an error: {err}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> AddEmployeeAsync(Employee employee) {
        try {
            await _repository.AddEmployeeAsync(employee);
            // Ok returns 200, Created 201 (successfully created).
            return Created();
        } catch (Exception err) {
            return StatusCode(500, $"There was an error: {err}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Employee>> DeleteEmployeeAsync(int id) {
        try {
            var deletedEmployee = await GetByIdAsync(id);
            await _repository.DeleteEmployeeAsync(id);
            return Ok(deletedEmployee);
        } catch (Exception err) {
            return StatusCode(500, $"There was an error: {err}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Employee>> UpdateEmployeeAsync(Employee employee) {
        try {
            await _repository.UpdateEmployeeAsync(employee);
            return Ok(employee);
        } catch (Exception err) {
            return StatusCode(500, $"There was an error: {err}");
        }
    }

}