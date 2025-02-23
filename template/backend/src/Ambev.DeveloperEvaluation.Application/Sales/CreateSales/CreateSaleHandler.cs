using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
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
        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            if (request.Items == null || !request.Items.Any())
            {
                throw new FluentValidation.ValidationException("Items cannot be null or empty.");
            }

            // Applying quantity rules and discount calculation for each item
            foreach (var item in request.Items)
            {
                // If quantity exceeds 20, throw an exception
                if (item.Quantity > 20)
                    throw new Exception("It is not possible to sell more than 20 identical items.");

                // Assigning discounts based on quantity
                if (item.Quantity >= 10)
                    item.Discount = 0.20m; // 20% discount
                else if (item.Quantity >= 4)
                    item.Discount = 0.10m; // 10% discount
                else
                    item.Discount = 0; // No discount

                // Calculating the total price considering the discount
                item.TotalPrice = item.Quantity * item.UnitPrice * (1 - item.Discount);
            }

            // Creating the sale object
            var sale = new SalesEntity
            {
                // Assuming you are creating a new sale entity
                Id = Guid.NewGuid(),
                Productname = request.Productname,
                UnitPrice = request.UnitPrice,
                Discount = request.Discount,
                Items = request.Items.Select(item => new ItemsEntity
                {
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount,
                    TotalPrice = item.TotalPrice
                }).ToList(),
                TotalAmount = request.Items.Sum(i => i.TotalPrice),
                CreatedAt = DateTime.UtcNow
            };

            // Saving the sale to the repository
            var createdSale = await _salesRepository.CreateAsync(sale, cancellationToken);

            // Mapping the result to the CreateSaleResult
            var result = _mapper.Map<CreateSaleResult>(createdSale);

            // Returning the result
            return result;
        }
    }
}
