using Marketplace.API.Models;

namespace Marketplace.API.Data.Contracts
{
    public interface IDatabaseInitializer
    {
        void InitializeSeedUsersAuthentication(out User? user);
        void InitializeSeedUserStores(User user, out Store? store);
        void InitializeSeedStoreProducts(Store store, out IList<Product> products);
    }
}