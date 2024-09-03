using CustomerRegistration.Data;
using CustomerRegistration.Models.Dtos;
using CustomerRegistration.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CustomerController(ApplicationDbContext db)
        {
            this._db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _db.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCustomerDto customerDto)
        {
            var customer = new Customer
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                PhoneNumber = customerDto.PhoneNumber,
                Email = customerDto.Email,
                Address = customerDto.Address,
                City = customerDto.City,
                PostalCode = customerDto.PostalCode
            };

            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await _db.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new { Message = "Customer not found" });
            }

            return Ok(customer);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _db.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new { Message = "Customer not found" });
            }

            customer.FirstName = updateCustomerDto.FirstName;
            customer.LastName = updateCustomerDto.LastName;
            customer.PhoneNumber = updateCustomerDto.PhoneNumber;
            customer.Email = updateCustomerDto.Email;
            customer.Address = updateCustomerDto.Address;
            customer.City = updateCustomerDto.City;
            customer.PostalCode = updateCustomerDto.PostalCode;

            _db.Customers.Update(customer);
            await _db.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _db.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new { Message = "Customer not found" });
            }

            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
            return Ok(new { Message = "Customer deleted successfully" });
        }


    }
}
