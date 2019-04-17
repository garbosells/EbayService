using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Models.ItemModels
{
  /// <summary>
  /// Represents an Item created in the app
  /// </summary>
  public class InventoryItem<T>
  {
    public long ItemId { get; set; }
    public long CompanyId { get; set; }
    public DateTime RowAddedDateTime { get; set; }
    public DateTime RowUpdatedDateTime { get; set; }
    public DateTime StatusLastChanged {get; set;}
    public long RowAddedByUserId { get; set; }
    public ItemStatus Status { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal SellingPrice { get; set; }
    //public Image PrimaryImage { get; set; }
    //public Image[] AdditionalImages { get; set; }
    public long CategoryId { get; set; }
    public long SubCategoryId { get; set; }
    public string[] Tags { get; set; } //Limit 15
    public string ShortDescription { get; set; }
    public ItemAttribute<T>[] ItemAttributes { get; set; }
    public ItemDescription Description { get; set; }
    public Condition Condition { get; set; }

  }
}
