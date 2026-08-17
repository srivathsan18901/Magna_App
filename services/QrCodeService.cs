using QRCoder;

namespace Magna_TestApplication.services
{
    public class QrCodeService
    {
        public Bitmap GenerateQr(string qrData)
        {
            using QRCodeGenerator generator = new QRCodeGenerator();

            using QRCodeData data =
                generator.CreateQrCode(
                    qrData,
                    QRCodeGenerator.ECCLevel.Q);

            using QRCode qrCode =
                new QRCode(data);

            return qrCode.GetGraphic(8);
        }
    }
}