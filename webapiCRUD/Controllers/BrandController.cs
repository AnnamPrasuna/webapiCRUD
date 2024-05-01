using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using webapiCRUD.Models;

namespace webapiCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _dbcontext;
        public BrandController(BrandContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            if (_dbcontext.brands == null)
            {
                return NotFound();
            }
            return await _dbcontext.brands.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrands(int id)
        {
            if (_dbcontext.brands == null)
            {
                return NotFound();
            }
            var brand = await _dbcontext.brands.FindAsync(id);
            if(brand==null)
            {
                return NotFound();
            }
            return brand;
        }
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _dbcontext.brands.Add(brand);
            await _dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBrands),new{ id = brand.Id }, brand);
        }
        [HttpPut]
        public async Task<ActionResult<Brand>> PutBrand(int id,Brand brand)
        {
            if(id!=brand.Id)
            {
                return BadRequest();
            }
            _dbcontext.Entry(brand).State= EntityState.Modified;
            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!BrandAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();    
        }
        private bool BrandAvailable(int id)
        {
            return (_dbcontext.brands?.Any(x=>x.Id==id)).GetValueOrDefault();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            if(_dbcontext.brands==null)
            {
                return NotFound();
            }
          var brand  =await _dbcontext.brands.FindAsync(id);
            if(brand==null)
            {
                return NotFound();
            }
            _dbcontext.brands.Remove(brand);
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }

    }
}
