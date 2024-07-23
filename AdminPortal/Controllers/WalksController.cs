using AdminPortal.CustomActionFilters;
using AdminPortal.Models.DTOs;
using AdminPortal.Models.Entities;
using AdminPortal.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        // Sort => retrive data from database عمل السورت استرجاع البيانات من قاعدة البيانات
        // Get: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name$isAscending=true&pageNumber=1&pageSize=10
        // Ascending ترتيب تصاعدي
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {

            var walkDomainModel = await walkRepositery.GetAllAsync(filterOn, filterQuery,
              sortBy, isAscending ?? true, pageNumber, pageSize);

           // throw new Exception("fuck you");
           
            return Ok(mapper.Map<List<WalkDto>>(walkDomainModel));

            //try
            //{
            //    var walkDomainModel = await walkRepositery.GetAllAsync(filterOn, filterQuery,
            //    sortBy, isAscending ?? true, pageNumber, pageSize);
            //    var walkDto = mapper.Map<List<WalkDto>>(walkDomainModel);
            //    return Ok(walkDto);
            //}
            //catch (Exception ex)
            //{
            //    return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError)
            //}


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
