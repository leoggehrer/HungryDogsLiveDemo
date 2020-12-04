using HungryDogs.Contracts.Modules.Common;
using System;

namespace HungryDogs.Logic.Entities.Persistence
{
    internal class SpecialOpeningHour : VersionEntity, HungryDogs.Contracts.Persistence.ISpecialOpeningHour
    {
        public int RestaurantId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Notes { get; set; }
        public SpecialOpenState State { get; set; }
        // Navigation
        public Restaurant Restaurant { get; set; }
    }
}
