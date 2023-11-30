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
    [Route("api/pagamentos")]
    [ApiController]
    public class FolhaDePagamentoController : ControllerBase
    {
        private readonly ConnectionContext _context;

        public FolhaDePagamentoController(ConnectionContext context)
        {
            _context = context;
        }

        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<IEnumerable<FolhaDePagamentoModel>>> BuscarTodas()
        {
          if (_context.FolhaDePagamentoModel == null)
          {
              return NotFound();
          }
            return await _context.FolhaDePagamentoModel.ToListAsync();
        }

        /*[HttpGet("{cpf}")]
        public async Task<ActionResult<FolhaDePagamentoModel>> BuscarPorCpf (string cpf)
        {
            if(_context.FolhaDePagamentoModel == null)
            {
                return NotFound();
            }

            var folhaDePagamentoModel = await _context.FolhaDePagamentoModel.Where(fp => fp.cpf_folha_pagamento == cpf).ToListAsync();

            if (folhaDePagamentoModel == null)
            {
                return NotFound();
            }

            return Ok(folhaDePagamentoModel);

        }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<FolhaDePagamentoModel>> FolhaDePagamentoIndividual(int id)
        {
          if (_context.FolhaDePagamentoModel == null)
          {
              return NotFound();
          }
            var folhaDePagamentoModel = await _context.FolhaDePagamentoModel.FindAsync(id);

            if (folhaDePagamentoModel == null)
            {
                return NotFound();
            }

            return folhaDePagamentoModel;
        }

        [HttpPost("CalcularFolha")]
        public async Task<ActionResult<FolhaDePagamentoModel>> CalcularFolhaDePagamento(string cpf, DateTime referencia, DateTime pagamento)
        {
            FolhaDePagamentoModel folhaDePagamento = new FolhaDePagamentoModel();
            decimal _inss;
            decimal _irpf;
            bool _convenioMedico;
            bool _convenioOdontologico;
            decimal _valor_descontos;
            decimal _salario;
            decimal valorConvenioMedico;
            decimal valorConvenioOdontologico;
            decimal _salarioLiquido;

            var dadosFuncionario = _context.RecursosHumanosModel
                .Where(rh => rh.cpf_rh == cpf)
                .Select(rh => new
                {
                    Salario = rh.salario,
                    ConvenioMedico = rh.convenio_medico,
                    ConvenioOdontologico = rh.convenio_odontologico
                })
                .FirstOrDefault();

            if (dadosFuncionario != null)
            {
                _salario = dadosFuncionario.Salario;
                _convenioMedico = dadosFuncionario.ConvenioMedico;
                _convenioOdontologico = dadosFuncionario.ConvenioOdontologico;
            }
            else
            {   
                return NoContent();
            }

            if (_salario <= 1320)
            {
                _inss = (decimal)Math.Round(_salario * 0.075m, 2);
            }
            else if (_salario <= 2571.29m) 
            {
                _inss = (decimal)Math.Round(_salario * 0.09m, 2);
            }
            else if (_salario <= 3856.94m)
            {
                _inss = (decimal)Math.Round(_salario * 0.12m, 2);
            }
            else if (_salario <= 7507.49m)
            {
                _inss = (decimal)Math.Round(_salario * 0.14m, 2);
            }
            else
            {
                _inss = (decimal)Math.Round(7507.49 * 0.14, 2);
            }

            if (_salario <= 2121.01m)
            {
                _irpf = 0;
            }
            else if (_salario <= 2826.65m)
            {
                _irpf = (decimal)Math.Round((_salario - 2121.01m) * 0.075m, 2);
            }
            else if (_salario <= 3751.05m)
            {
                _irpf = (decimal)Math.Round((_salario - 2826.66m) * 0.15m + 169.58m, 2);
            }
            else if (_salario <= 4664.68m)
            {
                _irpf = (decimal)Math.Round((_salario - 3751.06m) * 0.225m + 368.68m, 2);
            }
            else
            {
                _irpf = (decimal)Math.Round((_salario - 4664.68m) * 0.275m + 643.68m, 2);
            }

            if (_convenioMedico == true)
            {
                valorConvenioMedico = 80m;
            }
            else
            {
                valorConvenioMedico = 0m;
            }

            if (_convenioOdontologico == true)
            {
                valorConvenioOdontologico = 40;
            }
            else
            {
                valorConvenioOdontologico = 0;
            }

            _valor_descontos = _inss + _irpf + valorConvenioMedico + valorConvenioOdontologico;
            _salarioLiquido = _salario - _valor_descontos;


            // Preenchendo o objeto
            try
            {

                folhaDePagamento.cpf_folha_pagamento = cpf;
                folhaDePagamento.data_referencia = referencia;
                folhaDePagamento.data_pagamento = pagamento;
                folhaDePagamento.valor_descontos = _valor_descontos;
                folhaDePagamento.salario_bruto = _salario;
                folhaDePagamento.salario_liquido = _salarioLiquido;
                folhaDePagamento.inss = _inss;
                folhaDePagamento.irpf = _irpf;
                folhaDePagamento.convenio_medico = valorConvenioMedico;
                folhaDePagamento.convenio_odontologico = valorConvenioOdontologico;

            } catch (Exception ex)
            {
                return BadRequest(ex);
            }

            if (_context.FolhaDePagamentoModel == null)
          {
              return Problem("Entity set 'ConnectionContext.FolhaDePagamentoModel'  is null.");
          }
            _context.FolhaDePagamentoModel.Add(folhaDePagamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("FolhaDePagamentoIndividual", new { id = folhaDePagamento.id_pagamento }, folhaDePagamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarFolhaDePagamento(int id)
        {
            if (_context.FolhaDePagamentoModel == null)
            {
                return NotFound();
            }
            var folhaDePagamentoModel = await _context.FolhaDePagamentoModel.FindAsync(id);
            if (folhaDePagamentoModel == null)
            {
                return NotFound();
            }

            _context.FolhaDePagamentoModel.Remove(folhaDePagamentoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
