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
    public class SubModulesController : ControllerBase
    {
        private readonly ISubModuleService _subModuleService;
        private readonly SubModuleDataManager _subModuleDataManager = new(new ApplicationDbContext());

        public SubModulesController()
        {
            _subModuleService = new SubModuleService(_subModuleDataManager);
        }

        // GET: api/<SubModulesController>
        [HttpGet("GetAllSubModules")]
        public async Task<ActionResult<List<SubModule>>> GetAllSubModulesAsync()
        {
            var submodules = await _subModuleService.GetAllSubModulesAsync();
            return Ok(submodules);
        }

        // GET api/<SubModulesController>/5
        [HttpGet("GetSubModule/{id}")]
        public async Task<ActionResult<SubModule>> GetSubModuleAsync(int id)
        {
            var subModule = await _subModuleService.GetSubModuleAsync(id);
            if (subModule == null)
            {
                return NotFound();
            }
            return Ok(subModule);
        }

        // POST api/<SubModulesController>
        [HttpPost("CreateSubModule")]
        public ActionResult Post([FromBody] SubModule subModule)
        {
            var success = _subModuleService.AddSubModule(subModule);
            if (success)
            {
                return CreatedAtAction(nameof(GetSubModuleAsync), new { id = subModule.SubModuleID }, subModule);
            }
            return BadRequest();
        }

        // PUT api/<SubModulesController>/5
        [HttpPut("UpdateSubModule/{id}")]
        public ActionResult Put(int id, [FromBody] SubModule subModule)
        {
            if (id != subModule.SubModuleID)
            {
                return BadRequest();
            }
            var success = _subModuleService.UpdateSubModule(subModule);
            if (success)
            {
                return Ok(subModule);
            }
            return NotFound();
        }

        // DELETE api/<SubModulesController>/5
        [HttpDelete("DeleteSubModule/{id}")]
        public ActionResult Delete(int id)
        {
            var success = _subModuleService.DeleteSubModule(id);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
