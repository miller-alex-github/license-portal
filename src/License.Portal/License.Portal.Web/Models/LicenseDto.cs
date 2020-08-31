using QRCoder;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace License.Portal.Web
{
    public class LicenseDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Product { get; set; }

        [Required]
        [MaxLength(200)]
        public string Package { get; set; }
               
        public string Key { get; set; }

        public string QRCodeImage
        { 
            get 
            {
                var qrcode = @"{""code"":""" + Key + @""",""uri"":""license.zenner.com""}";
                using (var ms = new MemoryStream())
                {
                    var qrGenerator = new QRCodeGenerator();
                    var qrCodeData = qrGenerator.CreateQrCode(qrcode, QRCodeGenerator.ECCLevel.Q);
                    var qrCode = new QRCode(qrCodeData);                   
                    using (Bitmap bitMap = qrCode.GetGraphic(20))
                    {
                        bitMap.Save(ms, ImageFormat.Png);
                        return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }        
                
        [DisplayFormat(DataFormatString = @"{0:yyyy\/MM\/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTimeOffset GeneratedAt { get; set; }

        [DisplayFormat(DataFormatString = @"{0:yyyy\/MM\/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? RegisteredAt { get; set; }
                
        public string HardwareKey { get; set; }

        public string LicenseRaw { get; set; }
    }
}
