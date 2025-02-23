using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;

    public record DeleteSalesCommand : IRequest<DeleteSalesResponse>
    {
        public string Id { get; }

        public DeleteSalesCommand(string id)
        {
            Id = id;
        }
    }

