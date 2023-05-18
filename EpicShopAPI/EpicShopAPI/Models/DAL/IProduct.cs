namespace EpicShopAPI.Models.DAL
{
    public interface IProduct
    {
        Task<IEnumerable<ProductModel>> GetAllProductsSp();
    }
}
