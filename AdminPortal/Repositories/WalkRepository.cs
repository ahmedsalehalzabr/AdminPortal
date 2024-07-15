﻿using AdminPortal.Data;
using AdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminPortal.Repositories
{
    public class WalkRepository : IWalkRepositery
    {
        private readonly AppDbContext appDbContext;

        public WalkRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await appDbContext.Walks.AddAsync(walk);
            if (walk == null)
            {
                return null;
            }
            await appDbContext.SaveChangesAsync();
            return walk;
            
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await appDbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
           return await appDbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await appDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
          
            
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.Description = walk.Description;
            existingWalk.Name = walk.Name;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.LengthInKm = walk.LengthInKm;

            await appDbContext.SaveChangesAsync();
            return existingWalk;
        }


        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await appDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            appDbContext.Walks.Remove(existingWalk);
            await appDbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}