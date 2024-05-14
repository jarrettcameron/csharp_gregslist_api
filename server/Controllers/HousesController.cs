namespace csharp_gregslist_api.Controllers;

[ApiController]
[Route("api/houses")]
public class HousesController : ControllerBase
{
    private readonly HousesService _housesService;
    private readonly Auth0Provider _auth0provider;

    public HousesController(HousesService housesService, Auth0Provider auth0Provider)
    {
        _housesService = housesService;
        _auth0provider = auth0Provider;
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

    [HttpGet("{HouseId}")]
    public ActionResult<House> GetHouseById(int HouseId)
    {
        try
        {
            return Ok(_housesService.GetHouseById(HouseId));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<House>> CreateHouse([FromBody] House houseData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_housesService.CreateHouse(houseData, userInfo));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpDelete("{HouseId}")]
    public async Task<ActionResult<string>> DestroyHouse(int HouseId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_housesService.DestroyHouse(HouseId, userInfo));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPut("{HouseId}")]
    public async Task<ActionResult<House>> UpdateHouse(int HouseId, [FromBody] House houseData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            return _housesService.UpdateHouse(HouseId, userInfo, houseData);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}