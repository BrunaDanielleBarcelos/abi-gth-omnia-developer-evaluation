using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class GetSalesHandlerTests
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;
        private readonly GetSalesHandler _handler;

        public GetSalesHandlerTests()
        {
            _salesRepository = Substitute.For<ISalesRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetSalesHandler(_salesRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid sale ID When retrieving sale Then returns the correct result")]
        public async Task Handle_ValidRequest_ReturnsSaleResult()
        {
            // Given                                    
            var command = new GetSalesCommand("123456");

            var saleEntity = new SalesEntity
            {
                CodigoVenda = command.CodigoVenda,
                // Populating the remaining fields
                ProductName = "Sample Product",
                Quantity = 10,
                UnitPrice = 100.00M,
                Discount = 10,
                TotalAmount = 900.00M
            };

            var saleResult = new GetSalesResult
            {
                CodigoVenda = saleEntity.CodigoVenda,
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        ProductName = saleEntity.ProductName,
                        Quantity = saleEntity.Quantity,
                        UnitPrice = saleEntity.UnitPrice,
                        Discount = saleEntity.Discount
                    }
                },
                CreatedAt = DateTime.UtcNow
            };

            _salesRepository.GetByIdAsync(command.CodigoVenda, Arg.Any<CancellationToken>())
                .Returns(saleEntity);

            _mapper.Map<GetSalesResult>(saleEntity).Returns(saleResult);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();            
            result.Items.Should().HaveCount(1);
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1)); 
        }

    }
}
