using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using study4_be.Models;
using study4_be.Payment.DTO;
using study4_be.PaymentServices.Momo.Config;
using study4_be.PaymentServices.Momo.Request;
using System.Data;
using MediatR;
using System.Threading;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using study4_be.Interface;
using study4_be.Contrainst;
using static System.Net.Mime.MediaTypeNames;
namespace study4_be.Payment
{
        public class CreatePayment : IRequest<BaseResultWithData<PaymentLinkDto>>
        {
            public string PaymentContent { get; set; } = string.Empty;
            public string PaymentCurrency { get; set; } = string.Empty;
            public string PaymentRefId { get; set; } = string.Empty;
            public decimal? RequiredAmount { get; set; }
            public DateTime? PaymentDate { get; set; } = DateTime.Now;
            public DateTime? ExpireDate { get; set; } = DateTime.Now.AddMinutes(15);
            public string? PaymentLanguage { get; set; } = string.Empty;
            public string? MerchantId { get; set; } = string.Empty;
            public string? PaymentDestinationId { get; set; } = string.Empty;
            public string? Signature { get; set; } = string.Empty;
        }
    public class CreatePaymentHandler : IRequestHandler<CreatePayment, BaseResultWithData<PaymentLinkDto>>
    {
        private readonly ICurrentUserServices currentUserService;
        private readonly IConnectionService connectionService;
        private readonly ISqlService sqlService;
        //private readonly VnpayConfig vnpayConfig;
        private readonly MomoConfig momoConfig;
        //private readonly ZaloPayConfig zaloPayConfig;
        public CreatePaymentHandler(ICurrentUserServices currentUserService,
              IConnectionService connectionService,
              ISqlService sqlService,
              IOptions<MomoConfig> momoConfigOptions)
              //IOptions<ZaloPayConfig> zaloPayConfigOptions,
              //IOptions<VnpayConfig> vnpayConfigOptions
        {
            this.currentUserService = currentUserService;
            this.connectionService = connectionService;
            this.sqlService = sqlService;
            //this.vnpayConfig = vnpayConfigOptions.Value;
            this.momoConfig = momoConfigOptions.Value;
            //this.zaloPayConfig = zaloPayConfigOptions.Value;
        }
        public Task<BaseResultWithData<PaymentLinkDto>> Handle(
           CreatePayment request, CancellationToken cancellationToken)
        {
            var result = new BaseResultWithData<PaymentLinkDto>();

            try
            {
                // Xử lý logic tạo thanh toán tại đây

                // Ví dụ:
                var paymentUrl = string.Empty;

                switch (request.PaymentDestinationId)
                {
                    case "MOMO":
                        // Xử lý tạo liên kết thanh toán qua Momo
                        var momoOneTimePayRequest = new MomoOneTimePaymentRequest(
                            momoConfig.PartnerCode,
                            Guid.NewGuid().ToString(), // Đặt PaymentId ngẫu nhiên
                            (long)(request.RequiredAmount ?? 0),
                            Guid.NewGuid().ToString(), // Đặt OrderId ngẫu nhiên
                            request.PaymentContent ?? string.Empty,
                            momoConfig.ReturnUrl,
                            momoConfig.IpnUrl,
                            "captureWallet",
                            string.Empty);
                        momoOneTimePayRequest.MakeSignature(momoConfig.AccessKey, momoConfig.SecretKey);
                        (bool createMomoLinkResult, string? createMessage) = momoOneTimePayRequest.GetLink(momoConfig.PaymentUrl);
                        if (createMomoLinkResult)
                        {
                            //paymentUrl = createMessage;
                            paymentUrl = "ehehe";
                        }
                        else
                        {
                            //result.Message = createMessage;
                            result.Message = createMessage;
                        }
                        break;

                    default:
                        break;
                }

                result.Set(true, MessagesConstranst.OK, new PaymentLinkDto()
                {
                    PaymentId = Guid.NewGuid().ToString(), // Đặt PaymentId ngẫu nhiên
                    PaymentUrl = result.Message,
                });
            }
            catch (Exception ex)
            {
                result.Set(false, MessagesConstranst.Error);
                result.Errors.Add(new BaseError()
                {
                    Code = MessagesConstranst.Exception,
                    Message = ex.Message,
                });
            }
            return Task.FromResult(result);
        }

    }
}
