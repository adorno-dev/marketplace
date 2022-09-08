using Marketplace.API.Data.Contracts;
using Marketplace.API.Models;

namespace Marketplace.API.Data.Extensions
{
    public static class DatabaseInitializerExtension
    {
        #pragma warning disable CS8604
        public static void DatabaseInitialize(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope()) {
                var provider = scope.ServiceProvider.GetService<IDatabaseInitializer>();
                if (provider is not null) {
                    provider.InitializeSeedUsersAuthentication(out User? user);
                    provider.InitializeSeedUserStores(user, out Store? store);
                    provider.InitializeSeedStoreProducts(store, out IList<Product> products);
                }
            }
        }
    }
}