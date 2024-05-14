namespace csharp_gregslist_api.Controllers;

[ApiController]
[Route("api/houses")]
public class HousesController : ControllerBase
{
    private readonly HousesService _housesService;

    public HousesController(HousesService housesService)
    {
        _housesService = housesService;
    }

    [HttpGet]
    public ActionResult<List<House>> GetHouses()
    {
        try
        {
            return Ok(_housesService.GetHouses());
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}