namespace EbayService.Models.ItemModels
{
  /// <summary>
  /// Represents the size of a Clothing Item. Could be descriptive or numeric, could be age specific, could have a region, etc
  /// </summary>
  public class ClothingSize
  {
    public long ClothingSizeId { get; set; }
    public int Region { get; set; }
    public string SizeCategory { get; set; } // "Misses" or "Juniors" or "Kids" etc
    public Gender Gender { get; set; }
    public bool IsNumeric { get; set; }
    public int SizeNumber { get; set; }
    public string SizeDescription { get; set; }
  }
}
