namespace EbayService.Controllers
{
  public class GenerateUserAuthorizationUrlResponse
  {
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }
    public string URL { get; set; }
  }
}
