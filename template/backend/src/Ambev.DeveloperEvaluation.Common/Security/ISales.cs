namespace Ambev.DeveloperEvaluation.Common.Security
{
    /// <summary>
    /// Defines the contract for representing a sale in the system.
    /// </summary>
    public interface ISalesEntity
    {
        /// <summary>
        /// Gets the unique identifier of the sale.
        /// </summary>
        /// <returns>The ID of the sale as a string.</returns>
        public Guid Id { get; }

        public string CodigoVenda { get; }

        /// <summary>
        /// Gets the name of the product in the sale.
        /// </summary>
        /// <returns>The name of the product.</returns>
        public string ProductName { get; }

        /// <summary>
        /// Gets the unit price of the product in the sale.
        /// </summary>
        /// <returns>The unit price.</returns>
        public decimal UnitPrice { get; }

        /// <summary>
        /// Gets the discount applied to the sale.
        /// </summary>
        /// <returns>The discount value as a decimal.</returns>
        public int Discount { get; }
    }
}
