using FML.Core.Data;
using FML.Familiares.API.Clients;
using FML.Familiares.API.Data.Repository.Interface;
using FML.Familiares.API.Models;
using FML.Familiares.API.Services.Interface;

namespace FML.Familiares.API.Services
{
    public class RelativeService : IRelativeService
    {
        private readonly IRelativeRepository _relativeRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly IFileHttp _fileHttp;

        public RelativeService(
            IRelativeRepository relativeRepository,
            IFamilyRepository familyRepository,
            IHouseRepository houseRepository,
            IFileHttp fileHttp)
        {
            _relativeRepository = relativeRepository;
            _familyRepository = familyRepository;
            _houseRepository = houseRepository;
            _fileHttp = fileHttp;
        }

        public async Task<IEnumerable<Relative>> GetRelatives()
        {
            var id = Constantes.FamiliaCarvalhoId;
            var relatives = await _relativeRepository.GetRelativesByFamilyId(id);

            foreach (var relative in relatives.Where(x => x.FotoId != null))
            {
                var url = await _fileHttp.ImageUrlAsync(relative.FotoId.Value);
                relative.FotoPerfil = url;
            }

            return relatives;
        }

        public async Task<Relative> GetRelativeById(Guid relativeId)
        {
            var relative = await _relativeRepository.GetRelativeById(relativeId);

            if (relative.FotoId != null)
            {
                var url = await _fileHttp.ImageUrlAsync(relative.FotoId.Value);
                relative.FotoPerfil = url;
            }

            return relative;
        }

        public async Task<Guid> CargaInicial()
        {
            var familia = new Family();
            familia.Id = Guid.NewGuid();
            familia.Name = "Parker";
            familia.StartDate = new DateTime(1960, 1, 1);
            familia.IsActive = true;
            familia.History = "Familia Parker";

            await _familyRepository.AddFamily(familia);

            #region Casas 

            var casasParker = new List<House>();

            var parker = new House
            {
                Id = Guid.NewGuid(),
                Name = "Parker",
                FamilyId = familia.Id,
                IsActive = true
            };

            var parkerOsborn = new House
            {
                Id = Guid.NewGuid(),
                Name = "Parker Osborn",
                FamilyId = familia.Id,
                IsActive = true
            };

            casasParker.Add(parkerOsborn);

            foreach (var casa in casasParker)
            {
                await _houseRepository.AddHouse(casa);
            }

            await _familyRepository.UpdateFamily(familia);

            #endregion

            Relative peter, maryJane;
            GetPeterAndMaryJane(parker, out peter, out maryJane);

            Relative may, ben, harry, gwen, norman, miles, felicia, eddie, flash, liz, betty, jessica, anastasia, cindy, silk;
            ParkerOsborn(parkerOsborn, peter, maryJane, out may, out ben, out harry, out gwen, out norman, out miles, out felicia, out eddie, out flash, out liz, out betty, out jessica, out anastasia, out cindy, out silk);

            Final(may, harry, miles, felicia, jessica, anastasia);

            var relatives = new List<Relative>
            {
                peter, maryJane,
                may, ben
            };

            foreach (var relative in relatives)
            {
                relative.FamilyId = familia.Id;
                relative.HouseId = relative.House.Id;

                _relativeRepository.AddRelative(relative);
            }

            if (!await _relativeRepository.UnitOfWork.Commit())
            {
                Console.WriteLine("Erro ao salvar os dados");
            }

            return familia.Id;
        }

        public async Task<bool> RemoveRelative(Guid relativeId)
        {
            return await _relativeRepository.RemoveRelative(relativeId);
        }

        #region Private Methods

        private static void ParkerOsborn(House parkerOsborn, Relative peter, Relative maryJane, out Relative may, out Relative ben, out Relative harry, out Relative gwen, out Relative norman, out Relative miles, out Relative felicia, out Relative eddie, out Relative flash, out Relative liz, out Relative betty, out Relative jessica, out Relative anastasia, out Relative cindy, out Relative silk)
        {
            may = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "May",
                LastName = "Parker",
                BirthDate = new DateTime(1950, 1, 1),
                House = parkerOsborn,
                FatherId = peter.Id,
                MotherId = maryJane.Id,
                Gender = Gender.Female,
            };
            ben = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Ben",
                LastName = "Parker",
                BirthDate = new DateTime(1950, 1, 1),
                House = parkerOsborn,
                IsActive = false
            };
            may.Spouse = ben.Id;
            ben.Spouse = may.Id;

