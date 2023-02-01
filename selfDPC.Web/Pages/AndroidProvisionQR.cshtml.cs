using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace selfDPC.Web.Pages
{
    public class AndroidProvisionQRModel : PageModel
    {
        public string QrImageSrc { get; private set; } = string.Empty;

        public void OnGet()
        {
            var pi = new AndroidProvisioningInformation();
            var pijson = JsonSerializer.Serialize(pi);

            using (var qrg = new QRCodeGenerator())
            using (var qrd = qrg.CreateQrCode(pijson, QRCodeGenerator.ECCLevel.L))
            using (var qrc = new PngByteQRCode(qrd))
            {
                string b64png = Convert.ToBase64String(qrc.GetGraphic(20));
                QrImageSrc = "data:image/png;base64," + b64png;
            }
        }

        /// <summary>
        /// See: https://developers.google.com/android/work/play/emm-api/prov-devices#create_a_qr_code
        /// </summary>
        public class AndroidProvisioningInformation
        {
            [Required]
            [JsonPropertyName("android.app.extra.PROVISIONING_DEVICE_ADMIN_COMPONENT_NAME")]
            public string DeviceAdminComponentName { get; set; }

            [JsonPropertyName("android.app.extra.PROVISIONING_DEVICE_ADMIN_PACKAGE_CHECKSUM")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? DeviceAdminPackageChecksum { get; set; }

            [DataType(DataType.Url)]
            [JsonPropertyName("android.app.extra.PROVISIONING_DEVICE_ADMIN_PACKAGE_DOWNLOAD_LOCATION")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? DeviceAdminPackageDownloadLocation { get; set; }

            [JsonPropertyName("android.app.extra.PROVISIONING_WIFI_SSID")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? WifiSsid { get; set; }

            [JsonPropertyName("android.app.extra.PROVISIONING_WIFI_PASSWORD")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? WifiPassword { get; set; }

            [JsonPropertyName("android.app.extra.PROVISIONING_WIFI_HIDDEN")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public bool? WifiHidden { get; set; }

            /// <summary>
            /// See: https://developer.android.com/reference/android/app/admin/DevicePolicyManager.html#EXTRA_PROVISIONING_LOCALE
            /// </summary>
            /// <value>
            /// Acceptable values: https://developer.android.com/reference/java/util/Locale
            /// </value>
            [JsonPropertyName("android.app.extra.PROVISIONING_LOCALE")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public bool Locale { get; set; }

            /// <summary>
            /// See: https://developer.android.com/reference/android/app/admin/DevicePolicyManager.html#EXTRA_PROVISIONING_TIME_ZONE
            /// </summary>
            /// <value>
            /// Acceptable values: https://developer.android.com/reference/java/util/TimeZone
            /// </value>
            [JsonPropertyName("android.app.extra.PROVISIONING_TIME_ZONE")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public bool TimeZone { get; set; }

            /// <summary>
            /// See: https://developer.android.com/reference/android/app/admin/DevicePolicyManager.html#EXTRA_PROVISIONING_WIFI_SECURITY_TYPE
            /// </summary>
            [JsonPropertyName("android.app.extra.PROVISIONING_WIFI_SECURITY_TYPE")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? WifiSecurityType { get; set; }
        }
    }
}
