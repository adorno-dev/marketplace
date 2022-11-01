namespace Marketplace.API.UnitOfWorks.Contracts
{
    public interface IOrderUnitOfWork
    {
        Task Commit();
        Task Rollback();

        Task<bool> PlaceOrder(Guid userId);
    }
}