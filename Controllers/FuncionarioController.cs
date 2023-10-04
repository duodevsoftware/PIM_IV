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
    public class FuncionarioController : ControllerBase
    {
        private readonly ConnectionContext _context;

        public FuncionarioController(ConnectionContext context)
        {
            _context = context;
        }

        // GET: api/Funcionario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuncionarioModel>>> GetFuncionarioModel()
        {
          if (_context.FuncionarioModel == null)
          {
              return NotFound();
          }
            return await _context.FuncionarioModel.ToListAsync();
        }

        // GET: api/Funcionario/5
        [HttpGet("{cpf}")]
        public async Task<ActionResult<FuncionarioModel>> GetFuncionarioModel(string cpf)
        {
          if (_context.FuncionarioModel == null)
          {
              return NotFound();
          }
            var funcionarioModel = await _context.FuncionarioModel.FirstOrDefaultAsync(f => f.cpf_funcionario == cpf);

            if (funcionarioModel == null)
            {
                return NotFound();
            }

            return funcionarioModel;
        }

        // PUT: api/Funcionario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cpf}")]
        public async Task<IActionResult> PutFuncionarioModel(string cpf, FuncionarioModel funcionarioModel)
        {
            if (cpf != funcionarioModel.cpf_funcionario)
            {
                return BadRequest();
            }

            //var funcionarioExistente = await _context.FuncionarioModel.FirstOrDefaultAsync(f => f.cpf_funcionario == cpf);

            //if (funcionarioExistente == null)
            //{
            //    return NotFound("Funcionário não encontrado.");
            //}

            if (funcionarioModel.data_nascimento_funcionario.HasValue)
            {
                funcionarioModel.data_nascimento_funcionario = funcionarioModel.data_nascimento_funcionario.Value.Date.ToUniversalTime();
            }

            _context.Entry(funcionarioModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioModelExists(cpf))
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

        // POST: api/Funcionario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FuncionarioModel>> PostFuncionarioModel(FuncionarioModel funcionarioModel)
        {
            if (_context.FuncionarioModel == null)
            {
                return Problem("Entity set 'ConnectionContext.FuncionarioModel' is null.");
            }

            if (funcionarioModel.data_nascimento_funcionario.HasValue)
            {
                DateTime dataNascimento = funcionarioModel.data_nascimento_funcionario.Value;
                int idade = DateTime.Now.Year - dataNascimento.Year;

                if (DateTime.Now < dataNascimento.AddYears(idade))
                {
                    idade--;
                }

                funcionarioModel.idade_funcionario = idade;
            }

            _context.FuncionarioModel.Add(funcionarioModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFuncionarioModel", new { id = funcionarioModel.id_funcionario }, funcionarioModel);
        }


        // DELETE: api/Funcionario/5
        [HttpDelete("{cpf}")]
        public async Task<IActionResult> DeleteFuncionarioModel(string cpf)
        {
            var funcionarioModel = await _context.FuncionarioModel.FirstOrDefaultAsync(f => f.cpf_funcionario == cpf);

            if (funcionarioModel == null)
            {
                return NotFound("Funcionário não encontrado.");
            }

            _context.FuncionarioModel.Remove(funcionarioModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool FuncionarioModelExists(string cpf)
        {
            return (_context.FuncionarioModel?.Any(e => e.cpf_funcionario == cpf)).GetValueOrDefault();
        }
    }
}
