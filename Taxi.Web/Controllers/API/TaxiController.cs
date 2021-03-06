﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taxi.Web.Data;
using Taxi.Web.Data.Entities;
using Taxi.Web.Helpers;

namespace Taxi.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public TaxiController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;

        }

        // GET: api/Taxi
        [HttpGet]
        public IEnumerable<TaxiEntity> GetTaxis()
        {
            return _context.Taxis;
        }

        // GET: api/Taxi/5
        [HttpGet("{plaque}")]
        public async Task<IActionResult> GetTaxiEntity([FromRoute] string plaque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            plaque = plaque.ToUpper();
            TaxiEntity taxiEntity = await _context.Taxis
               .Include(t => t.User)
               .Include(t => t.Trips)
               .ThenInclude(t => t.TripDetails)
               .Include(t => t.Trips)
               .ThenInclude(t => t.User)
               .FirstOrDefaultAsync(t => t.Plaque == plaque);


            if (taxiEntity == null)
            {
                _context.Taxis.Add(new TaxiEntity { Plaque = plaque });
                await _context.SaveChangesAsync();
                taxiEntity = await _context.Taxis.FirstOrDefaultAsync(t => t.Plaque == plaque);

            }

            return Ok(_converterHelper.ToTaxiResponse(taxiEntity));

        }
    }
}

    //    // PUT: api/Taxi/5
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutTaxiEntity([FromRoute] int id, [FromBody] TaxiEntity taxiEntity)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        if (id != taxiEntity.Id)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(taxiEntity).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!TaxiEntityExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return NoContent();
    //    }

    //    // POST: api/Taxi
    //    [HttpPost]
    //    public async Task<IActionResult> PostTaxiEntity([FromBody] TaxiEntity taxiEntity)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        _context.Taxis.Add(taxiEntity);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction("GetTaxiEntity", new { id = taxiEntity.Id }, taxiEntity);
    //    }

    //    // DELETE: api/Taxi/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteTaxiEntity([FromRoute] int id)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        var taxiEntity = await _context.Taxis.FindAsync(id);
    //        if (taxiEntity == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Taxis.Remove(taxiEntity);
    //        await _context.SaveChangesAsync();

    //        return Ok(taxiEntity);
    //    }

    //    private bool TaxiEntityExists(int id)
    //    {
    //        return _context.Taxis.Any(e => e.Id == id);
    //    }
    //}
//}