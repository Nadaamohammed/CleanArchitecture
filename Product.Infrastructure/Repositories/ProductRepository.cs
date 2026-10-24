namespace Product.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Product.Application.Interface;
using Product.Infrastructure.persistance;


public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entity.Product?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(
                p => p.Id == id, cancellationToken);
    }

    public async Task<List<Domain.Entity.Product>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        Domain.Entity.Product product,
        CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
