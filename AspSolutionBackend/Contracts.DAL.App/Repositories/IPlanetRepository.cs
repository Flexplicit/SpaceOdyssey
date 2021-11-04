using System;
using System.Collections.Generic;
using App.Domain.TravelModels;
using App.Domain.TravelModels.Enums;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.APP.Repositories
{
    public interface IPlanetRepository : IBaseRepository<Planet, Guid>
    {
        IEnumerable<string> GetPlanetNames();

    }
}