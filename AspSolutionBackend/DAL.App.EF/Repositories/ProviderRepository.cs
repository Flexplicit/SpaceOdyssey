using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.TravelModels;
using Contracts.DAL.APP.Repositories;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ProviderRepository : BaseRepository<Provider, AppDbContext>, IProviderRepository
    {
        public ProviderRepository(AppDbContext ctx) : base(ctx)
        {
        }

        public async Task<List<Provider>> GetProviderByIdAndConfirmIfItExistsInTravelPrice(List<Guid> providerIds,
            Guid travelPricesId)
        {
            var query = CreateQuery(true);

            var queryRes = await query
                .Include(provider => provider.Legs)
                .ThenInclude(leg => leg!.TravelPrices)
                .Where(provider => provider.Legs!.TravelPrices!.Id == travelPricesId
                                   && providerIds.Contains(provider.Id))
                .ToListAsync();
            return queryRes.Count != providerIds.Count ? new List<Provider>() : queryRes;
        }
    }
}