using FML.Familiares.API.Data.Repository.Interface;
using FML.Familiares.API.Services.Interface;

namespace FML.Familiares.API.Services
{
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _houseRepository;

        public HouseService(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        public async Task<IEnumerable<House>> GetHousesByFamilyId(Guid familyId)
        {
            return await _houseRepository.GetHousesByFamilyId(familyId);
        }
    }
}