            harry = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Harry",
                LastName = "Osborn",
                BirthDate = new DateTime(1970, 1, 1),
                House = parkerOsborn,
                FatherId = ben.Id,
                MotherId = may.Id
            };
            gwen = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Gwen",
                LastName = "Stacy",
                BirthDate = new DateTime(1970, 1, 1),
                House = parkerOsborn,
                FatherId = ben.Id,
                MotherId = may.Id
            };
            norman = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Norman",
                LastName = "Osborn",
                BirthDate = new DateTime(1970, 1, 1),
                House = parkerOsborn,
                FatherId = ben.Id,
                MotherId = may.Id
            };
            miles = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Miles",
                LastName = "Morales",
                BirthDate = new DateTime(1970, 1, 1),
                House = parkerOsborn,
                IsActive = false
            };
            norman.Spouse = miles.Id;
            miles.Spouse = norman.Id;

            felicia = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Felicia",
                LastName = "Hardy",
                BirthDate = new DateTime(1970, 1, 1),
                House = parkerOsborn,
                FatherId = ben.Id,
                MotherId = may.Id
            };
            eddie = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Eddie",
                LastName = "Brock",
                BirthDate = new DateTime(1970, 1, 1),
                House = parkerOsborn,
                IsActive = false
            };
            felicia.Spouse = eddie.Id;
            eddie.Spouse = felicia.Id;

            flash = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Flash",
                LastName = "Thompson",
                BirthDate = new DateTime(1990, 1, 1),
                House = parkerOsborn,
                FatherId = eddie.Id,
                MotherId = felicia.Id
            };
            liz = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Liz",
                LastName = "Allan",
                BirthDate = new DateTime(1990, 1, 1),
                House = parkerOsborn,
                IsActive = false
            };
            liz.Spouse = flash.Id;
            flash.Spouse = liz.Id;

            betty = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Betty",
                LastName = "Brant",
                BirthDate = new DateTime(1990, 1, 1),
                House = parkerOsborn,
                FatherId = eddie.Id,
                MotherId = felicia.Id
            };
            jessica = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jessica",
                LastName = "Drew",
                BirthDate = new DateTime(1990, 1, 1),
                House = parkerOsborn,
                FatherId = eddie.Id,
                MotherId = felicia.Id
            };
            anastasia = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Anastasia",
                LastName = "Romanova",
                BirthDate = new DateTime(1990, 1, 1),
                House = parkerOsborn,
                FatherId = miles.Id,
                MotherId = norman.Id
            };
            cindy = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Cindy",
                LastName = "Moon",
                BirthDate = new DateTime(1990, 1, 1),
                House = parkerOsborn,
                FatherId = miles.Id,
                MotherId = norman.Id
            };
            silk = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Silk",
                LastName = "Moon",
                BirthDate = new DateTime(1990, 1, 1),
                House = parkerOsborn,
                FatherId = miles.Id,
                MotherId = norman.Id
            };
        }

        private static void GetPeterAndMaryJane(House parker, out Relative peter, out Relative maryJane)
        {
            peter = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Peter",
                LastName = "Parker",
                BirthDate = new DateTime(1930, 1, 1),
                House = parker,
                Patriarch = true,
                Gender = Gender.Male,
                FamilyId = parker.FamilyId,
                IsActive = false
            };
            maryJane = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Mary Jane",
                LastName = "Watson",
                BirthDate = new DateTime(1930, 1, 1),
                FamilyId = parker.FamilyId,
                House = parker,
                Matriarch = true,
                Gender = Gender.Female,
                IsActive = false
            };

            peter.Spouse = maryJane.Id;
            maryJane.Spouse = peter.Id;
        }

        private static void Final(Relative may, Relative harry, Relative flash, Relative liz, Relative jessica, Relative anastasia)
        {
            jessica.SecretSanta = true;
            jessica.Phone = "1 857 289-6282";
            jessica.LinkName = "https://www.4shared.com/s/fdSX8S8vXku";

            harry.SecretSanta = true;
            harry.Phone = "35 98865-8438";
            harry.LinkName = "https://www.4shared.com/s/fXDizycDmku";

            may.SecretSanta = true;
            may.Phone = "35 98865-8438";
            may.LinkName = "https://www.4shared.com/s/fSbUBXRTIge";

            anastasia.SecretSanta = true;
            anastasia.Phone = "35 99107-8315";
            anastasia.LinkName = "https://www.4shared.com/s/fjGOez6Nmku";

            flash.SecretSanta = true;
            flash.Phone = "35 98436-9551";
            flash.LinkName = "https://www.4shared.com/s/fpr9mL_PFjq";

            liz.SecretSanta = true;
            liz.Phone = "44 9173-3999";
            liz.LinkName = "https://www.4shared.com/s/fbV0LCnq9ku";
        }

        #endregion
    }
}
