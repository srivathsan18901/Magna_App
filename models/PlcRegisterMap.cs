namespace Magna_TestApplication.Models
{
    public class PlcRegisterMap
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string ParameterName { get; set; }
        public string RegisterAddress { get; set; }
        public string ValueType { get; set; } // Min, Max, Actual, Status
        public string UiControlName { get; set; }
    }
}