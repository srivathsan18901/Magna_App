public class PlcRegisterMap
{
    public int Id { get; set; }
    public string Category { get; set; }
    public string ParameterName { get; set; }
    public string RegisterAddress { get; set; }    // Primary (e.g. D106)
    public string RegisterAddress2 { get; set; }   // Paired  (e.g. D107) — NEW
    public string ValueType { get; set; }          // Maximum / Minimum / Actual / Status
    public string DataType { get; set; }           // "Float32" / "Int16" — NEW
    public string UiControlName { get; set; }
    public string LogPropertyName { get; set; }
    public string LogGroup { get; set; }
    public bool ShowInReport { get; set; }
}