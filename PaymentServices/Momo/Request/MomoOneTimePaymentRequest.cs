using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NuGet.ContentModel;
using study4_be.Helper;
using study4_be.PaymentServices.Momo.Response;
using System.Text;

namespace study4_be.PaymentServices.Momo.Request
{
    public class MomoOneTimePaymentRequest
    {
        public MomoOneTimePaymentRequest(string partnerCode, string requestId,
             long amount, string orderId, string orderInfo, string redirectUrl,
             string ipnUrl, string requestType, string extraData, string lang = "vi")
        {
            this.partnerCode = partnerCode;
            this.requestId = requestId;
            this.amount = amount;
            this.orderId = orderId;
            this.orderInfo = orderInfo;
            this.redirectUrl = redirectUrl;
            this.ipnUrl = ipnUrl;
            this.requestType = requestType;
            this.extraData = extraData;
            this.lang = lang;
        }
        public string partnerCode { get; set; } = string.Empty;
        public string requestId { get; set; } = string.Empty;
        public long amount { get; set; }
        public string orderId { get; set; } = string.Empty;
        public string orderInfo { get; set; } = string.Empty;
        public string redirectUrl { get; set; } = string.Empty;
        public string ipnUrl { get; set; } = string.Empty;
        public string requestType { get; set; } = string.Empty;
        public string extraData { get; set; } = string.Empty;
        public string lang { get; set; } = string.Empty;
        public string signature { get; set; } = string.Empty;
        public string ss { get; set; }
        public void MakeSignature(string accessKey, string secretKey)
        {
            var rawHash = "accessKey=" + accessKey +
                "&amount=" + this.amount +
                "&extraData=" + this.extraData +
                "&ipnUrl=" + this.ipnUrl +
                "&orderId=" + this.orderId +
                "&orderInfo=" + this.orderInfo +
                "&partnerCode=" + this.partnerCode +
                "&redirectUrl=" + this.redirectUrl +
                "&requestId=" + this.requestId +
                "&requestType=" + this.requestType;
            ////this.signature = HashHelper.(rawHash, secretKey);
            //// Tạo đối tượng JSON chứa chữ ký
            //var signatureJson = JsonConvert.SerializeObject(new { Signature = this.signature });
            //ss= rawHash;
        }

        public (bool, string?) GetLink(string paymentUrl)
        {
            using HttpClient client = new HttpClient();
            var requestData = JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
            });
            var requestContent = new StringContent(requestData, Encoding.UTF8,
                "application/json");
            var createPaymentLinkRes = client.PostAsync(paymentUrl, requestContent)
                .Result;

            if (createPaymentLinkRes.IsSuccessStatusCode)
            {
                var responseContent = createPaymentLinkRes.Content.ReadAsStringAsync().Result;
                var responseData = JsonConvert
                    .DeserializeObject<MomoOneTimePaymentCreateLinkResponse>(responseContent);
                if (responseData.resultCode == "0")
                {
                    //return (true, responseData.payUrl);
                    return (true, "1");
                }
                else
                {
                    //return (false, responseData.message);
                    return (false, "2");
                }

            }
            else
            {
                    var errorContent = createPaymentLinkRes.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("Error Content: " + errorContent);
                return (false, ss);
                //return (false, createPaymentLinkRes.ReasonPhrase);
            }
        }
    }
}
