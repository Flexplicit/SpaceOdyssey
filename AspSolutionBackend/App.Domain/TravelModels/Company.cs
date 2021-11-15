using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Domain.TravelModels
{
    public class Company : DomainEntityId
    {
        [StringLength(255)]
        [Required]
        public string Name { get; set; } = null!;
    
        public Reservation? Reservation { get; set; }
        public Guid? ReservationId { get; set; }

        public List<Provider>? Providers { get; set; }
    }
}