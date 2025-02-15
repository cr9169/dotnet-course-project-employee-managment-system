
using Microsoft.EntityFrameworkCore;

public class EmployeeRepository : IEmployeeRepository
{
    // A readonly prop - can be assigned only in the actual declaration or in the constructor.
    private readonly AppDbContext _context;

    // Performing DI in order to use our AppDbContext.
    // Using it as a readonly property to use it outside of the constructor scope.
    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddEmployeeAsync(Employee employee)
    {
        // Uses Our context, and referencing the Employees DbSet, to add the employee asyncrounsly.
        await _context.Employees.AddAsync(employee);
        // Importent to actually save the operation we have performed.
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FindAsync();
        
        if(employee == null) {
            throw new KeyNotFoundException($"Emploee with id {id} could'nt be found.");
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    // Employee can be empty. ("?" - nullable sign)
    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();

    }
}