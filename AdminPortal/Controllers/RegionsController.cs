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
            // get data from database - domain models
            var regionDomain = db.Regions.ToList();

            //Map domain models to Dto
            var regionDto = new List<RegionDto>();
                foreach (var region in regionDomain)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,

                });

            }
                //return Dto back to client
            return Ok(regionDto);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id) 
        {
            // var regions = db.Regions.Find(id);
            var regionDomain = db.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();

            }

            var regionDto = new RegionDto
            {
                Id=regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,

            };
             
            return Ok(regionDto);


        }

        [HttpPost]
        public IActionResult AddRegion(ElRegionDto dto)
        {
            var region = new Region()
            {
                Code = dto.Code,
                Name = dto.Name,
                RegionImageUrl = dto.RegionImageUrl,
            };
            db.Regions.Add(region);
            db.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            };
            // هذا يرجع رابط للعميل وكذالك 201
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //[HttpPost]
        //public IActionResult AddRegion(ElRegionDto dto)
        //{
        //    var region = new Region()
        //    {
        //        Code = dto.Code,
        //        Name = dto.Name,
        //        RegionImageUrl = dto.RegionImageUrl,
        //    };
        //    db.Regions.Add(region);
        //    db.SaveChanges();

        //    return Ok(region);
        //}

    }
}
