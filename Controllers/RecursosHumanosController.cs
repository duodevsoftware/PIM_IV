using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM_IV.Infra;
using PIM_IV.Models;

namespace PIM_IV.Controllers
{
    [Route("api/recursosHumanos")]
    [ApiController]
    public class RecursosHumanosController : ControllerBase
    {
        private readonly ConnectionContext _context;

        public RecursosHumanosController(ConnectionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecursosHumanosModel>>> GetRecursosHumanosModel()
        {
          if (_context.RecursosHumanosModel == null)
          {
              return NotFound();
          }
            return await _context.RecursosHumanosModel.ToListAsync();
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<RecursosHumanosModel>> GetRecursosHumanosModel(string cpf)
        {
          if (_context.RecursosHumanosModel == null)
          {
              return NotFound();
          }
            var recursosHumanosModel = await _context.RecursosHumanosModel.FirstOrDefaultAsync(r => r.cpf_rh == cpf);

            if (recursosHumanosModel == null)
            {
                return NotFound();
            }

            return recursosHumanosModel;
        }

        [HttpGet("bemVindo")]
        public async Task<ActionResult<RecursosHumanosModel>> AcessarSistema(string login, string senha)
        {
            var usuario = await _context.RecursosHumanosModel.SingleOrDefaultAsync(u => u.login == login);
            //var senhaDigitada = await _context.RecursosHumanosModel.SingleOrDefaultAsync(s => s.senha == senha);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado");
            }
            
            if(usuario.senha != senha)
            {
                return BadRequest("Senha inválida");
            }

            if(usuario.senha == senha)
            {
                return Ok("Acesso autorizado!");
            }

            return Ok("Vai Corinthians");
        }

        [HttpPut("{cpf}")]
        public async Task<IActionResult> PutRecursosHumanosModel(string cpf, RecursosHumanosModel recursosHumanosModel)
        {
            if (cpf != recursosHumanosModel.cpf_rh)
            {
                return BadRequest();
            }

            _context.Entry(recursosHumanosModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecursosHumanosModelExists(cpf))
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

        [HttpPost]
        public async Task<ActionResult<RecursosHumanosModel>> PostRecursosHumanosModel(RecursosHumanosModel recursosHumanosModel)
        {
          if (_context.RecursosHumanosModel == null)
          {
              return Problem("Entity set 'ConnectionContext.RecursosHumanosModel'  is null.");
          }
            _context.RecursosHumanosModel.Add(recursosHumanosModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecursosHumanosModel", new { id = recursosHumanosModel.id_rh }, recursosHumanosModel);
        }
  
        [HttpPatch("{cpf}")]
        public async Task<IActionResult> AtualizarSenha(string cpf, [FromBody] TrocarSenhaModel trocarSenhaModel)
        {
            var recursosHumanoModel = await _context.RecursosHumanosModel.SingleOrDefaultAsync(r => r.cpf_rh == cpf);

            if (recursosHumanoModel == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            if (trocarSenhaModel.senhaAtual != recursosHumanoModel.senha)
            {
                return BadRequest("A senha atual não corresponde à senha armazenada.");
            }

            recursosHumanoModel.senha = trocarSenhaModel.novaSenha;

            await _context.SaveChangesAsync();

            return Ok("Senha atualizada com sucesso.");
        }

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> DeleteRecursosHumanosModel(string cpf)
        {
            if (_context.RecursosHumanosModel == null)
            {
                return NotFound();
            }
            var recursosHumanosModel = await _context.RecursosHumanosModel.FindAsync(cpf);
            if (recursosHumanosModel == null)
            {
                return NotFound();
            }

            _context.RecursosHumanosModel.Remove(recursosHumanosModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecursosHumanosModelExists(string cpf)
        {
            return (_context.RecursosHumanosModel?.Any(r => r.cpf_rh == cpf)).GetValueOrDefault();
        }
    }
}
