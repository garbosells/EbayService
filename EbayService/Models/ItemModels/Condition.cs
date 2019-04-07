using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Models.ItemModels
{
  /// <summary>
  /// Represents the condition of an app item, uniquely identified by its ID
  /// Example: New, Like New, Used - Excellent, Fair, etc
  /// </summary>
  public class Condition
  {
    public long ConditionId { get; set; }
    public string ConditionDescription { get; set; }
  }
}
