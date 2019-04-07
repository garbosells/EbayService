namespace EbayService.Models.ItemModels
{
  /// <summary>
  /// Represents a period of time (this is used for vintage classifications on the platform)
  /// </summary>
  public class Era
  {
    public long EraId { get; set; }
    public string EraDescription { get; set; }
  }
}
