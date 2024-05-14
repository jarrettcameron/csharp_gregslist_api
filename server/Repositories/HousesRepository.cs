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
        string sql = "SELECT * FROM houses JOIN accounts ON accounts.id = houses.creatorId;";
        List<House> houses = _db.Query<House, Account, House>(sql, (house, account) =>
        {
            house.Creator = account;
            return house;
        }).ToList();
        return houses;
    }

    public House GetHouseById(int HouseId)
    {
        string sql = "SELECT * FROM houses JOIN accounts ON accounts.id = houses.creatorId WHERE houses.id = @HouseId;";

        House house = _db.Query<House, Account, House>(sql, (house, account) =>
        {
            house.Creator = account;
            return house;
        }, new { HouseId }).FirstOrDefault();

        return house;
    }

    public House CreateHouse(House houseData)
    {
        string sql = @"INSERT INTO
            houses (
                sqft,
                bedrooms,
                bathrooms,
                imgUrl,
                description,
                price,
                creatorId
            ) VALUES (@Sqft, @Bedrooms, @Bathrooms, @ImgUrl, @Description, @Price, @CreatorId);

        SELECT * FROM houses
        JOIN accounts ON accounts.id = houses.creatorId
        WHERE houses.id = LAST_INSERT_ID();";
        House house = _db.Query<House, Account, House>(sql, (house, account) =>
        {
            house.Creator = account;
            return house;
        }, houseData).FirstOrDefault();
        return house;
    }

    public void DestroyHouse(int HouseId)
    {
        string sql = "DELETE FROM houses WHERE id = @HouseId;";
        _db.Execute(sql, new { HouseId });
    }

    public House UpdateHouse(House houseData)
    {
        string sql = @"UPDATE houses SET
            price = @Price,
            description = @Description WHERE id = @Id
        ;

        SELECT * FROM houses JOIN accounts ON accounts.id = houses.creatorId WHERE houses.id = @Id;";

        House house = _db.Query<House, Account, House>(sql, (house, account) =>
        {
            house.Creator = account;
            return house;
        }, houseData).FirstOrDefault();

        return house;
    }
}