using AdminPortal.Data;
using AdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminPortal.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly AppDbContext db;

        public RegionRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<Region?> CreateAsync(Region region)
        {
             await db.Regions.AddAsync(region);
             await db.SaveChangesAsync();
             return region;

        }


        public async Task<List<Region>> GetAllAsync()
        {
            return await db.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await db.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await db.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await db.SaveChangesAsync();
            return existingRegion;
        }


        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await db.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            db.Regions.Remove(existingRegion);
            await db.SaveChangesAsync();
            return existingRegion;
        }
    }
}
