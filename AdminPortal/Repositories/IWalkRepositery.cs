﻿using AdminPortal.Models.Entities;

namespace AdminPortal.Repositories
{
    public interface IWalkRepositery
    {
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> CreateAsync(Walk walk);

        Task<Walk?> UpdateAsync(Guid id, Walk walk);

        Task<Walk?> DeleteAsync(Guid id);
    }
}
