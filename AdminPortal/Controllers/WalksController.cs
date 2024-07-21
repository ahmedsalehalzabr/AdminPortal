using AdminPortal.CustomActionFilters;
using AdminPortal.Models.DTOs;
using AdminPortal.Models.Entities;
using AdminPortal.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepositery walkRepositery;
        public WalksController(IMapper mapper , IWalkRepositery walkRepositery)
        {
            this.mapper = mapper;
            this.walkRepositery = walkRepositery;
        }


        // Get: /api/walks?filterOn=Name&filterQuery=Track
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            var walkDomainModel = await walkRepositery.GetAllAsync(filterOn, filterQuery);
            var walkDto = mapper.Map<List<WalkDto>>(walkDomainModel);
            return Ok(walkDto);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepositery.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
           
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] ElWalkDto dto)
        {
            var walkDomainModel = mapper.Map<Walk>(dto);

            walkDomainModel = await walkRepositery.CreateAsync(walkDomainModel);

            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ElWalkDto dto)
        {

            var walkDomainModel = mapper.Map < Walk>(dto);
            // Check if region exists
            walkDomainModel = await walkRepositery.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }


            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepositery.DeleteAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }

       
            
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }
    }
}
