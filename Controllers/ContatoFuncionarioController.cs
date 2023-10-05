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
    [Route("api/contatos")]
    [ApiController]
    public class ContatoFuncionarioController : ControllerBase
    {
        private readonly ConnectionContext _context;

        public ContatoFuncionarioController(ConnectionContext context)
        {
            _context = context;
        }

        // GET: api/ContatoFuncionario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContatoModel>>> GetContatoModel()
        {
          if (_context.ContatoModel == null)
          {
              return NotFound();
          }
            return await _context.ContatoModel.ToListAsync();
        }

        // GET: api/ContatoFuncionario/5
        [HttpGet("{cpf}")]
        public async Task<ActionResult<ContatoModel>> GetContatoModel(string cpf)
        {
          if (_context.ContatoModel == null)
          {
              return NotFound();
          }
            var contatoModel = await _context.ContatoModel.FirstOrDefaultAsync(c => c.cpf_contato == cpf);

            if (contatoModel == null)
            {
                return NotFound();
            }

            return contatoModel;
        }

        // PUT: api/ContatoFuncionario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cpf}")]
        public async Task<IActionResult> PutContatoModel(string cpf, ContatoModel contatoModel)
        {
            if (cpf != contatoModel.cpf_contato)
            {
                return BadRequest();
            }

            _context.Entry(contatoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContatoModelExists(cpf))
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

        // POST: api/ContatoFuncionario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContatoModel>> PostContatoModel(ContatoModel contatoModel)
        {
          if (_context.ContatoModel == null)
          {
              return Problem("Entity set 'ConnectionContext.ContatoModel'  is null.");
          }
            _context.ContatoModel.Add(contatoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContatoModel", new { id = contatoModel.id_contato }, contatoModel);
        }

        // DELETE: api/ContatoFuncionario/5
        [HttpDelete("{cpf}")]
        public async Task<IActionResult> DeleteContatoModel(string cpf)
        {
            if (_context.ContatoModel == null)
            {
                return NotFound();
            }
            var contatoModel = await _context.ContatoModel.FindAsync(cpf);
            if (contatoModel == null)
            {
                return NotFound();
            }

            _context.ContatoModel.Remove(contatoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContatoModelExists(string cpf)
        {
            return (_context.ContatoModel?.Any(e => e.cpf_contato == cpf)).GetValueOrDefault();
        }
    }
}
