using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using study4_be.Helper;
using study4_be.Payment.MomoPayment;
using study4_be.PaymentServices.Momo.Config;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
[Route("api/[controller]")]
[ApiController]
public class Momo_PaymentController : ControllerBase
{
    private readonly ILogger<Momo_PaymentController> _logger;
    private readonly MomoConfig _momoConfig;
    private readonly HashHelper _hashHelper;
    public Momo_PaymentController(ILogger<Momo_PaymentController> logger, IOptions<MomoConfig> momoPaymentSettings)
    {
        _logger = logger;
        _momoConfig = momoPaymentSettings.Value;
        _hashHelper = new HashHelper();
    }
    [HttpPost]
    public async Task<IActionResult> MakePayment([FromBody] MomoPaymentRequest request)
    {
        try
        {
            var signature = _hashHelper.GenerateSignature(request, _momoConfig);
            var response = await SendPaymentRequest(request, signature);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Ok(responseContent);
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return BadRequest($"Yêu cầu thanh toán không thành công. Mã lỗi: {response.StatusCode}. Chi tiết lỗi: {errorResponse}");
            }
        }
        catch (Exception ex)
        {
            // Ghi log lỗi
            _logger.LogError(ex, "Lỗi khi thực hiện yêu cầu thanh toán MoMo");

            // Trả về lỗi 500 Internal Server Error
            return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu thanh toán");
        }
    }
    private async Task<HttpResponseMessage> SendPaymentRequest(MomoPaymentRequest request, string signature)
    {
        // Dữ liệu yêu cầu thanh toán
        var paymentData = new
        {
            partnerCode = _momoConfig.PartnerCode,
            storeName = _momoConfig.StoreName,
            storeId = _momoConfig.StoreId,
            subPartnerCode = request.SubPartnerCode,
            requestId = request.RequestId,
            amount = request.Amount,
            orderId = request.OrderId,
            orderInfo = request.OrderInfo,
            redirectUrl = request.RedirectUrl,
            ipnUrl = request.IpnUrl,
            requestType = request.RequestType,
            extraData = request.ExtraData,
            lang = request.Lang,
            signature = signature
        };

        using (var client = new HttpClient())
        {
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(paymentData), Encoding.UTF8, "application/json");
            return await client.PostAsync(_momoConfig.PaymentUrl, content);
        }
    }
}
  