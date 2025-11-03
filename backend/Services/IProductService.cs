public interface IProductService
{
    Task<IEnumerable<ProductGetDto>> GetAllProductAsync();
    Task<ProductGetDto> GetProductByIdAsync(int id);
    Task<ProductCreateDto> CreateProductAsync(ProductCreateDto productCreateDto);
    Task<ProductUpdateDto> UpdateProductAsync(ProductUpdateDto product);
    Task<int> DeleteProductAsync(int id);

}