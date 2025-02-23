using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale in the system with details about items, total amount, and discounts.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class SalesEntity : BaseEntity, ISalesEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the unique identifier of the sale as a string for ISalesEntity interface.
        /// </summary>
        public string IdString => Id.ToString(); // ISalesEntity interface expects a string

        public string CodigoVenda { get; set; }
        /// <summary>
        /// Gets or sets the name of the product in the sale.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unit price of the product in the sale.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount for the sale as a percentage.
        /// The discount is stored as a decimal value, e.g., 0.10 for a 10% discount.
        /// </summary>
        public int Discount { get; set; } = 0;

        /// <summary>
        /// Gets or sets the quantity of the item being sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the sale, calculated from all items.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the sale was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the Sale class.
        /// </summary>
        public SalesEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }

}
