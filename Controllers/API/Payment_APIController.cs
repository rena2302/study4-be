using MediatR;
using Microsoft.AspNetCore.Mvc;
using study4_be.Models;
using study4_be.Payment.DTO;
using study4_be.Payment;
using System.Net;
using study4_be.PaymentServices.Momo.Request;
using study4_be.Models.DTO;

namespace study4_be.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Payment_APIController : ControllerBase
    {
        private readonly IMediator _mediator;

        public Payment_APIController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultWithData<PaymentLinkDto>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreatePayment request)
        {
            var response = new BaseResultWithData<PaymentLinkDto>();
            response = await _mediator.Send(request);
            return Ok(response);
        }
        

    }
}
