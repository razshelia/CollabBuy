namespace CollabBuy.CollabBuyApp.Repositories.Interfaces
{
    /// <summary>
    /// Kontrak untuk repository yang mendukung soft delete (is_deleted = TRUE).
    /// Implement di: ProductRepository, PreOrderRepository, CategoryRepository.
    /// </summary>
    public interface ISoftDeletable
    {
        void SoftDelete(int id);
    }
}
