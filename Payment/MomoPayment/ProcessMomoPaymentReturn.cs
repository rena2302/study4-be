using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using study4_be.Contrainst;
using study4_be.Interface;
using study4_be.Models.DTO;
using study4_be.Models;
using study4_be.PaymentServices.Momo.Config;
using study4_be.PaymentServices.Momo.Request;
using study4_be.Payment.DTO;

namespace study4_be.Payment.MomoPayment
{
    public class ProcessMomoPaymentReturn : MomoOneTimePaymentResultRequest,
       IRequest<BaseResultWithData<(PaymentReturnDtos, string)>>
    {
    }

    public class ProcessMomoPaymentReturnHandler
        : IRequestHandler<ProcessMomoPaymentReturn, BaseResultWithData<(PaymentReturnDtos, string)>>
    {
        private readonly IConnectionService connectionService;
        private readonly ISqlService sqlService;
        private readonly MomoConfig momoConfig;

        public ProcessMomoPaymentReturnHandler(IConnectionService connectionService,
            ISqlService sqlService,
            IOptions<MomoConfig> momoConfigOptions)
        {
            this.connectionService = connectionService;
            this.sqlService = sqlService;
            this.momoConfig = momoConfigOptions.Value;
        }

        public Task<BaseResultWithData<(PaymentReturnDtos, string)>> Handle(ProcessMomoPaymentReturn request, CancellationToken cancellationToken)
        {
            string returnUrl = string.Empty;
            var result = new BaseResultWithData<(PaymentReturnDtos, string)>();

            try
            {

            }
            catch (Exception ex)
            {

            }

            return Task.FromResult(result);
        }
    }
}
