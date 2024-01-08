using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Interfaces;
using KintellaLocalizationREST.Managers;
using KintellaLocalizationREST.Mockdata;
using KintellaLocalizationREST.Model;
using KintellaLocalizationREST.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KintellaLocalizationREST.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService _moduleService;
        private readonly ModuleDataManager _moduleDataManager = new(new ApplicationDbContext());

        private readonly MockModules _mockmodules = new();

        public ModulesController()
        {
            _moduleService = new ModuleService(_moduleDataManager);
        }

        // GET: api/<ModulesController>
        [HttpGet("GetAllModules")]
        public async Task<ActionResult<List<Module>>> GetAllModulesAsync()
        {
            var modules = await _moduleService.GetAllModulesAsync();
            return Ok(modules);
        }

        // GET api/<ModulesController>/5
        [HttpGet("GetModule/{id}")]
        public async Task<ActionResult<Module>> GetModuleAsync(int id)
        {
            var module = await _moduleService.GetModuleAsync(id);
            if (module == null)
            {
                return NotFound();
            }
            return Ok(module);
        }

        // POST api/<ModulesController>
        [HttpPost("CreateModule")]
        public ActionResult Post([FromBody] Module module)
        {
            var success = _moduleService.AddModule(module);
            if (success)
            {
                return CreatedAtAction(nameof(GetModuleAsync), new { id = module.ModuleID }, module);
            }
            return BadRequest();
        }

        // PUT api/<ModulesController>/5
        [HttpPut("UpdateModule/{id}")]
        public ActionResult Put(int id, [FromBody] Module module)
        {
            if (id != module.ModuleID)
            {
                return BadRequest();
            }
            var success = _moduleService.UpdateModule(module);
            if (success)
            {
                return Ok(module);
            }
            return NotFound();
        }


        // DELETE api/<ModulesController>/5
        [HttpDelete("DeleteModule{id}")]
        public ActionResult Delete(int id)
        {
            var success = _moduleService.DeleteModule(id);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }

        #region Mock Modules
        // ## MOCK MODULES ##
        //GET api/<LocaizationController>/5
        //[HttpGet("GetAllModulesFromMockData")]
        //public List<Module> GetAllModules()
        //{
        //    //Mockmodules kk = _mockmodules.allmodules
        //    return _mockmodules.allmodules;
        //}

        //[HttpGet("GetAllTextFromMockData")]
        //public List<Text> GetAllText()
        //{
        //    //Mockmodules kk = _mockmodules.allmodules
        //    return _mockmodules.alltext;
        //}
        #endregion 
    }
}
