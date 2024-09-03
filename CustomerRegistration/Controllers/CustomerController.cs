using CustomerRegistration.Data;
using CustomerRegistration.Models.Dtos;
using CustomerRegistration.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerRegistration.Controllers;

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
        try
        {
            var customers = await _db.Customers.Where(customer => !customer.IsDeleted).ToListAsync();
            if (customers != null)
            {
                return Ok(new ResponceVM<List<Customer>>()
                {
                    Code = 200,
                    Data = customers,
                    IsSuccess = true,
                    Message = "Success"
                });
            }
            return NotFound(new ResponceVM<string>()
            {
                Code = 404,
                IsSuccess = false,
                Message = "Not Found"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponceVM<string>()
            {
                Code = 500,
                Data = ex.Message,
                IsSuccess = false,
                Message = "An error occurred while getting the customers."
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddCustomerDto customerDto)
    {
        try
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
            return Ok(new ResponceVM<Customer>()
            {
                Code = 200,
                Data = customer,
                IsSuccess = true,
                Message = "Success"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponceVM<string>()
            {
                Code = 500,
                Data = ex.Message,
                IsSuccess = false,
                Message = "An error occurred while adding the customer."
            });
        }
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetCustomer(Guid id)
    {
        try
        {
            var customer = await _db.Customers.FindAsync(id);
            if (customer != null)
            {
                return Ok(new ResponceVM<Customer>()
                {
                    Code = 200,
                    Data = customer,
                    IsSuccess = true,
                    Message = "Success"
                });
            }
            return NotFound(new ResponceVM<string>()
            {
                Code = 404,
                IsSuccess = false,
                Message = "Not Found"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponceVM<string>()
            {
                Code = 500,
                Data = ex.Message,
                IsSuccess = false,
                Message = "An error occurred while getting the customer."
            });
        }
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCustomerDto updateCustomerDto)
    {
        try
        {
            var customer = await _db.Customers.FindAsync(id);
            if (customer != null)
            {
                customer.FirstName = updateCustomerDto.FirstName;
                customer.LastName = updateCustomerDto.LastName;
                customer.PhoneNumber = updateCustomerDto.PhoneNumber;
                customer.Email = updateCustomerDto.Email;
                customer.Address = updateCustomerDto.Address;
                customer.City = updateCustomerDto.City;
                customer.PostalCode = updateCustomerDto.PostalCode;

                _db.Customers.Update(customer);
                await _db.SaveChangesAsync();

                return Ok(new ResponceVM<Customer>()
                {
                    Code = 200,
                    Data = customer,
                    IsSuccess = true,
                    Message = "Success"
                });
            }
            return NotFound(new ResponceVM<string>()
            {
                Code = 404,
                IsSuccess = false,
                Message = "Not Found"
            });

        }
        catch (Exception ex)
        {
            return BadRequest(new ResponceVM<string>()
            {
                Code = 500,
                Data = ex.Message,
                IsSuccess = false,
                Message = "An error occurred while updating the customer."
            });
        }
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var customer = await _db.Customers.FindAsync(id);
            if(customer != null)
            {
                customer.IsDeleted = true;
                _db.Customers.Update(customer);
                await _db.SaveChangesAsync();

                return Ok(new ResponceVM<Customer>()
                {
                    Code = 200,
                    Data = customer,
                    IsSuccess = true,
                    Message = "Success"
                });
            }
            return NotFound(new ResponceVM<string>()
            {
                Code = 404,
                IsSuccess = false,
                Message = "Not Found"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponceVM<string>()
            {
                Code = 500,
                Data = ex.Message,
                IsSuccess = false,
                Message = "An error occurred while deleting the customer."
            });
        }
    }
}
