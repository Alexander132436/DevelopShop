using Npgsql;

public class ProductService : IProductService
{
    private readonly DataBaseContext _context;
    public ProductService(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductGetDto>> GetAllProductAsync()
    {
        var products = new List<ProductGetDto>();
        using (var connection = _context.CreateConnection())
        {
            await connection.OpenAsync();
            var sql = @"SELECT * FROM products";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        products.Add(new ProductGetDto
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Description = reader.GetString(reader.GetOrdinal("description")),
                            Price = reader.GetDecimal(reader.GetOrdinal("price")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("image_url")),
                            StockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity"))
                        });
                    }
                }
            }
        }
        return products;
    }


    public async Task<ProductGetDto> GetProductByIdAsync(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            await connection.OpenAsync();
            var sql = @"SELECT * FROM products WHERE id=@id";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                        return new ProductGetDto
                        {
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Description = reader.GetString(reader.GetOrdinal("description")),
                            Price = reader.GetDecimal(reader.GetOrdinal("price")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("image_url")),
                            StockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity"))
                        };
                }
            }
        }
        return null!;
    }

    public async Task<ProductCreateDto> CreateProductAsync(ProductCreateDto productCreateDto)
    {
        using (var connection = _context.CreateConnection())
        {
            await connection.OpenAsync();
            var sql = @"INSERT INTO products (name,description,price,image_url,stock_quantity) VALUES(@Name,@Description,@Price,@ImageUrl,@StockQuantity) RETURNING *";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("Name", productCreateDto.Name!);
                command.Parameters.AddWithValue("Description", productCreateDto.Description!);
                command.Parameters.AddWithValue("Price", productCreateDto.Price!);
                command.Parameters.AddWithValue("ImageUrl", productCreateDto.ImageUrl!);
                command.Parameters.AddWithValue("StockQuantity", productCreateDto.StockQuantity!);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var product = new ProductCreateDto
                        {
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Description = reader.GetString(reader.GetOrdinal("description")),
                            Price = reader.GetDecimal(reader.GetOrdinal("price")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("image_url")),
                            StockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity"))
                        };
                        return product;
                    }

                }
            }
        }
        throw new Exception("Failed to create product");
    }

    public async Task<ProductUpdateDto> UpdateProductAsync(ProductUpdateDto product)
    {
        using (var connection = _context.CreateConnection())
        {
            await connection.OpenAsync();
            var sql = @"UPDATE products SET name=@Name, description=@Description, price=@Price, image_url=@ImageUrl, stock_quantity=@StockQuantity WHERE id=@Id RETURNING *";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("Id", product.Id);
                command.Parameters.AddWithValue("Name", product.Name!);
                command.Parameters.AddWithValue("Description", product.Description!);
                command.Parameters.AddWithValue("Price", product.Price!);
                command.Parameters.AddWithValue("ImageUrl", product.ImageUrl!);
                command.Parameters.AddWithValue("StockQuantity", product.StockQuantity);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var newProduct = new ProductUpdateDto
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Description = reader.GetString(reader.GetOrdinal("description")),
                            Price = reader.GetDecimal(reader.GetOrdinal("price")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("image_url")),
                            StockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity"))
                        };
                        return newProduct;
                    }

                }

            }
        }
        throw new Exception("Failed to update product");
    }
    
    public async Task<int> DeleteProductAsync(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            await connection.OpenAsync();
            var sql = @"DELETE FROM products WHERE id=@Id";
            using(var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("Id", id);
                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected;
            }
        }
    }
}