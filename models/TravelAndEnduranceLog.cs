namespace Magna_TestApplication.Models
{
    public class TravelAndEnduranceLog
    {
        public int SNo { get; set; }
        public DateTime LoggedAt { get; set; }

        public string Date => LoggedAt.ToString("dd-MM-yyyy");
        public string Time => LoggedAt.ToString("HH:mm:ss");

        // These will be populated by decoding the Functional Test QR
        public string Shift { get; set; } = "";
        public string Variant { get; set; } = "";
        public string SerialNumber { get; set; } = ""; // Parsed from quantityCode

        // Specific to Travel & Endurance
        public string TravelResult { get; set; } = "";
        public string EnduranceResult { get; set; } = "";
    }
}