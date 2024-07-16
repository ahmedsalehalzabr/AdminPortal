using AdminPortal.CustomActionFilters;
using AdminPortal.Data;
using AdminPortal.Models.DTOs;
using AdminPortal.Models.Entities;
using AdminPortal.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        // للحصول على id Guid نضغط على interactive ونكتب Guid.NewGuid()
        
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // get data from database - domain models
            var regionDomain = await regionRepository.GetAllAsync();

            //Map domain models to Dto
            //var regionDto = new List<RegionDto>();
            //foreach (var region in regionDomain)
            //{
            //    regionDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl,

            //    });

            //}
            // هذا الصدر بدل الي فوقه
            var regionDto = mapper.Map<List<RegionDto>>(regionDomain);
            //return Dto back to client
            return Ok(regionDto);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // var regions = db.Regions.Find(id);
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();

            }

            //var regionDto = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl,

            //};
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);


        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddRegion([FromBody] ElRegionDto dto)
        {
            //var region = new Region
            //{
            //    Code = dto.Code,
            //    Name = dto.Name,
            //    RegionImageUrl = dto.RegionImageUrl,
            //};

            var region = mapper.Map<Region>(dto);

            region = await regionRepository.CreateAsync(region);

            //var regionDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl,
            //};
            var regionDto = mapper.Map<RegionDto>(region);
            // هذا يرجع رابط للعميل وكذالك 201
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

    
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ElRegionDto dto)
        {
            //var regionDomainModel = new Region
            //{
            //    Code = dto.Code,
            //    Name = dto.Name,
            //    RegionImageUrl = dto.RegionImageUrl,
            //};
            var regionDomainModel = mapper.Map<Region>(dto);
            // Check if region exists
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if(regionDomainModel == null)
            {
                return NotFound();
            }


            // convert domain model to Dto
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,
            //};
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //return delete region back
            // map domain model to Dto
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,
            //};
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }

    }
}
