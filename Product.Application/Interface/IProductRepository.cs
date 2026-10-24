using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Interface;

public interface IProductRepository
{
    Task<Product.Domain.Entity.Product?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

    Task<List<Product.Domain.Entity.Product>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Product.Domain.Entity.Product product,
        CancellationToken cancellationToken = default);
}
