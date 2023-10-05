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
        [HttpGet("{cpf}")]
        public async Task<ActionResult<EnderecoModel>> GetEnderecoModel(string cpf)
        {
          if (_context.EnderecoModel == null)
          {
              return NotFound();
          }
            var enderecoModel = await _context.EnderecoModel.FirstOrDefaultAsync(e => e.cpf_endereco == cpf);

            if (enderecoModel == null)
            {
                return NotFound();
            }

            return enderecoModel;
        }

        // PUT: api/EnderecoFuncionario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cpf}")]
        public async Task<IActionResult> PutEnderecoModel(string cpf, EnderecoModel enderecoModel)
        {
            if (cpf != enderecoModel.cpf_endereco)
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
                if (!EnderecoModelExists(cpf))
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
        [HttpDelete("{cpf}")]
        public async Task<IActionResult> DeleteEnderecoModel(string cpf)
        {
            if (_context.EnderecoModel == null)
            {
                return NotFound();
            }
            var enderecoModel = await _context.EnderecoModel.FindAsync(cpf);
            if (enderecoModel == null)
            {
                return NotFound();
            }

            _context.EnderecoModel.Remove(enderecoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnderecoModelExists(string cpf)
        {
            return (_context.EnderecoModel?.Any(e => e.cpf_endereco == cpf)).GetValueOrDefault();
        }
    }
}
