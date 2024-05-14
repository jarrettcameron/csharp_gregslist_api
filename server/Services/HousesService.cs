namespace csharp_gregslist_api.Services;

public class HousesService
{
    private readonly HousesRepository _repository;

    public HousesService(HousesRepository repository)
    {
        _repository = repository;
    }

    public List<House> GetHouses()
    {
        return _repository.GetHouses();
    }

    public House GetHouseById(int HouseId)
    {
        House house = _repository.GetHouseById(HouseId);

        if (house == null)
        {
            throw new Exception("Invalid house Id.");
        }

        return house;
    }

    public House CreateHouse(House houseData, Account userInfo)
    {
        houseData.CreatorId = userInfo.Id;
        return _repository.CreateHouse(houseData);
    }

    public string DestroyHouse(int HouseId, Account userInfo)
    {
        House house = GetHouseById(HouseId);

        if (userInfo.Id != house.CreatorId)
        {
            throw new Exception("Forbidden - You cannot delete a listing you did not create.");
        }

        _repository.DestroyHouse(HouseId);

        return "Deleted house.";
    }

    public House UpdateHouse(int HouseId, Account userInfo, House houseData)
    {
        House houseToUpdate = GetHouseById(HouseId);

        if (houseToUpdate.CreatorId != userInfo.Id)
        {
            throw new Exception("You can't edit a listing you didn't create.");
        }

        houseToUpdate.Price = houseData.Price ?? houseToUpdate.Price;
        houseToUpdate.Description = houseData.Description ?? houseToUpdate.Description;

        return _repository.UpdateHouse(houseToUpdate);
    }
}