namespace csharp_gregslist_api.Repositories;

public class HousesRepository
{
    private readonly IDbConnection _db;

    public HousesRepository(IDbConnection db)
    {
        _db = db;
    }

    public List<House> GetHouses()
    {
        string sql = "SELECT * FROM houses JOIN accounts ON accounts.id == houses.creatorId;";
        List<House> houses = _db.Query<House>(sql).ToList();
        return houses;
    }
}