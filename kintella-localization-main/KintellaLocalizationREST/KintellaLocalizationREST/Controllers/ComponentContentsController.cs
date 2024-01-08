using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Interfaces;
using KintellaLocalizationREST.Managers;
using KintellaLocalizationREST.Model;
using KintellaLocalizationREST.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KintellaLocalizationREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentContentsController : ControllerBase
    {
        private readonly IComponentContentService _componentContentService;
        private readonly ComponentContentDataManager _componentContentDataManager = new(new ApplicationDbContext());

        public ComponentContentsController()
        {
            _componentContentService = new ComponentContentService(_componentContentDataManager);
        }

        // GET: api/<ComponentContentsController>
        [HttpGet("GetAllComponentContents")]
        public async Task<ActionResult<List<ComponentContent>>> GetAllComponentContentsAsync()
        {
            var componentContents = await _componentContentService.GetAllComponentContentsAsync();
            return Ok(componentContents);
        }

        // GET api/<ComponentContentsController>/5
        [HttpGet("GetComponentContent/{id}")]
        public async Task<ActionResult<ComponentContent>> GetComponentContentAsync(int id)
        {
            var componentContent = await _componentContentService.GetComponentContentAsync(id);
            if (componentContent == null)
            {
                return NotFound();
            }
            return Ok(componentContent);
        }

        // POST api/<ComponentContentsController>
        [HttpPost("CreateComponentContent")]
        public ActionResult Post([FromBody] ComponentContent componentContent)
        {
            var success = _componentContentService.AddComponentContent(componentContent);
            if (success)
            {
                return CreatedAtAction(nameof(GetComponentContentAsync), new { id = componentContent.ComponentContentID }, componentContent);
            }
            return BadRequest();
        }

        // PUT api/<ComponentContentsController>/5
        [HttpPut("UpdateComponentContent/{id}")]
        public ActionResult Put(int id, [FromBody] ComponentContent componentContent)
        {
            if (id != componentContent.ComponentContentID)
            {
                return BadRequest();
            }
            var success = _componentContentService.UpdateComponentContent(componentContent);
            if (success)
            {
                return Ok(componentContent);
            }
            return NotFound();
        }

        // DELETE api/<ComponentContentsController>/5
        [HttpDelete("DeleteComponentContent{id}")]
        public ActionResult Delete(int id)
        {
            var success = _componentContentService.DeleteComponentContent(id);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
