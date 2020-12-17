using HungryDogs.Contracts.Modules.Common;
using System;
using System.Threading.Tasks;

namespace HungryDogs.Contracts.Business
{
    public interface ICustomerRestaurant : IIdentifiable, ICommonRestaurant
	{
		DateTime? NextOpen { get; set; }
		DateTime? NextClose { get; set; }
		SpecialOpenState OpenState { get; set; }
		Task<ICustomerRestaurant> SetOpenStateAsync(SpecialOpenState openState, int value);
    }
}
