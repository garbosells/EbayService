using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Models.ItemModels
{
  /// <summary>
  /// Represents a note. Could be a personal note about an item written by the user but not intended to be viewed publically
  /// </summary>
  public class Note
  {
    public long NoteId { get; set; }
    public string NoteText { get; set; }
  }
}
