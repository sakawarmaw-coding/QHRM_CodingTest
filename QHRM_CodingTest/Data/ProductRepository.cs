using Dapper;
using Microsoft.Data.SqlClient;
using QHRM_CodingTest.Model;
using System.Data;
using System.Data.Common;

namespace QHRM_CodingTest.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly string  _connectionString;
        ResponseModel repModel = new ResponseModel();
        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<ProductModel>> GetAllProduct()
        {
            using IDbConnection conn = new SqlConnection(_connectionString);
            string query = "select ROW_NUMBER() OVER(ORDER BY Id desc) AS RowNumber,* from TblProduct with(nolock) order by Id desc";
            return await conn.QueryAsync<ProductModel>(query);
        }

        public async Task<ResponseModel> CreateProduct(ProductModel model)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);
            string query = @"INSERT INTO [dbo].[TblProduct]
                                               ([Name]
                                               ,[Description]
                                               ,[Price]
                                               ,[CreatedDate])
                                         VALUES
                                               (@Name
                                               ,@Description
                                               ,@Price
                                               ,GETDATE())";
            var result = await conn.ExecuteScalarAsync<int>(query, model);
            repModel.RespCode = result > 0 ? "success" : "failed";
            repModel.RespMsg = result > 0 ? "Saving Success" : "Save failed";
            return repModel;
        }

        public async Task<ProductModel?> GetProductById(int id)
        {
            string query = "select * from TblProduct where Id = @Id";
            using IDbConnection conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<ProductModel>(query, new
            {
                Id =id
            });
        }

        public async Task<ResponseModel> UpdateProduct(ProductModel model)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);
            string query = @"UPDATE [dbo].[TblProduct]
                                       SET [Name] = @Name
                                          ,[Description] = @Description
                                          ,[Price] = @Price
                                          ,[ModifiedDate] = GETDATE()
                                     WHERE Id=@Id";

            int result = await conn.ExecuteScalarAsync<int>(query, model);
            repModel.RespCode = result > 0 ? "success" : "failed";
            repModel.RespMsg = result > 0 ? "Updating Success" : "Updated failed";
            return repModel;
        }

        private ProductModel? FindById(int id)
        {
            string query = "select * from TblProduct where Id = @Id";
            using IDbConnection db = new SqlConnection(_connectionString);
            var item = db.Query<ProductModel>(query, new ProductModel { Id = id }).FirstOrDefault();
            return item;
        }

        public async Task<ResponseModel> DeleteProduct(int id)
        {
            try
            {
                var item = FindById(id);
                if (item is null)
                {
                    repModel.RespCode = "error";
                    repModel.RespMsg = "Data Not Found";
                    return repModel;
                }

                string query = @"Delete From TblProduct where Id = @Id";
                using IDbConnection conn = new SqlConnection(_connectionString);
                int result = await conn.ExecuteAsync(query, new ProductModel { Id = id });
                repModel.RespCode = result > 0 ? "success" : "failed";
                repModel.RespMsg = result > 0 ? "Deleting Success" : "Delete failed";
            }
            catch (Exception ex)
            {
                repModel.RespCode = "error";
                repModel.RespMsg = ex.Message;
            }

            return repModel;
        }
    }
}
