using MediatR;
using MicroMarinCaseV2.Api.Strategies;
using MicroMarinCaseV2.Application.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace MicroMarinCaseV2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOperation _operation;

        public RecordsController(IMediator mediator, IOperation operation)
        {
            _mediator = mediator;
            _operation = operation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordType"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>
        /// **Sample request body:**
        /// {
        /// "name":"John",
        /// "surname":"Doe",
        /// "email":"john.doe@example.com",
        /// "address":{ "Country":"USA","City":"New York","AddressLine":"Street 123","AddressLine2":"test"},
        /// "orders":[
        /// {
        /// "orderItems":
        ///     [
        ///     {
        ///         "Count":5,
        ///         "Price":17.12,
        ///         "ProductId":"d04BF3911-9C6E-4960-80CF-81F865A784F0"
        ///     },
        ///     {
        ///         "Count":12,
        ///         "Price":9.99,
        ///         "ProductId":"04BF3911-9C6E-4960-80CF-81F865A784F1"
        ///     }
        ///     ]
        /// }
        /// ]
        /// }
        /// 
        /// 
        /// 
        /// </remarks>
        /// <exception cref="ArgumentException"></exception>
        [HttpPost("{recordType}")]
        public async Task<IActionResult> Create(string recordType, [FromBody] JsonObject request, CancellationToken cancellationToken = default)
        {
            switch (recordType)
            {
                case "customer":
                    _operation.SetStrategy(new CustomerStrategy(_mediator));
                    break;
                case "order":
                    _operation.SetStrategy(new OrderStrategy(_mediator));
                    break;
                case "product":
                    _operation.SetStrategy(new ProductStrategy(_mediator));
                    break;
                default:
                    throw new ArgumentException("This operation not provided.");
            }


            return Ok(await _operation.Create(request));
        }
        [HttpDelete("{recordType}/{id}")]
        public async Task<IActionResult> Delete(string recordType,Guid id)
        {
            switch (recordType)
            {
                case "customer":
                    _operation.SetStrategy(new CustomerStrategy(_mediator));
                    break;
                case "order":
                    _operation.SetStrategy(new OrderStrategy(_mediator));
                    break;
                case "product":
                    _operation.SetStrategy(new ProductStrategy(_mediator));
                    break;
                default:
                    throw new ArgumentException("This operation not provided.");
            }

            await _operation.Delete(id);

            return NoContent();
        }

        [HttpPut("{recordType}/{id}")]
        public async Task<IActionResult> Update(string recordType, Guid id, [FromBody] JsonObject request)
        {
            switch (recordType)
            {
                case "customer":
                    _operation.SetStrategy(new CustomerStrategy(_mediator));
                    break;
                case "order":
                    _operation.SetStrategy(new OrderStrategy(_mediator));
                    break;
                case "product":
                    _operation.SetStrategy(new ProductStrategy(_mediator));
                    break;
                default:
                    throw new ArgumentException("This operation not provided.");
            }

            await _operation.Update(id,request);

            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordType"></param>
        /// <param name="filterParameters"></param>
        /// <returns></returns>
        /// <remarks>
        /// **Sample request body:**
        /// {
        ///    "Filters": [
        ///        { "Key": "Orders.OrderItems", "Value": "", "FilterType": "includes" },
        ///        { "Key": "Name", "Value": "Janet", "FilterType": "Equals" }
        ///    ]
        ///  }
        ///
        /// </remarks>
        /// <exception cref="ArgumentException"></exception>
        [HttpPost("{recordType}/Get")]
        public async Task<IActionResult> Get(string recordType, [FromBody] FilterParameters filterParameters)
        {
            switch (recordType)
            {
                case "customer":
                    _operation.SetStrategy(new CustomerStrategy(_mediator));
                    break;
                case "order":
                    _operation.SetStrategy(new OrderStrategy(_mediator));
                    break;
                case "product":
                    _operation.SetStrategy(new ProductStrategy(_mediator));
                    break;
                default:
                    throw new ArgumentException("This operation not provided.");
            }

            var result = await _operation.Get(filterParameters);

            return Ok(result);
        }
    }
}
