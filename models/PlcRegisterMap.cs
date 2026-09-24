namespace Magna_TestApplication.Models
{
    public class PlcRegisterMap
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string ParameterName { get; set; }
        public string RegisterAddress { get; set; }
        public string ValueType { get; set; }        // Min, Max, Actual, Status
        public string UiControlName { get; set; }    // TextBox name on Home page
        public string LogPropertyName { get; set; }  // TestLog property name
        public bool ShowInReport { get; set; }       // Show in Report grid?
        public string LogGroup { get; set; }         // NEW: "FT" or "TET"
    }
}