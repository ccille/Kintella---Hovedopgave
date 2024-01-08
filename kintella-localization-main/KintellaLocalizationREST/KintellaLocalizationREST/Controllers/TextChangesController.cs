using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Interfaces;
using KintellaLocalizationREST.Managers;
using KintellaLocalizationREST.Model;
using KintellaLocalizationREST.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KintellaLocalizationREST.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TextChangesController : ControllerBase
    {
        private readonly ITextChangesService _textChangesService;
        private readonly TextChangesDataManager _textChangesDataManger = new(new ApplicationDbContext());

        //Constructor with dependency injection
        public TextChangesController()
        {
            _textChangesService = new TextChangesService(_textChangesDataManger);
        }

        // GET: api/TextChanges/GetAllTextChangesFromDB
        [HttpGet("GetAllTextChanges")]
        public async Task<ActionResult<List<TextChanges>>> GetAllTextChangesFromDB()
        {
            var textchanges = await _textChangesService.GetAllTextChanges();
            return Ok(textchanges);
        }
        [HttpPost("PublishText")]
        public async Task<ActionResult> PublishText(int textChangesID)
        {
            bool success = _textChangesService.PublishText(textChangesID);

            if(success)
            {
                TextChanges newText = await _textChangesService.GetTextChangeAsync(textChangesID);
                return Created("Changes", newText);
            }
            return BadRequest();
        }
        [HttpPost("CancelTextPublishing")]
        public async Task<ActionResult> CancelTextPublishing(int textChangesID)
        {
            TextChanges newText = await _textChangesService.GetTextChangeAsync(textChangesID);
            bool success = _textChangesService.CancelTextPublishing(textChangesID);

            if(success)
            {
                return Ok(newText);
            }
            return BadRequest();
        }
        
        
    }
}
