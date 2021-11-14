using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using App.Domain.TravelModels.Enums;
using Domain.Base;

namespace App.Domain.TravelModels
{
    public class Planet : DomainEntityId
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EPlanet Name { get; set; }

    }
}