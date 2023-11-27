using Microsoft.AspNetCore.Mvc;
using WembleyScada.Api.Application.Queries.Products;

namespace WembleyScada.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductViewModel>> GetProducts([FromQuery]ProductsQuery query)
    {
        return await _mediator.Send(query);
    }
}
