using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TokenAuthLogin.Models;
using TokenAuthLogin.Helper;

namespace TokenAuthLogin.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeContext _context;
        private readonly IJWTAuthenticationManager jWTAuthenticationManager;
 
        // public EmployeesController(IJWTAuthenticationManager jWTAuthenticationManager)
        // {
        //     this.jWTAuthenticationManager = jWTAuthenticationManager;
        // }

        public EmployeesController(IJWTAuthenticationManager jWTAuthenticationManager, EmployeeContext context)
        {
            this.jWTAuthenticationManager = jWTAuthenticationManager;
            _context = context;
            
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Employee employee)
        {
            string token;
            List<Employee> employees = _context.employees.ToList();
            foreach(Employee emp in employees)
            {
                if(employee.UserName.Equals(emp.UserName) && employee.Password.Equals(emp.Password)){
                    token = jWTAuthenticationManager.Authenticate(employee.UserName, employee.Password);
                    if (token == null) 
                    return Unauthorized();
             
                    return Ok(token);
                }
                
            }
            // var token = jWTAuthenticationManager.Authenticate(employee.UserName, employee.Password);
             
            // if (token == null) 
            //     return Unauthorized();
             
             return Ok("Something went wrong");
        }

        // GET: api/Employees
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Getemployees()
        {
            return await _context.employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            var employee = await _context.employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(long id)
        {
            return _context.employees.Any(e => e.Id == id);
        }
    }
}
