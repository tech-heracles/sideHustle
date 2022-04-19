namespace DbCore.WebAutoFear.Klasat
{
  public class TecDocType
  {
    public int TecDocTypeId { get; set; }
    public int TecDocModelId { get; set; }
    public int TecDocManufacturerId { get; set; }
    public int VehicleType { get; set; }
    public string VIN { get; set; }
    public string Description { get; set; }
    public string FullDescription { get; set; }
    public int DateFrom { get; set; }
    public int DateTo { get; set; }
    public int KwFrom { get; set; }
    public int KwTo { get; set; }
    public int HpFrom { get; set; }
    public int HpTo { get; set; }
    public int CcmTax { get; set; }
    public int CcmTech { get; set; }
    public int Liter { get; set; }
    public int CylinderNumber { get; set; }
    public int ABS { get; set; }
    public string VehicleMode { get; set; }
    public string EngineMode { get; set; }
    public string DriveType { get; set; }
    public int ASR { get; set; }
    public string BreakSystem { get; set; }
    public string FuelType { get; set; }
    public string CatalystType { get; set; }
    public string TransmissionType { get; set; }
    public string BodyType { get; set; }
    public string[] KeyNumbers { get; set; }      
    public string[] EngineCodes { get; set; }
    public string PlateNumber { get; set; }     
  }
}
