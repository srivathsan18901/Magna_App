public class PlcRegisterMap
{
    public int Id { get; set; }
    public string Category { get; set; }
    public string ParameterName { get; set; }
    public string RegisterAddress { get; set; }
    public string ValueType { get; set; }
    public string UiControlName { get; set; }
    public string LogPropertyName { get; set; }
    public bool ShowInReport { get; set; }
    public string LogGroup { get; set; }   // NEW: "FT" or "TET"
}