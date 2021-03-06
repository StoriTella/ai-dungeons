using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smart_dungeons.Domain.Users;
using smart_dungeons.DTO;
using smart_dungeons.Infrastructure;
using smart_dungeons.Domain.Shared;
namespace smart_dungeons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            var user = await _service.GetByIdAsync(new UserId(id));

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserLoginDTO dto)
        {
            try
            {
                var user = await _service.AddAsync(dto);

                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<bool>> Login(UserLoginDTO dto)
        {
            try
            {
                bool login = await _service.Login(dto);

                if (login) {
                return Ok();
                } else {
                return BadRequest(new {Message = "Login invalido"});
                }
            }
            catch(BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

    }
}
