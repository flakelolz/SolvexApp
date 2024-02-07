using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolvexApp.Data;
using SolvexApp.Models;
using SolvexApp.Models.DTO;

namespace SolvexApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _context;

        public CharacterController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetAllCharacters()
        {
            var characters = await _context.Characters.ToListAsync();

            return Ok(characters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        [HttpPost]
        public async Task<ActionResult<Character>> AddCharacter(CreateCharacterDto characterDto)
        {
            var character = new Character
            {
                Name = characterDto.Name,
                Profession = characterDto.Profession,
            };

            _context.Characters.Add(character);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Character>> UpdateCharacter(int id, UpdateCharacterDto characterDto)
        {
            var dbCharacter = await _context.Characters.FindAsync(id);
            if (dbCharacter == null)
            {
                return NotFound();
            }

            dbCharacter.Name = characterDto.Name;
            dbCharacter.Profession = characterDto.Profession;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Character>> DeleteCharacter(int id)
        {
            var dbCharacter = await _context.Characters.FindAsync(id);
            if (dbCharacter == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(dbCharacter);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
