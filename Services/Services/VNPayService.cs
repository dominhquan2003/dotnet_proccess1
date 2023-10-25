using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using Services.Helpers;

public class VNPayService
{
    private static string callBackURl = "https://localhost:7222/order/callback";
    private static string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
    private static string vnp_TmnCode = "5I3WMN4E";
    private static string vnp_HashSecret = "AMYWRJIRXHWAGPRJNXOQEXQCFRFXQMII";
    private static string vnp_Version = "2.1.0";
    private static string vnp_Command = "pay";
    private static string vnp_OrderType = "other";

    public static string GeneratePaymentUrl(decimal vnp_Amount, string vnp_BankCode, string vnp_OrderInfo, string vnp_TxnRef)
    {
        TimeZoneInfo timeZone = TimeZoneInfo.Local;
        DateTime now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        string vnp_CreateDate = now.ToString("yyyyMMddHHmmss");

        // vnp query params
        Dictionary<string, string> vnp_Params = new Dictionary<string, string>(){
            { "vnp_Version", vnp_Version },
            { "vnp_Command", vnp_Command },
            { "vnp_TmnCode", vnp_TmnCode },
            { "vnp_Amount", (vnp_Amount*100).ToString()},
            { "vnp_BankCode", vnp_BankCode },
            { "vnp_CreateDate", vnp_CreateDate },
            { "vnp_CurrCode", "VND" },
            { "vnp_Locale", "vn" },
            { "vnp_OrderInfo", vnp_OrderInfo },
            { "vnp_IpAddr", GetClientIpAddress() },
            { "vnp_OrderType", vnp_OrderType },
            { "vnp_ReturnUrl", callBackURl },
            { "vnp_TxnRef", vnp_TxnRef}
        };

        DateTime expiredTime = now.AddMinutes(15);
        string vnp_ExpireDate = expiredTime.ToString("yyyyMMddHHmmss");
        vnp_Params.Add("vnp_ExpireDate", vnp_ExpireDate);

        string queryUrl = string.Join("&", vnp_Params
            .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
            .OrderBy(kvp => kvp.Key)
            .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));

        string vnp_SecureHash = Generate.CalculateHMACSHA512(vnp_HashSecret, queryUrl);

        string paymentUrl = $"{vnp_Url}?{queryUrl}&vnp_SecureHash={vnp_SecureHash}";

        return paymentUrl;
    }

    public static string GetClientIpAddress()
    {
        string ipAddress = string.Empty;
        string hostName = Dns.GetHostName();
        IPAddress[] addresses = Dns.GetHostAddresses(hostName);

        foreach (IPAddress address in addresses)
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
            {
                ipAddress = address.ToString();
                break;
            }
        }
        return ipAddress;
    }
}
