using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IMapper _mapper;

    // Constructor to inject dependencies
    public CreateSaleHandler(ISalesRepository salesRepository, IMapper mapper)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
    }

    // Handler to process the sale creation
    public async Task<CreateSaleResult?> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var createdSales = new List<SalesEntity>();
        
        var codigoVenda = new Random().Next(100000, 999999).ToString(); // Changed to string

        // Iterate over each item and create a separate sale for each one
        foreach (var item in request.Items)
        {
            if (item.Quantity > 20)
                throw new Exception("It is not possible to sell more than 20 identical items.");

            // Applying discount rules
            item.Discount = item.Quantity >= 10 ? 20 :
                            item.Quantity >= 4 ? 10 : 0;

            // Calculate the total price of the item with the discount
            item.TotalPrice = item.Quantity * item.UnitPrice * (1 - (item.Discount / 100m));

            
            // Creating a separate sale for each item
            var sale = new SalesEntity
            {
                Id = Guid.NewGuid(),
                ProductName = item.ProductName,
                CodigoVenda = codigoVenda, // Store the string
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = item.Discount,
                TotalAmount = item.TotalPrice, // TotalAmount now represents the total for a single item
                CreatedAt = DateTime.UtcNow
            };

            // Saving to the repository
            var createdSale = await _salesRepository.CreateAsync(sale, cancellationToken);
            createdSales.Add(createdSale);
        }

        var result = new CreateSaleResult
        {
            CodigoVenda = codigoVenda.ToString(),
            Items = createdSales.Select(sale => new SaleItem
            {
                ProductName = sale.ProductName,
                Quantity = sale.Quantity,
                UnitPrice = sale.UnitPrice,
                Discount = sale.Discount,
                TotalPrice = sale.Quantity * sale.UnitPrice * (1 - sale.Discount / 100m)
            }).ToList()
        };

        // Returning the list of created sales (Only the first sale is returned for simplicity)
        return result;
    }
}
