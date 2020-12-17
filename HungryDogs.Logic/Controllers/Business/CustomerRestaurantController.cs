using CommonBase.Extensions;
using HungryDogs.Contracts.Modules.Common;
using HungryDogs.Logic.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TContract = HungryDogs.Contracts.Business.ICustomerRestaurant;
using TEntity = HungryDogs.Logic.Entities.Business.CustomerRestaurant;

namespace HungryDogs.Logic.Controllers.Business
{
    class CustomerRestaurantController : ControllerObject, Contracts.Client.IController<TContract>
	{
		public CustomerRestaurantController() 
			: base(new ProjectDbContext())
		{
		}
		public CustomerRestaurantController(ControllerObject controllerObject)
			: base(controllerObject)
		{
		}

        protected DbSet<Entities.Persistence.Restaurant> Set => Context.RestaurauntSet;

        internal async Task<TEntity> LoadCustomerRestaurantAsync(int id)
		{
            var result = default(TEntity);
			var restaurant = await Set.Include(e => e.OpeningHours)
							         .Include(e => e.SepcialOpeningHours)
							         .SingleOrDefaultAsync(e => e.Id == id)
							         .ConfigureAwait(false);

            if (restaurant != null)
			{
                var now = DateTime.Now;
                var timeTable = Time.TimeTable.LoadTimeTable(restaurant, now);
                var fromTo = timeTable.FirstOrDefault(e => e.IsBetween(now));

                result = new TEntity();
                result.Id = restaurant.Id;
                result.Name = restaurant.Name;
                result.UniqueName = restaurant.UniqueName;
                result.OwnerName = restaurant.OwnerName;
                result.Email = restaurant.Email;
                result.State = restaurant.State;

                if (fromTo != null)
                {
                    result.OpenState = fromTo.State;
                    if ((result.OpenState & SpecialOpenState.Open) > 0)
                    {
                        result.NextClose = fromTo.To;
                    }
                    else if ((result.OpenState & SpecialOpenState.Closed) > 0)
                    {
                        var next = timeTable.FirstOrDefault(e => e.From.ToDateSecondStamp() > fromTo.To.ToDateSecondStamp()
                                                              && (e.State & SpecialOpenState.Open) > 0);

                        if (next != null)
                        {
                            result.NextOpen = next.To;
                        }
                    }
                }
                result.TimeTable = timeTable;
            }
            return result;
		}
        public async Task<TContract> SetOpenStateAsync(int id, SpecialOpenState openState, int value)
        {
            var customerRestaurant = await LoadCustomerRestaurantAsync(id).ConfigureAwait(false);

            if (customerRestaurant != null)
            {
                //var timeTable = Time.
            }
            return customerRestaurant;
        }

        public Task<int> Count()
        {
            return Set.CountAsync();
        }
        public Task<TContract> CreateAsync()
        {
            throw new NotSupportedException();
        }

        public async Task<TContract> GetByIdAsync(int id)
        {
            var model = await LoadCustomerRestaurantAsync(id).ConfigureAwait(false);

            return model;
        }
        public async Task<TContract[]> GetAllAsync()
        {
            var result = new List<TEntity>();
            var qry = await Set.ToArrayAsync().ConfigureAwait(false);

            foreach (var item in qry)
            {
                result.Add(await LoadCustomerRestaurantAsync(item.Id).ConfigureAwait(false));
            }
            return result.ToArray();
        }
        public Task<TContract> InsertAsync(TContract entity)
        {
            throw new NotSupportedException();
        }
        public Task<TContract> UpdateAsync(TContract entity)
        {
            throw new NotSupportedException();
        }
        public Task<TContract> DeleteAsync(int id)
        {
            throw new NotSupportedException();
        }

        protected virtual TEntity ConvertTo(TContract contract)
        {
            return CopyTo(new TEntity(), contract);
        }
        protected virtual TEntity CopyTo(TEntity entity, TContract contract)
        {
            entity.CheckArgument(nameof(entity));
            contract.CheckArgument(nameof(contract));

            entity.Id = contract.Id;
            entity.Name = contract.Name;
            entity.UniqueName = contract.UniqueName;
            entity.OwnerName = contract.OwnerName;
            entity.Email = contract.Email;
            entity.State = contract.State;

            entity.NextOpen = contract.NextOpen;
            entity.NextClose = contract.NextClose;
            entity.OpenState = contract.OpenState;

            return entity;
        }
    }
}
