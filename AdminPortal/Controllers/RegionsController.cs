﻿using AdminPortal.Data;
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
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,

            };

            return Ok(regionDto);


        }

        [HttpPost]
        public IActionResult AddRegion([FromBody] ElRegionDto dto)
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

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] ElRegionDto dto)
        {
            // Check if region exists
            var regionDomainModel = db.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            regionDomainModel.Code = dto.Code;
            regionDomainModel.Name = dto.Name;
            regionDomainModel.RegionImageUrl = dto.RegionImageUrl;

            db.SaveChanges();

            // convert domain model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel = db.Regions.FirstOrDefault(x =>x.Id == id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
            db.Regions.Remove(regionDomainModel);
            db.SaveChanges();

            //return delete region back
            // map domain model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }

    }
}
