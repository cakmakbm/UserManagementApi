using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Models;
using UserManagementApi.Repositories;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAllUsers());
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _repository.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
       
        

        // POST: api/users
        [HttpPost]
        public IActionResult Create(User user)
        {
            _repository.AddUser(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            if (id != user.Id) return BadRequest("ID uyuşmazlığı.");

            var existingUser = _repository.GetUserById(id);
            if (existingUser == null) return NotFound();

            _repository.UpdateUser(user);
            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _repository.GetUserById(id);
            if (user == null) return NotFound();

            _repository.DeleteUser(id);
            return NoContent();
        }
    }
}