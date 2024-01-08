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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TextsController : ControllerBase
    {
        private readonly ITextService _textService;
        private readonly ITextChangesService _textChangesService;
        private readonly TextDataManager _textDataManager = new(new ApplicationDbContext());
        private readonly TextChangesDataManager _textChangesDataManager = new(new ApplicationDbContext());

        // Constructor with dependency injection
        public TextsController()
        {
            _textService = new TextService(_textDataManager);
            _textChangesService = new TextChangesService(_textChangesDataManager);
        }

        // GET api/Texts/GetText/5
        [HttpGet("GetText/{id}")]
        public async Task<ActionResult<Text>> GetTextAsync(int id)
        {
            var textInfo = await _textService.GetTextAsync(id);
            if (textInfo == null)
            {
                return NotFound();
            }
            return Ok(textInfo);
        }

        // GET api/Texts/GetAllTextsFromDb
        [HttpGet("GetAllTexts")]
        public async Task<ActionResult<List<Text>>> GetAllTextsFromDb()
        {
            var texts = await _textService.GetAllTextsAsync();
            return Ok(texts);
        }

        // POST api/Texts/CreateText
        [HttpPost("CreateText")]
        public async Task<ActionResult> Post([FromBody] TextChanges text) // TODO: how to save textobject?
        {
            //var success = _textService.AddText(text);
            bool success = _textChangesService.AddTextChanges(text);
            if (success)
            {
                TextChanges newText = await _textChangesService.GetTextChangeAsync(text.TextObject.TextID);
                return Created("Modules", newText);
            }
            return BadRequest();
        }

        // PUT api/Texts/UpdateText/5
        [HttpPut("UpdateText/{id}")]
        public ActionResult Put(int id, [FromBody] Text text)
        {
            if (id != text.TextID)
            {
                return BadRequest();
            }

            var success = _textChangesService.UpdateTextChanges(text);
            if (success)
            {
                return Ok(text);
            }
            return NotFound();
        }

        // DELETE api/Texts/DeleteText/5
        [HttpDelete("DeleteText/{id}")]
        public ActionResult Delete(int id)
        {
            var success = _textChangesService.DeleteTextChanges(id);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
