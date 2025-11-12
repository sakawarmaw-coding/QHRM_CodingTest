using QHRM_CodingTest.Model;

namespace QHRM_CodingTest.Data
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetAllProduct();
        Task<ProductModel?> GetProductById(int id);
        Task<ResponseModel> CreateProduct(ProductModel model);
        Task<ResponseModel> UpdateProduct(ProductModel model);
        Task<ResponseModel> DeleteProduct(int id);
    }
}
