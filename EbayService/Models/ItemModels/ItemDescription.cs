using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Models.ItemModels
{
  /// <summary>
  /// Represents the description of an Item in the app
  /// Contains the listing title, listing text, measurements (which become part of the listing text), disclaimers, notes, shipping info, etc
  /// </summary>
  public class ItemDescription
  {
    public long DescriptionId { get; set; }
    public string ListingTitleDescription { get; set; }
    public string MainDescription { get; set; }
    public Disclaimer[] Disclaimers { get; set; }
    public Note[] AdditionalNotes { get; set; }
    public ClothingMeasurement[] Measurements { get; set; }
    public ShippingDetails ShippingDetails { get; set; }
  }
}
