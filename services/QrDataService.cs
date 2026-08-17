namespace Magna_TestApplication.services
{
    public class QrDataService
    {
        public string GenerateQrData(
            DateTime dateTime,
            string shift,
            string variant,
            int quantity)
        {
            string date =
                dateTime.ToString("ddMMyyyy");

            string time =
                dateTime.ToString("HHmm");

            string shiftCode =
                GetShiftCode(shift);

            string variantCode =
                GetVariantCode(variant);

            string quantityCode =
                quantity.ToString("D4");

            return
                date +
                time +
                shiftCode +
                variantCode +
                quantityCode;
        }

        private string GetShiftCode(string shift)
        {
            if (string.IsNullOrWhiteSpace(shift))
                return "00";

            shift = shift.Trim().ToUpper();

            if (shift.Length == 1)
                return shift + shift;

            return shift.Substring(0, 2);
        }

        private string GetVariantCode(string variant)
        {
            if (string.IsNullOrWhiteSpace(variant))
                return "00";

            if (int.TryParse(variant, out int value))
            {
                return value.ToString("D2");
            }

            return "00";
        }
    }
}