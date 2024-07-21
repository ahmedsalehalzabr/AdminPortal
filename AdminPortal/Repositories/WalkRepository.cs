using AdminPortal.Data;
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

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walk = appDbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();

            //Filtering 
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walk = walk.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting يرتب حسب الابجديه او الارقام 
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walk = isAscending ? walk.OrderBy(x => x.Name) : walk.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walk = isAscending ? walk.OrderBy(x => x.LengthInKm) : walk.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination تحدد كم رقم يطلع من api
            var skipResults = (pageNumber - 1) * pageSize;

            return await walk.Skip(skipResults).Take(pageSize).ToListAsync();
          //  return await appDbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).ToListAsync();
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
