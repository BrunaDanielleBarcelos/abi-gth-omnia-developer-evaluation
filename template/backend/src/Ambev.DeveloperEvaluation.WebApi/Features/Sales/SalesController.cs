using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

[Route("api/[controller]")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalesController(IMediator mediator)
    {
        _mediator = mediator;
    }

  /*  [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sales = await _mediator.Send(new GetAllSales());
        return Ok(sales);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var sale = await _mediator.Send(new GetSaleById { Id = id });
        if (sale == null)
            return NotFound();

        return Ok(sale);
    }*/

    [HttpPost]
    public async Task<IActionResult> Create(CreateSalesRequest request)
    {
        var sale = await _mediator.Send(request);
        return Ok(sale);        
    }

 /*   [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateSale request)
    {
        request.Id = id;
        var sale = await _mediator.Send(request);
        if (sale == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelSale(Guid id)
    {
        var result = await _mediator.Send(new CancelSale { Id = id });
        if (!result)
            return NotFound();

        return NoContent();
    }*/
}
