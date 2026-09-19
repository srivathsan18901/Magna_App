namespace Magna_TestApplication.Models
{
    public class TravelAndEnduranceLog
    {
        public int SNo { get; set; }
        public DateTime LoggedAt { get; set; }

        public string Date => LoggedAt.ToString("dd-MM-yyyy");
        public string Time => LoggedAt.ToString("HH:mm:ss");

        public string Shift { get; set; } = "";
        public string Variant { get; set; } = "";
        public string SerialNumber { get; set; } = "";

        public string TravelResult { get; set; } = "";
        public string EnduranceResult { get; set; } = "";

        // NEW: overall result (both must PASS for PASS)
        public string Result =>
            (TravelResult == "PASS" && EnduranceResult == "PASS") ? "PASS" : "FAIL";
    }
}