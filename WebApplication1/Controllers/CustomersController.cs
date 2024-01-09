using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CusttomersController _dbContext;

        public CustomersController(CusttomersController dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            var customers = _dbContext.Customers.ToList();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> AddCustomer(Customer customer)
        {
            customer.Created = DateTime.UtcNow;
            customer.CustomerNumber = Guid.NewGuid();

            bool isNameExists = _dbContext.Customers.Any(c => c.NameFull == customer.NameFull);
            if (isNameExists)
            {
                return BadRequest("שם הלקוח כבר קיים במערכת.");
            }

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return Ok(customer);
        }

        [HttpPut("{id}")]
        public ActionResult<Customer> UpdateCustomer(int id, Customer updatedCustomer)
        {
            var existingCustomer = _dbContext.Customers.FirstOrDefault(c => c.Id == id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.NameFull = updatedCustomer.NameFull;

            _dbContext.SaveChanges();

            return Ok(existingCustomer);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            customer.IsDeleted = true;

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        [Route("customers/{customerId}/contacts")]
        public ActionResult<Customer> AddContactToCustomer(int customerId, Contact contact)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            contact.CustomerId = customerId;

            customer.Contacts.Add(contact);

            _dbContext.SaveChanges();

            return Ok(customer);
        }

    }
}
