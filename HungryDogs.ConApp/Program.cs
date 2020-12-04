using System;
using System.Threading.Tasks;

namespace HungryDogs.ConApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("HungryDogs");

            await CreateModelAsync();
        }

        static async Task CreateModelAsync()
        {
            var restCtrl = Logic.Factory.CreateRestaurant();

            for (int i = 0; i < 5; i++)
            {
                var model = await restCtrl.CreateAsync();

                model.Name = $"Name{i + 1}";
                model.OwnerName = $"OwnerName{i + 1}";
                model.UniqueName = $"UniqueName{i + 1}";
                model.Email = $"name{i + 1}@gmail.com";
                model.State = Contracts.Modules.Common.RestaurantState.Active;

                await restCtrl.InsertAsync(model);
            }
            await restCtrl.SaveChangesAsync();

        }
    }
}
