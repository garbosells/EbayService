using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Models.ItemModels
{
  /// <summary>
  /// Represents a Clothing Item created in the app
  /// </summary>
  public class ClothingItem<T> : InventoryItem<T>
  {
    public ClothingSize Size { get; set; }
    public ClothingMaterial Material { get; set; }
    public Gender ClothingItemGender { get; set; }
    public Color PrimaryColor { get; set; }
    public Color SecondaryColor { get; set; }
    public Era Era { get; set; }
  }
}
