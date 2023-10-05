﻿using System;
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
    [Route("api/dependentes")]
    [ApiController]
    public class DependentesFuncionarioController : ControllerBase
    {
        private readonly ConnectionContext _context;

        public DependentesFuncionarioController(ConnectionContext context)
        {
            _context = context;
        }

        // GET: api/DependentesFuncionario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DependentesModel>>> GetDependentesModel()
        {
          if (_context.DependentesModel == null)
          {
              return NotFound();
          }
            return await _context.DependentesModel.ToListAsync();
        }

        // GET: api/DependentesFuncionario/5
        [HttpGet("{cpf}")]
        public async Task<ActionResult<DependentesModel>> GetDependentesModel(string cpf)
        {
          if (_context.DependentesModel == null)
          {
              return NotFound();
          }
            var dependentesModel = await _context.DependentesModel.FirstOrDefaultAsync(d => d.cpf_dependentes == cpf);

            if (dependentesModel == null)
            {
                return NotFound();
            }

            return dependentesModel;
        }

        // PUT: api/DependentesFuncionario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cpf}")]
        public async Task<IActionResult> PutDependentesModel(string cpf, DependentesModel dependentesModel)
        {
            if (cpf != dependentesModel.cpf_dependentes)
            {
                return BadRequest();
            }

            _context.Entry(dependentesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DependentesModelExists(cpf))
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

        // POST: api/DependentesFuncionario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DependentesModel>> PostDependentesModel(DependentesModel dependentesModel)
        {
          if (_context.DependentesModel == null)
          {
              return Problem("Entity set 'ConnectionContext.DependentesModel'  is null.");
          }
            _context.DependentesModel.Add(dependentesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDependentesModel", new { id = dependentesModel.id_dependentes }, dependentesModel);
        }

        // DELETE: api/DependentesFuncionario/5
        [HttpDelete("{cpf}")]
        public async Task<IActionResult> DeleteDependentesModel(string cpf)
        {
            if (_context.DependentesModel == null)
            {
                return NotFound();
            }
            var dependentesModel = await _context.DependentesModel.FindAsync(cpf);
            if (dependentesModel == null)
            {
                return NotFound();
            }

            _context.DependentesModel.Remove(dependentesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DependentesModelExists(string cpf)
        {
            return (_context.DependentesModel?.Any(e => e.cpf_dependentes == cpf)).GetValueOrDefault();
        }
    }
}
