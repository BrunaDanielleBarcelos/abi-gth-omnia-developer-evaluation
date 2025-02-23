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
    /// <summary>
    /// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
    /// </summary>
    public class CreateSaleHandlerTests
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;
        private readonly CreateSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public CreateSaleHandlerTests()
        {
            _salesRepository = Substitute.For<ISalesRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateSaleHandler(_salesRepository, _mapper);
        }

        /// <summary>
        /// Tests that a valid sale creation request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand();
            
            // Sale object to return from the repository
            var sale = new SalesEntity
            {
                Id = Guid.NewGuid(),
                Productname = command.Productname,
                UnitPrice = command.UnitPrice,
                Discount = command.Discount,
                Items = command.Items.Select(item => new ItemsEntity
                {
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount,
                    TotalPrice = item.TotalPrice
                }).ToList(),
                TotalAmount = command.Items.Sum(i => i.TotalPrice),
                CreatedAt = DateTime.UtcNow
            };

            var result = new CreateSaleResult
            {
                Id = sale.Id, 
            };


            _mapper.Map<SalesEntity>(command).Returns(sale);
            _mapper.Map<CreateSaleResult>(sale).Returns(result);

            _salesRepository.CreateAsync(Arg.Any<SalesEntity>(), Arg.Any<CancellationToken>())
                .Returns(sale);

            
            // When
            var createSaleResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            createSaleResult.Should().NotBeNull();
            createSaleResult.Id.Should().Be(sale.Id);
            await _salesRepository.Received(1).CreateAsync(Arg.Any<SalesEntity>(), Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that an invalid sale creation request throws a validation exception.
        /// </summary>
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

        /// <summary>
        /// Tests that the discount and total price are correctly calculated for sale items.
        /// </summary>
        [Fact(DisplayName = "Given sale items When handling Then calculates discounts and total price correctly")]
        public async Task Handle_ValidRequest_CalculatesDiscountAndTotalPriceCorrectly()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand();
            var sale = new SalesEntity
            {
                Id = Guid.NewGuid(),
                Productname = command.Productname,
                UnitPrice = command.UnitPrice,
                Discount = command.Discount,
                Items = command.Items.Select(item => new ItemsEntity
                {
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount,
                    TotalPrice = item.TotalPrice
                }).ToList(),
                TotalAmount = command.Items.Sum(i => i.TotalPrice),
                CreatedAt = DateTime.UtcNow
            };

            _mapper.Map<SalesEntity>(command).Returns(sale);
            _salesRepository.CreateAsync(Arg.Any<SalesEntity>(), Arg.Any<CancellationToken>())
                .Returns(sale);

            // When
            await _handler.Handle(command, CancellationToken.None);

            // Then
            sale.Items.Should().NotContain(item => item.TotalPrice <= 0);
        }
    }
}
