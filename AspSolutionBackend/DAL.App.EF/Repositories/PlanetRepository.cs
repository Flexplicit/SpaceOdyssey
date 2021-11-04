using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.TravelModels;
using App.Domain.TravelModels.Enums;
using Contracts.DAL.APP.Repositories;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PlanetRepository : BaseRepository<Planet, Guid, AppDbContext>, IPlanetRepository
    {
        public PlanetRepository(AppDbContext ctx) : base(ctx)
        {
        }

        public IEnumerable<string> GetPlanetNames()
            => Enum.GetNames<EPlanet>().ToList();
    }
}