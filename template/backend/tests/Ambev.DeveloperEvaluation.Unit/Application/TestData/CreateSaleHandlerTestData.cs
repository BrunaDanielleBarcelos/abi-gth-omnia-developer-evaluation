using Ambev.DeveloperEvaluation.Application.Sales;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class CreateSaleHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid CreateSaleCommand entities.
        /// The generated sale commands will have valid:
        /// - Productname (random product names)
        /// - UnitPrice (random prices)
        /// - Discount (random discount percentage)
        /// - Items (randomized sale items with quantity and price)
        /// </summary>
        private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
            .RuleFor(s => s.Productname, f => f.Commerce.ProductName())
            .RuleFor(s => s.UnitPrice, f => f.Random.Decimal(10, 100)) // UnitPrice between 10 and 100
            .RuleFor(s => s.Discount, f => f.Random.Decimal(0, 0.30m)) // Discount up to 30%
            .RuleFor(s => s.Items, f => f.Make(3, () => new SaleItem
            {
                Quantity = f.Random.Int(1, 20), // Random quantity between 1 and 20
                UnitPrice = f.Random.Decimal(10, 100), // Random unit price between 10 and 100
                Discount = f.Random.Decimal(0, 0.30m), // Random discount up to 30%
                TotalPrice = 0 // Initialize with a placeholder, will calculate later
            }));

        /// <summary>
        /// Generates a valid CreateSaleCommand entity with randomized data.
        /// The generated sale command will have all properties populated with valid values
        /// that meet the system's validation requirements.
        /// </summary>
        /// <returns>A valid CreateSaleCommand entity with randomly generated data.</returns>
        public static CreateSaleCommand GenerateValidCommand()
        {
            var command = createSaleHandlerFaker.Generate();

            // Calculate the TotalPrice for each item dynamically based on Quantity, UnitPrice, and Discount
            foreach (var item in command.Items)
            {
                item.TotalPrice = item.Quantity * item.UnitPrice * (1 - item.Discount); // Calculate total price with discount applied
            }

            return command;
        }
    }
   
}
