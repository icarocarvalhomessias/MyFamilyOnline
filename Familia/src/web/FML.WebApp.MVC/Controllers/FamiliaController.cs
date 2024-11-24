using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FML.WebApp.MVC.Controllers
{
    [Authorize]
    public class FamiliaController : Controller
    {
        private readonly IFamiliaHttpService _familiaService;

        public FamiliaController(IFamiliaHttpService familiaService)
        {
            _familiaService = familiaService;
        }

        public IActionResult Index(Guid familyId)
        {
            familyId = Guid.Parse("63c3763d-81f7-47e5-8978-b9e1a1f369e7");
            var parentes = _familiaService.GetRelativeByFamilyId(familyId).Result;

            var matriarca = parentes.FirstOrDefault(p => p.Matriarch == true);
            var patriarca = parentes.FirstOrDefault(p => p.Patriarch == true);

            if (patriarca == null || matriarca == null)
            {
                // Handle the case where there is no matriarca or patriarca
                return View(new Dictionary<string, List<Relative>>());
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


            return View(resposta);
        }


    }
}
