using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Models.ItemModels
{
  /// <summary>
  /// Represents a status for an app item, uniquely identified by its ID
  /// </summary>
  public class ItemStatus
  {
    public long StatusId { get; set; }
    public string StatusDescription { get; set; }
  }
}
