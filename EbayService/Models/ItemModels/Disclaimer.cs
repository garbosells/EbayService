using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Models.ItemModels
{
  /// <summary>
   /// Represents a disclaimer
   /// The user company may have pre-saved disclaimers that they want to add to particular listing(s) or every listing by default
   /// This is saved per Company
   /// </summary>
  public class Disclaimer
  {
    public long DisclaimerId { get; set; }
    public string DisclaimerDescription { get; set; }
    public string DisclaimerText { get; set; }
  }
}
