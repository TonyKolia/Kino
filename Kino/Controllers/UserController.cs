using Kino.Models;
using Kino.Models.KinoDBModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult> AddUser(User user)
        {
            try
            {
                if(await userRepository.UserExists(user.Username, null))
                {
                    return BadRequest("Username already exists");
                }

                if (await userRepository.UserExists(null, user.Email))
                {
                    return BadRequest("Email already exists");
                }

                var addedUser = await userRepository.AddUser(user);
                return StatusCode(StatusCodes.Status201Created, addedUser);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding user to the database");
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if(await userRepository.GetUser(id, "") != null)
            {
                await userRepository.DeteleUser(id);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<ActionResult> GetUser(int? id, string username)
        {
            var user = await userRepository.GetUser(id, username);
            if(user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<ActionResult> GetUsers()
        {
            return Ok(await userRepository.GetUsers());
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] User user)
        {
            try
            {
                var updatedUser = await userRepository.UpdateUser(user);
                return Ok(updatedUser);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user to the database");
            }
        }

        [HttpGet]
        [Route("UserExists")]
        public async Task<ActionResult> UserExists(string username, string email)
        {
            return Ok(await userRepository.UserExists(username, email));
        }

        [HttpGet]
        [Route("ValidateUserCredentials")]
        public async Task<ActionResult> ValidateUserCredentials(string username, string password)
        {
            if (await userRepository.ValidateUserCredentials(username, password))
            {
                return Ok(await userRepository.GetUser(null, username));
            }
            else
            {
                return BadRequest("Invalid credential combination");
            }
        }

    }
}
