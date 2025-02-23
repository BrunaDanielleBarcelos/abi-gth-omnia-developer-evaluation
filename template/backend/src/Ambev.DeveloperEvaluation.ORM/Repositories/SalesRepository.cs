using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISalesRepository using Entity Framework Core
/// </summary>
public class SalesRepository : ISalesRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SalesRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SalesRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new sale in the repository
    /// </summary>
    /// <param name="sale">The sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale</returns>
    public async Task<SalesEntity> CreateAsync(SalesEntity sales, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sales, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sales;
    }
    /// <summary>
    /// Retrieves a sale by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    public async Task<SalesEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales.FirstOrDefaultAsync(o => o.CodigoVenda == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a sale by its product name
    /// </summary>
    /// <param name="productName">The product name to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>

    public async Task<SalesEntity?> GetByProductNameAsync(string productName, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .FirstOrDefaultAsync(u => u.ProductName == productName, cancellationToken);
    }

    /// <summary>
    /// Deletes a sale from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale was deleted, false if not found</returns>

    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale == null)
            return false;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
