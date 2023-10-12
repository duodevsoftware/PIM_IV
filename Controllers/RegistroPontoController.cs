using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM_IV.Infra;
using PIM_IV.Models;

namespace PIM_IV.Controllers
{
    [Route("api/registro_ponto")]
    [ApiController]
    public class RegistroPontoController : ControllerBase
    {
        private readonly ConnectionContext _context;

        public RegistroPontoController(ConnectionContext context)
        {
            _context = context;
        }

        // GET: api/RegistroPonto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroPontoModel>>> GetRegistroPontoModel()
        {
          if (_context.RegistroPontoModel == null)
          {
              return NotFound();
          }
            return await _context.RegistroPontoModel.ToListAsync();
        }

        // GET: api/RegistroPonto/5
        [HttpGet("{cpf}")]
        public async Task<ActionResult<List<RegistroPontoModel>>> GetRegistroPontoModel(string cpf)
        {
          if (_context.RegistroPontoModel == null)
          {
              return NotFound();
          }
            var registroPontoModel = await _context.RegistroPontoModel.Where(p => p.cpf_registro_ponto == cpf).ToListAsync();

            if (registroPontoModel == null)
            {
                return NotFound();
            }

            return registroPontoModel;
        }

        [HttpPost("bater-ponto")]
        public async Task<IActionResult> BaterPonto(string cpf)
        {
            var horaAtual = DateTime.Now.ToUniversalTime();

            var funcionario = await _context.RegistroPontoModel
                .FirstOrDefaultAsync(r => r.cpf_registro_ponto == cpf && r.data_registro == horaAtual.Date);

            if (funcionario == null)
            {
                funcionario = new RegistroPontoModel
                {
                    cpf_registro_ponto = cpf,
                    entrada = horaAtual.TimeOfDay,
                    data_registro = horaAtual.Date,
                    saida_almoco = null,
                    volta_almoco = null,
                    saida = null,
                    entrada_extra = null,
                    saida_extra = null
                };
                _context.RegistroPontoModel.Add(funcionario);
            }
            else
            {
                if (funcionario.saida_almoco == null)
                {
                    funcionario.saida_almoco = horaAtual.TimeOfDay;
                }
                else if (funcionario.volta_almoco == null)
                {
                    funcionario.volta_almoco = horaAtual.TimeOfDay;
                }
                else if (funcionario.saida == null)
                {
                    funcionario.saida = horaAtual.TimeOfDay;
                }
                else if (funcionario.entrada_extra == null)
                {
                    funcionario.entrada_extra = horaAtual.TimeOfDay;
                }
                else if (funcionario.saida_extra == null)
                {
                    funcionario.saida_extra = horaAtual.TimeOfDay;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // DELETE: api/RegistroPonto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistroPontoModel(int id)
        {
            if (_context.RegistroPontoModel == null)
            {
                return NotFound();
            }
            var registroPontoModel = await _context.RegistroPontoModel.FindAsync(id);
            if (registroPontoModel == null)
            {
                return NotFound();
            }

            _context.RegistroPontoModel.Remove(registroPontoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistroPontoModelExists(int id)
        {
            return (_context.RegistroPontoModel?.Any(e => e.id_ponto == id)).GetValueOrDefault();
        }
    }
}
