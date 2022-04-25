using Birthday.Base;
using Birthday.Routes;
using Business.Interfaces;
using Business.Services;
using Dto.Models;
using Gridify;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Birthday.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BirthdayBaseController
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService person,
        ServiceResponse<object> serviceResponse)
        {
            this._personService = person;
            ServiceResponse = serviceResponse ?? throw new ArgumentNullException(nameof(serviceResponse));
        }

        [HttpGet(ApiRoutes.GetAll)]
        public async Task<IActionResult> GetAllPersons([FromQuery] GridifyQuery query)
        {
            var response = await _personService.GetPersonsAsync(query, Token);
            return !response.Success ? (IActionResult)BadRequest(response) : Ok(response);
        }

        [HttpGet(ApiRoutes.Get)]
        public async Task<IActionResult> GetPerson([FromRoute] int id)
        {
            var response = await _personService.GetPerson(id, Token);
            return !response.Success ? (IActionResult)BadRequest(response) : Ok(response);
        }


        [HttpPost(ApiRoutes.Save), DisableRequestSizeLimit]
        public async Task<IActionResult> PostPerson([FromBody] PersonDto dto)
        {
            var response = await _personService.AddPerson(dto, Token);
            return !response.Success ? (IActionResult)BadRequest(response) : Ok(response);
        }


        [HttpPut(ApiRoutes.Update)]
        public async Task<IActionResult> UpdatePerson([FromRoute] int id, [FromBody] PersonDto dto)
        {
            var response = await _personService.UpdatePerson(id, dto, Token);
            return !response.Success ? (IActionResult)BadRequest(response) : Ok(response);
        }


        [HttpDelete(ApiRoutes.Delete)]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            var response = await _personService.DeletePerson(id, Token);
            return !response.Success ? (IActionResult)BadRequest(response) : Ok(response);
        }

        [AllowAnonymous]
        [HttpPut("Upload/" + ApiRoutes.SaveLogo), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadLogo([FromRoute] int id, [FromForm] FileModel request)
        {
            try
            {
                var reqFile = Request.Form.Files[0];
                if (request.File is null) return BadRequest();
                var file = request.File.FileName;
                await _personService.UploadImage(id, file, Token);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        public class FileModel
        {
            public IFormFile File { get; set; }
        }
    }
}
