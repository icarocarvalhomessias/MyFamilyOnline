using System.Collections.Generic;

namespace FML.WebApp.MVC.ViewModels
{
    public class FamilyTreeViewModel
    {
        public string Id { get; set; }
        public Relative Pessoa1 { get; set; }
        public Relative Pessoa2 { get; set; }
        public List<FamilyTreeViewModel> Filhos { get; set; } = new List<FamilyTreeViewModel>();
    }
}
