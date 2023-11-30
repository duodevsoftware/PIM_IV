using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet]
        public async Task<ActionResult<List<RegistroPontoModel>>> GetRegistroPontoModel()
        {
          if (_context.RegistroPontoModel == null)
          {
              return NotFound();
          }
            return await _context.RegistroPontoModel.ToListAsync();
        }

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
        public async Task<ActionResult<RegistroPontoModel>> BaterPonto(string cpf)
        {
            var horaAtualBrasilia = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "E. South America Standard Time");
            var dataAtualBrasilia = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "E. South America Standard Time").Date;

            var registroPontoModel = await _context.RegistroPontoModel
                .FirstOrDefaultAsync(r => r.cpf_registro_ponto == cpf && r.data_registro == dataAtualBrasilia.Date);
            
            if (registroPontoModel == null)
            {
                registroPontoModel = new RegistroPontoModel
                {
                    cpf_registro_ponto = cpf,
                    entrada = horaAtualBrasilia,
                    data_registro = horaAtualBrasilia,
                    saida_almoco = null,
                    volta_almoco = null,
                    saida = null,
                    entrada_extra = null,
                    saida_extra = null
                };
                _context.RegistroPontoModel.Add(registroPontoModel);
            }
            else
            {
                if (registroPontoModel.saida_almoco == null)
                {
                    registroPontoModel.saida_almoco = horaAtualBrasilia;
                }
                else if (registroPontoModel.volta_almoco == null)
                {
                    registroPontoModel.volta_almoco = horaAtualBrasilia;
                }
                else if (registroPontoModel.saida == null)
                {
                    registroPontoModel.saida = horaAtualBrasilia;
                }
                else if (registroPontoModel.entrada_extra == null)
                {
                    registroPontoModel.entrada_extra = horaAtualBrasilia;
                }
                else if (registroPontoModel.saida_extra == null)
                {
                    registroPontoModel.saida_extra = horaAtualBrasilia;
                }
                else
                {
                    return BadRequest("Registros excedidos no dia");
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

            return CreatedAtAction("BaterPonto", new { id = registroPontoModel.id_ponto }, new
            {
                id = registroPontoModel.id_ponto,
                dataAtual = dataAtualBrasilia
            });
        }

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
