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
    [Route("api/[controller]")]
    [ApiController]
    public class RecursosHumanosController : ControllerBase
    {
        private readonly ConnectionContext _context;

        public RecursosHumanosController(ConnectionContext context)
        {
            _context = context;
        }

        // GET: api/RecursosHumanos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecursosHumanosModel>>> GetRecursosHumanosModel()
        {
          if (_context.RecursosHumanosModel == null)
          {
              return NotFound();
          }
            return await _context.RecursosHumanosModel.ToListAsync();
        }

        // GET: api/RecursosHumanos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecursosHumanosModel>> GetRecursosHumanosModel(int id)
        {
          if (_context.RecursosHumanosModel == null)
          {
              return NotFound();
          }
            var recursosHumanosModel = await _context.RecursosHumanosModel.FindAsync(id);

            if (recursosHumanosModel == null)
            {
                return NotFound();
            }

            return recursosHumanosModel;
        }

        // PUT: api/RecursosHumanos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecursosHumanosModel(int id, RecursosHumanosModel recursosHumanosModel)
        {
            if (id != recursosHumanosModel.id_rh)
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
                if (!RecursosHumanosModelExists(id))
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

        // POST: api/RecursosHumanos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // DELETE: api/RecursosHumanos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecursosHumanosModel(int id)
        {
            if (_context.RecursosHumanosModel == null)
            {
                return NotFound();
            }
            var recursosHumanosModel = await _context.RecursosHumanosModel.FindAsync(id);
            if (recursosHumanosModel == null)
            {
                return NotFound();
            }

            _context.RecursosHumanosModel.Remove(recursosHumanosModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecursosHumanosModelExists(int id)
        {
            return (_context.RecursosHumanosModel?.Any(e => e.id_rh == id)).GetValueOrDefault();
        }
    }
}