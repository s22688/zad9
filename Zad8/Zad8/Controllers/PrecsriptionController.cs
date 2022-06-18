using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zad8.Models;
using Zad8.Services;

namespace Zad8.Controllers
{
    [Route("api")]
    [ApiController]
    public class PrecsriptionController : ControllerBase
    {
        private readonly IDbService _dbService;

        public PrecsriptionController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IEnumerable<object>> GetDoctor(int id)
        {
            return await _dbService.GetDoctor(id);
        }

        [HttpGet]
        [Route("{id}/prescription")]
        [Authorize]
        public async Task<IEnumerable<object>> GetPerscription(int id)
        {
            return await _dbService.GetPerciption(id);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddDoctor(Doctor doctor)
        {
            if (await _dbService.AddDoctor(doctor)) return Ok("Doctor added");
            else return BadRequest("There is mistake in the data");
        }

        [HttpPost]
        [Route("{id}/change")]
        [Authorize]
        public async Task<IActionResult> ChangeDoctor(Doctor doctor, int id)
        {
            if (await _dbService.PostDoctor(doctor,id)) return Ok("Data changed");
            else return BadRequest("There is mistake in the data");
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            if (await _dbService.DeleteDoctor(id)) return Ok("Doctor deleted");
            else return BadRequest("There is mistake in the data");
        }


    }
}
