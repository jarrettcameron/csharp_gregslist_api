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
}