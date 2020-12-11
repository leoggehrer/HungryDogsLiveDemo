using CommonBase.Extensions;
using HungryDogs.Logic.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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
			var qry = await Set.Include(e => e.OpeningHours)
							   .ThenInclude(e => e.Restaurant.SepcialOpeningHours)
							   .SingleOrDefaultAsync(e => e.Id == id)
							   .ConfigureAwait(false);



			return null;
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
            return (TContract[])await Set.ToArrayAsync().ConfigureAwait(false);
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
