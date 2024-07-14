using AdminPortal.Data;
using AdminPortal.Models.DTOs;
using AdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly AppDbContext db;

        public RegionsController(AppDbContext db)
        {
            this.db = db;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var region = db.Regions.ToList();
            return Ok(region);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id) 
        {
            // var regions = db.Regions.Find(id);
            var regions = db.Regions.FirstOrDefault(x => x.Id == id);
            if (regions == null)
            {
                return NotFound();

            }

            return Ok(regions);


        }

        [HttpPost]
        public IActionResult AddRegion(AddRegionDto dto)
        {
            var region = new Region()
            {
                Code = dto.Code,
                Name = dto.Name,
                RegionImageUrl = dto.RegionImageUrl,
            };
            db.Regions.Add(region);
            db.SaveChanges();
            return Ok(region);
        }
    }
}
