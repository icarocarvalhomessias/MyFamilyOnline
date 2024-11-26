using FML.WebApp.MVC.Clients.HttpServices.Interface;
using FML.WebApp.MVC.Services.Interfaces;

namespace FML.WebApp.MVC.Services
{
    public class FamiliaService : IFamiliaService
    {
        private readonly IFamiliaHttpService _familiaHttpService;

        public FamiliaService(IFamiliaHttpService familiaService)
        {
            _familiaHttpService = familiaService;
        }

        public  async Task<Dictionary<string, Dictionary<Relative, Relative>>> GetFamilyTree(Guid familyId)
        {
            var parentes = _familiaHttpService.GetRelativeByFamilyId(familyId).Result;
            var matriarca = parentes.FirstOrDefault(p => p.Matriarch == true);
            var patriarca = parentes.FirstOrDefault(p => p.Patriarch == true);

            if (patriarca == null || matriarca == null)
            {
                return null;
            }

            var resposta = new Dictionary<string, Dictionary<Relative, Relative>>();

            var patriarcas = new Dictionary<Relative, Relative>
            {
                { patriarca, matriarca }
            };

            resposta.Add("Patriarcas", patriarcas);


            var filhos = parentes.Where(p => p.FatherId == patriarca.Id)
                                 .Concat(parentes.Where(p => p.MotherId == patriarca.Id)).ToList();

            var filhosComConjugues = new Dictionary<Relative, Relative>();

            foreach (var filho in filhos)
            {
                var conjuge = parentes.FirstOrDefault(p => p.Id == filho.Spouse);

                if (conjuge != null)
                {
                    filhosComConjugues.Add(filho, conjuge);
                }
            }

            var filhosSemConjugues = filhos.Except(filhosComConjugues.Keys).ToList();

            foreach (var filho in filhosSemConjugues)
            {
                filhosComConjugues.Add(filho, null);
            }

            resposta.Add("Filhos", filhosComConjugues);


            var netos = new List<Relative>();

            foreach (var filho in filhos)
            {
                var filhoDosFilhos = parentes.Where(p => p.FatherId == filho.Id)
                                              .Concat(parentes.Where(p => p.MotherId == filho.Id)).ToList();

                netos.AddRange(filhoDosFilhos);
            }

            var netosComConjugues = new Dictionary<Relative, Relative>();

            foreach (var neto in netos)
            {
                var conjuge = parentes.FirstOrDefault(p => p.Id == neto.Spouse);

                if (conjuge != null)
                {
                    netosComConjugues.Add(neto, conjuge);
                }
            }

            var netosSemConjugues = netos.Except(netosComConjugues.Keys).ToList();

            foreach (var neto in netosSemConjugues)
            {
                netosComConjugues.Add(neto, null);
            }

            resposta.Add("Netos", netosComConjugues);

            var bisnetos = new List<Relative>();

            foreach (var neto in netos)
            {
                var filhoDosNetos = parentes.Where(p => p.FatherId == neto.Id)
                                            .Concat(parentes.Where(p => p.MotherId == neto.Id)).ToList();

                bisnetos.AddRange(filhoDosNetos);
            }

            var bisnetosComConjugues = new Dictionary<Relative, Relative>();

            foreach (var bisneto in bisnetos)
            {
                var conjuge = parentes.FirstOrDefault(p => p.Id == bisneto.Spouse);

                if (conjuge != null)
                {
                    bisnetosComConjugues.Add(bisneto, conjuge);
                }
            }

            var bisnetosSemConjugues = bisnetos.Except(bisnetosComConjugues.Keys).ToList();

            foreach (var bisneto in bisnetosSemConjugues)
            {
                bisnetosComConjugues.Add(bisneto, null);
            }

            resposta.Add("Bisnetos", bisnetosComConjugues);

            var trinetos = new List<Relative>();

            foreach (var bisneto in bisnetos)
            {
                var filhoDosBisnetos = parentes.Where(p => p.FatherId == bisneto.Id)
                                               .Concat(parentes.Where(p => p.MotherId == bisneto.Id)).ToList();

                trinetos.AddRange(filhoDosBisnetos);
            }

            var trinetosComConjugues = new Dictionary<Relative, Relative>();

            foreach (var trineto in trinetos)
            {
                var conjuge = parentes.FirstOrDefault(p => p.Id == trineto.Spouse);

                if (conjuge != null)
                {
                    trinetosComConjugues.Add(trineto, conjuge);
                }
            }

            var trinetosSemConjugues = trinetos.Except(trinetosComConjugues.Keys).ToList();

            foreach (var trineto in trinetosSemConjugues)
            {
                trinetosComConjugues.Add(trineto, null);
            }

            resposta.Add("Trinetos", trinetosComConjugues);

            return resposta;
        }


        public async Task AddRelative(Relative relative)
        {
            await _familiaHttpService.AddRelative(relative);
        }

        public async Task UpdateRelative(Relative relative)
        {
            await _familiaHttpService.UpdateRelative(relative);
        }

        public async Task RemoveRelative(Guid relativeId)
        {
            await _familiaHttpService.RemoveRelative(relativeId);
        }

        public async Task<Relative> GetRelativeById(Guid relativeId)
        {
            return await _familiaHttpService.GetRelativeById(relativeId);
        }

        public async Task<List<Relative>> GetRelativesByFamilyId(Guid familyId)
        {
            return await _familiaHttpService.GetRelativeByFamilyId(familyId);
        }

        public async Task<List<Family>> GetFamilies()
        {
            return await _familiaHttpService.GetFamilies();
        }

        public async Task<List<House>> GetHousesByFamilyId(Guid familyId)
        {
            return await _familiaHttpService.GetHousesByFamilyId(familyId);
        }
    }
}
