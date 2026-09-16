namespace Magna_TestApplication.services
{
    public class QrDecoderService
    {
        public (DateTime dateTime, string shift, string variant, string serialNumber) DecodeQrData(string qrData)
        {
            if (string.IsNullOrWhiteSpace(qrData) || qrData.Length < 16)
            {
                throw new ArgumentException("Invalid QR data format.");
            }

            try
            {
                // Extract based on the format: DDMMYYYY + HHmm + Shift(2) + Variant(2) + Serial(4)
                string dateStr = qrData.Substring(0, 8);     // DDMMYYYY
                string timeStr = qrData.Substring(8, 4);     // HHmm
                string shiftCode = qrData.Substring(12, 2);  // AA
                string variantCode = qrData.Substring(14, 2); // 01
                string serialCode = qrData.Substring(16);    // 0001

                // Parse Date and Time
                DateTime dateTime = DateTime.ParseExact(
                    dateStr + timeStr,
                    "ddMMyyyyHHmm",
                    System.Globalization.CultureInfo.InvariantCulture);

                // Return the decoded parts
                return (dateTime, shiftCode, variantCode, serialCode);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to decode QR data: " + ex.Message);
            }
        }
    }
}