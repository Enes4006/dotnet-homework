using Microsoft.AspNetCore.Mvc;
using OdevWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace OdevWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // In-memory store for demo purposes
        private static readonly List<ProductDto> _store = new List<ProductDto>()
{
        // Yerli gururumuz Togg
        new ProductDto { Id = 1, Name = "Togg T10X V2 Uzun Menzil", Price = 1823000M },
    
        // Popüler SUV modelleri
        new ProductDto { Id = 2, Name = "Tesla Model Y Long Range", Price = 3200000M },
        new ProductDto { Id = 3, Name = "BYD Atto 3", Price = 1690000M },
        new ProductDto { Id = 4, Name = "Skywell ET5", Price = 1880000M },
    
        // Lüks ve Spor segment
        new ProductDto { Id = 5, Name = "Porsche Taycan 4S", Price = 9500000M },
        new ProductDto { Id = 6, Name = "Mercedes-Benz EQS 580", Price = 8200000M },
    
        // Þehir içi kompakt modeller
        new ProductDto { Id = 7, Name = "Renault Zoe", Price = 1350000M },
        new ProductDto { Id = 8, Name = "Opel Corsa-e", Price = 1450000M }
};

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll() => Ok(_store);

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            var p = _store.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPost]
        public ActionResult<ProductDto> Create([FromBody] ProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            dto.Id = _store.Any() ? _store.Max(x => x.Id) + 1 : 1;
            _store.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var p = _store.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();
            _store.Remove(p);
            return NoContent();
        }
    }
}
