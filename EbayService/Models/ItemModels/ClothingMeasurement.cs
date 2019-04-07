namespace EbayService.Models.ItemModels
{
  public class ClothingMeasurement
  {
    /// <summary>
    /// Represents a single measurement on an item of clothing (sleeve, waist, etc)
    /// </summary>
    public long ClothingMeasurementId { get; set; }
    public ClothingSection Section { get; set; }
    public decimal Value { get; set; }
    public string Units { get; set; }
  }
}
