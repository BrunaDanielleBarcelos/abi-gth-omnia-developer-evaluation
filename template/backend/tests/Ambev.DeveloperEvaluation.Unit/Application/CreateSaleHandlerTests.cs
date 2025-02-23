using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CreateSaleHandlerTests
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _salesRepository = Substitute.For<ISalesRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateSaleHandler(_salesRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            // Calculate TotalPrice for each item in the command
            foreach (var item in command.Items)
            {
                item.TotalPrice = item.Quantity * item.UnitPrice * (1 - item.Discount / 100m);
            }

            // SalesEntity mock creation
            var sale = new SalesEntity
            {
                Id = Guid.NewGuid(),
                ProductName = command.Items.First().ProductName, // Example using the first item
                UnitPrice = command.Items.First().UnitPrice,
                Discount = command.Items.First().Discount,
                Quantity = command.Items.Sum(i => i.Quantity), // Sum of all quantities in command
                TotalAmount = command.Items.Sum(i => i.TotalPrice), // Total sum of all item prices
                CreatedAt = DateTime.UtcNow,
            };

            var result = new CreateSaleResult
            {
                CodigoVenda = sale.CodigoVenda,
                Items = command.Items.Select(item => new SaleItem
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount,
                    TotalPrice = item.Quantity * item.UnitPrice * (1 - item.Discount / 100m) // Correct price calculation
                }).ToList()
            };

            _mapper.Map<SalesEntity>(command).Returns(sale);
            _mapper.Map<CreateSaleResult>(sale).Returns(result);

            _salesRepository.CreateAsync(Arg.Any<SalesEntity>(), Arg.Any<CancellationToken>())
                .Returns(sale);

            // When
            var createSaleResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            createSaleResult.Should().NotBeNull();
            createSaleResult.CodigoVenda.Should().NotBeNull();           

        }

        [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new CreateSaleCommand(); // Empty command will fail validation

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact(DisplayName = "Given sale items When handling Then calculates discounts and total price correctly")]
        public async Task Handle_ValidRequest_CalculatesDiscountAndTotalPriceCorrectly()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            // Calculate TotalPrice for each item in the command
            foreach (var item in command.Items)
            {
                item.TotalPrice = item.Quantity * item.UnitPrice * (1 - item.Discount / 100m);
            }

            // Mock SalesEntity creation
            var sale = new SalesEntity
            {
                Id = Guid.NewGuid(),
                ProductName = command.Items.First().ProductName,
                UnitPrice = command.Items.First().UnitPrice,
                Discount = command.Items.First().Discount,
                Quantity = command.Items.Sum(i => i.Quantity),
                TotalAmount = command.Items.Sum(i => i.TotalPrice),
                CreatedAt = DateTime.UtcNow
            };

            var saleItems = command.Items.Select(item => new SalesEntity
            {
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = item.Discount
            }).ToList();

            _mapper.Map<SalesEntity>(command).Returns(sale);
            _salesRepository.CreateAsync(Arg.Any<SalesEntity>(), Arg.Any<CancellationToken>())
                .Returns(sale);

            // When
            var createSaleResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            createSaleResult.Items.Should().NotContain(item => item.TotalPrice <= 0);
        }

        [Fact(DisplayName = "Given quantity greater than 20 When handling Then throws exception")]
        public async Task Handle_QuantityGreaterThan20_ThrowsException()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            // Modify command to have an item with quantity greater than 20
            command.Items[0].Quantity = 21;

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("It is not possible to sell more than 20 identical items.");
        }
    }
}
