namespace Magna_TestApplication.Models
{
    public class FunctionalTestLog
    {
        public int SNo { get; set; }

        public DateTime LoggedAt { get; set; }

        public string Date =>
            LoggedAt.ToString("dd-MM-yyyy");

        public string Time =>
            LoggedAt.ToString("HH:mm:ss");

        public string Shift { get; set; } = "";

        public string Variant { get; set; } = "";

        public string Result { get; set; } = "";
    }
}