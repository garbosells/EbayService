using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Models.ItemModels
{
  /// <summary>
  /// Represents an attribute of a given item. An item's attributes are based on its category.
  /// </summary>
  public class ItemAttribute
  {
    public long AttributeId { get; set; } //uniquely identifies this attribute for this item
    public long CategoryAttributeId { get; set; } //identifies the category attribute that goes with this -> need to use this to map to Ebay/Etsy item attributes
    public bool Required { get; set; }
    public int ListOrder { get; set; } //determines the order by which the user is prompted for this value in the app
    public string AttributeDescription { get; set; } //used to prompt the user, ie Brand, Style, etc
    public Type AttributeValueType { get; set; }
    public dynamic AttributeValue { get; set; }
  }
}
