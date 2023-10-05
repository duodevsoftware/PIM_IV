using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM_IV.Infra;
using PIM_IV.Models;

namespace PIM_IV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoFuncionarioController : ControllerBase
    {
        private readonly ConnectionContext _context;

        public EnderecoFuncionarioController(ConnectionContext context)
        {
            _context = context;
        }

        // GET: api/EnderecoFuncionario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoModel>>> GetEnderecoModel()
        {
          if (_context.EnderecoModel == null)
          {
              return NotFound();
          }
            return await _context.EnderecoModel.ToListAsync();
        }

        // GET: api/EnderecoFuncionario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnderecoModel>> GetEnderecoModel(int id)
        {
          if (_context.EnderecoModel == null)
          {
              return NotFound();
          }
            var enderecoModel = await _context.EnderecoModel.FindAsync(id);

            if (enderecoModel == null)
            {
                return NotFound();
            }

            return enderecoModel;
        }

        // PUT: api/EnderecoFuncionario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnderecoModel(int id, EnderecoModel enderecoModel)
        {
            if (id != enderecoModel.id_endereco)
            {
                return BadRequest();
            }

            _context.Entry(enderecoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnderecoModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EnderecoFuncionario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EnderecoModel>> PostEnderecoModel(EnderecoModel enderecoModel)
        {
          if (_context.EnderecoModel == null)
          {
              return Problem("Entity set 'ConnectionContext.EnderecoModel'  is null.");
          }
            _context.EnderecoModel.Add(enderecoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnderecoModel", new { id = enderecoModel.id_endereco }, enderecoModel);
        }

        // DELETE: api/EnderecoFuncionario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnderecoModel(int id)
        {
            if (_context.EnderecoModel == null)
            {
                return NotFound();
            }
            var enderecoModel = await _context.EnderecoModel.FindAsync(id);
            if (enderecoModel == null)
            {
                return NotFound();
            }

            _context.EnderecoModel.Remove(enderecoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnderecoModelExists(int id)
        {
            return (_context.EnderecoModel?.Any(e => e.id_endereco == id)).GetValueOrDefault();
        }
    }
}
