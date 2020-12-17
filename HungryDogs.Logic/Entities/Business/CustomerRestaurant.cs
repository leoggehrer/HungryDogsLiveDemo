using HungryDogs.Contracts.Business;
using HungryDogs.Contracts.Modules.Common;
using HungryDogs.Logic.Time;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HungryDogs.Logic.Entities.Business
{
    class CustomerRestaurant : IdentityEntity, ICustomerRestaurant
    {
        public DateTime? NextOpen { get; set; }
        public DateTime? NextClose { get; set; }
        public SpecialOpenState OpenState { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string UniqueName { get; set; }
        public string Email { get; set; }
        public RestaurantState State { get; set; }

        public Task<ICustomerRestaurant> SetOpenStateAsync(SpecialOpenState openState, int value)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<FromToTime> TimeTable { get; set; } 
    }
}
