using HungryDogs.Contracts.Modules.Common;

namespace HungryDogs.Contracts.Persistence
{
    public interface IRestaurant : IVersionable
    {
        string Name { get; set; }
        string OwnerName { get; set; }
        string UniqueName { get; set; }
        string Email { get; set; }
        RestaurantState State { get; set; }
    }
}
