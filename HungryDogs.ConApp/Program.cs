using System;
using System.Threading.Tasks;

namespace HungryDogs.ConApp
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("HungryDogs");

			//await CreateModelAsync();
			await GetCustomerRestaurantAsync(1);
		}

		static async Task GetCustomerRestaurantAsync(int id)
		{
			using var ctrl = Logic.Factory.CreateCustomerRestaurant();

			var model = await ctrl.GetByIdAsync(id);
		}
		static async Task CreateModelAsync()
		{
			using var restCtrl = Logic.Factory.CreateRestaurant();
			using var ohCtrl = Logic.Factory.CreateOpeningHour(restCtrl);
			using var sohCtrl = Logic.Factory.CreateSpecialOpeningHour(restCtrl);

			var model = await restCtrl.CreateAsync();

			model.Name = $"TestRestaurant";
			model.OwnerName = $"Herbert";
			model.UniqueName = $"HerbertUnique";
			model.Email = $"herbert@gmail.com";
			model.State = Contracts.Modules.Common.RestaurantState.Active;

			var rr = await restCtrl.InsertAsync(model);
			await restCtrl.SaveChangesAsync();

			var oh = await ohCtrl.CreateAsync();

			oh.RestaurantId = rr.Id;
			oh.OpenFrom = new TimeSpan(10, 00, 00);
			oh.OpenTo = new TimeSpan(22, 00, 00);
			oh.Weekday = (int)DayOfWeek.Monday;

			await ohCtrl.InsertAsync(oh);

			oh = await ohCtrl.CreateAsync();

			oh.RestaurantId = rr.Id;
			oh.OpenFrom = new TimeSpan(10, 00, 00);
			oh.OpenTo = new TimeSpan(22, 00, 00);
			oh.Weekday = (int)DayOfWeek.Friday;

			await ohCtrl.InsertAsync(oh);
			await ohCtrl.SaveChangesAsync();

		}
	}
}
